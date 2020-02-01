using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.IO.Ports; // for the serial port connections
using System.Threading; // threading to prevent blocking

// How does this work:
// ******************* 
// CONDOR via UDP--> (string)condordata-->(struct)PLANEDATA glider -->(BYTE[])serialdata--> via serialport-->Arduino-->Decoded into variables.
// CondorConnect.cs = connect to condor, convert the data and store it into the struct 'glider'
// FormC2A.cs = visual part and setting of portnumber, arduino port and timer.
// Send2Arduino.cs = convert the struct 'glider'into Bytes (serialdata) and send it to the Arduino through a COMport.

namespace Condor2Arduino
{
   
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent(); // C# thing.
            Portfiller(); // Get the available COM ports and fill them in the dropdown
            comboBoxBaudrate.SelectedIndex = 0; // Set the default Baudrate
            comboBoxComPort.SelectedIndex = 0; // Set the default COM port
            labelTick.Text = trackBar1.Value.ToString(); // Set the label to show the current selected interbal (millisec)
            timer1.Interval = trackBar1.Value;// Set the timer interval to the default
        }  
       
       
        CondorConnect condorconnect = new CondorConnect();
        Send2Arduino send2arduino = new Send2Arduino();
        CondorConnect.PLANEDATA glider,decoded; // we store all condordata into the variable 'glider'. 'decoded' is used for debugging

       
        public void btnConnect_Click(object sender, EventArgs e) //Connect - Disconnect button Condor
        {
                if (!condorconnect.condorconnected) //if not connected then..
                {
                    backgroundWorker1.RunWorkerAsync(); // start the background worker thread
                    btnConnectCondor.Text = "Disconnect"; 
                    labelStatus.Text = "connected";
                    timer1.Enabled = true; // start the timer
                }
                else // we are connected so we want to disconnect
                {
                    backgroundWorker2.CancelAsync(); // stop the background worker (fetching and converting condordata) 
                    backgroundWorker1.CancelAsync(); // stop the connector to condor
                    labelStatus.Text = "disconnected"; 
                    btnConnectCondor.Text = "Connect";
                    condorconnect.CondorDisConnect(); // stop and dispose of the UDP connection
                    timer1.Enabled = false; // stop the timer
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (condorconnect.condorconnected) // if connected to condor = true
            {
                textBox2.Text = glider.raw; // show the raw data as we recieved it.
                if (glider.check) // check = true if condordata filled. check =false if we catch exception
                {
                    SendByte2Arduino(); // Send the converted Bytes to the Arduino. In a timer routine because it went nuts when trying to send it in a while loop. its send and forget. no handshaking stuff.
                    
                    DecodeData(glider.serialdata); // For debugging only. Convert the data as it is converted in the Arduino so we can check if it is valid 
                    ShowConvertedData(decoded,glider); // For debugging only. Show the converted data on screen
                }
            }
            else // something went wrong
            {
                labelStatus.Text = "No Data Received"; // and no condordata is sent to the arduino
                
            }
        }
        
      private void Portfiller() // just a routine to show connected COM port devices
        {
            foreach (string s in SerialPort.GetPortNames())
                comboBoxComPort.Items.Add(s);
       }

     private void DecodeData(byte[] b) // for debugging only
        {
            decoded.altitudebaro = b[1] << 8 | b[2];
            decoded.speedias = b[3] << 8 | b[4];
            decoded.compass = b[5] << 8 | b[6];
            decoded.pitch = (b[7] << 8 | b[8]) - 90; // Ik weet niet hoe ik de negatieve waarden kan doorsturen en converteren. Vandaar de optelling van de max negatieve waarde.
            decoded.bank = (b[9] << 8 | b[10]) - 180;
            decoded.varioraw = Math.Round(((b[11] << 8 | b[12]) / 10.0) - 100.0, 2);
            decoded.varioelec = Math.Round(((b[13] << 8 | b[14]) / 10.0) -100.0, 2);
            decoded.varioint = Math.Round(((b[15] << 8 | b[16]) / 10.0) - 100.0, 2);
            decoded.gforce = Math.Round(((b[17] << 8 | b[18]) / 10.0) - 10.0, 1);
            decoded.yawstring = (b[19] << 8 | b[20])-50 ;
        }
           
        private void ShowConvertedData(CondorConnect.PLANEDATA a, CondorConnect.PLANEDATA b) // a is decoded bytes data. b is decoded stringdata
        {
            textBoxDecodeAlt.Text = a.altitudebaro.ToString();
            textBoxDecodeBank.Text = a.bank.ToString();
            textBoxDecodeCompass.Text = a.compass.ToString();
            textBoxDecodeGforce.Text = a.gforce.ToString();
            textboxDecodeSpeed.Text = a.speedias.ToString();
            textBoxDecodeVarraw.Text = a.varioraw.ToString();
            textBoxDecodeVarint.Text = a.varioint.ToString();
            textBoxDecodevarElec.Text = a.varioelec.ToString();
            textBoxDecodePitch.Text = a.pitch.ToString();
            textBoxyawstring.Text = a.yawstring.ToString();

            TBSaltitude.Text=b.altitudebaro.ToString();
            TBSbank.Text = b.bank.ToString();
            TBScompass.Text = b.compass.ToString();
            TBSgforce.Text = b.gforce.ToString();
            TBSspeed.Text = b.speedias.ToString();
            TBSvarraw.Text = b.varioraw.ToString();
            TBSvarint.Text = b.varioint.ToString();
            TBSvarelec.Text = b.varioelec.ToString();
            TBSpitch.Text = b.pitch.ToString();
            TBSyawstring.Text = b.yawstring.ToString();
        }

        private void buttonCom_Click(object sender, EventArgs e) //Arduino Connect-Disconnect button
        {
            try
            {
                if (send2arduino.arduino == false) // not connected
                {
                    if (send2arduino.ArduinoConnect(comboBoxComPort.Text, Convert.ToInt32(comboBoxBaudrate.Text))) // connection succes
                    {
                        BtnConnectArduino.Text = "Disconnect";
                        labelStatusArduino.Text = "Connected";
                    }
                    else labelStatusArduino.Text = "error arduino connect"; 
                }
                else // we are connected lets disconnect 
                    if (send2arduino.ArduinoDisconnect())
                    {
                            BtnConnectArduino.Text = "Connect";
                            labelStatusArduino.Text = "Disconnected";
                    }
                    else labelStatusArduino.Text = "error arduino disconnect"; 
                }
            catch 
            {
                labelStatusArduino.Text = "unknown error arduino connection"; 
            }
        }
         
        private void SendByte2Arduino() // I put this in a timer routine to limit the amount of data sent. if < 30 millsec interval the arduino goes nuts
         {
            if (send2arduino.arduino) // we are connected
            try
             {
                 if (send2arduino.SendByte2Arduino(glider.serialdata, glider.serialdata.Length)) // sending returns true
                 labelStatusArduino.Text = "sending arduinodata";
                 else
                 labelStatusArduino.Text = "error sending arduino data"; // something went wrong with sending
             }
             
             catch { labelStatusArduino.Text = "unknown error Arduino"; }
         } 
   
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!condorconnect.condorconnected)
            {
                condorconnect.CondorConnector(Convert.ToInt32(textBoxPortCondor.Text));
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (condorconnect.condorconnected) // fill the glider struct
            {
                glider = condorconnect.GetCondorData();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            labelTick.Text = trackBar1.Value.ToString();
            timer1.Interval = trackBar1.Value;
        }
       
    }
}
