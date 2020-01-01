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

namespace Condor2Arduino
{
    

    public partial class Form1 : Form
    {
        public struct planedata
        {
            public string speed;
            public string altitudebaro;
            public string bank;
            public string varioraw;
            public string variointegrated;
            public string varioelec;
            public string gforce;
        }

        public Form1()

        {
            InitializeComponent();
            
        }
       
        UdpClient Ontvanger; 
        IPEndPoint listenEndPoint;
        byte[] receivedData;
        int poort;
        int speedkmph;
        string condordata, extract;
        SerialPort port;
        bool connected = false;
        bool arduino = false;
        planedata PlaneData;

        public void btnConnect_Click(object sender, EventArgs e) //event when buttonConnect UDP is clicked
        {
            try
            {
                if (!connected)
                {
                    poort = Convert.ToInt32(textBox1.Text); //converts port string to variable  poort as int32
                    Ontvanger = new UdpClient(poort); //create new client using port 'poort'
                    Ontvanger.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);
                    listenEndPoint = new IPEndPoint(IPAddress.Any, poort);
                    label1.Text = "Connected";
                    btnConnect.Text = "Disconnect";
                    connected = true;
                    timer1.Enabled = true;
                    
                }
                else
                {
                    Ontvanger.Close();
                    label1.Text = "disconnected";
                    btnConnect.Text = "Connect";
                    connected = false;
                    timer1.Enabled = false;
                }
            }
            catch (Exception f) { } // no exception catch coded. 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            condordata = GetCondorData();
            textBox2.Text = condordata;

            if (condordata != "-1")
            {
                label1.Text = "Receiving data";
                ConvertCondorData(condordata);
                ShowConvertedData(PlaneData);
                
                // ************** Build & Send Arduino Serial String
                string Send2Arduino = "<G" + PlaneData.speed + "<B" + PlaneData.varioraw; // the actual string to send to the serial port
                
                if (arduino) port.Write(Send2Arduino); // send it
                textBoxTestData.Text = Send2Arduino; // show it. testing only
            }
            else
            {
                label1.Text = "No Data Received";

            }
        }
        public string GetCondorData()
        {
                  try
                    {
                   
                        receivedData = Ontvanger.Receive(ref listenEndPoint); // blocking code via threads proberen te omzeilen maar werkt niet: nog aanpassen
                        return (Encoding.ASCII.GetString(receivedData));
                       
                    }
                catch (Exception f) { return "-1";}
            }


        public void ConvertCondorData(string s) //s=condordata 
        {
            // airspeed incl wind m/s --> km/h
            if (s.Contains("airspeed"))
            {
                int pos1 = s.IndexOf("airspeed="); // posnumber where airspeed starts
                extract = s.Substring(pos1 + 9, 4);
                double a = (double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat)) * 3.6; // in km/h
                speedkmph = Convert.ToInt32(a);
                if (speedkmph < 10)
                    PlaneData.speed = "000" + speedkmph.ToString(); // speed 1 km/h --> 0001 km/h - we want 4 chars in the string
                if (speedkmph < 100 && speedkmph >= 10)
                    PlaneData.speed = "00" + speedkmph.ToString(); // debug info on screen
                if (speedkmph >= 100)
                    PlaneData.speed = "0" + speedkmph.ToString(); // debug info on screen
            }
            
            // Altitude (m or ft according to units selected)
            if (s.Contains("altitude"))
            {
                int pos1 = s.IndexOf("altitude="); // posnumber where altitude starts
                extract = s.Substring(pos1 + 9, 6);
                int a = Convert.ToInt32((double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat))); // meters afgerond naar beneden 
                if (a < 10)
                    PlaneData.altitudebaro = "000" + a.ToString(); // alt 9m--> 0009m - we want 4 chars in the string
                if (a < 100 && a >= 10)
                    PlaneData.altitudebaro = "00" + a.ToString();
                if (a >= 100)
                    PlaneData.altitudebaro = "0" + a.ToString();
            }
           
            // Bankangle (radians) --> deg
            if (s.Contains("bank"))
            {
                int pos1 = s.IndexOf("bank="); // posnumber where altitude starts
                extract = s.Substring(pos1 + 5, 6);
                if (extract == "0\r\nqua")
                {
                    PlaneData.bank = "0";
                }
                else
                {
                    double a = double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat);
                    PlaneData.bank = (Convert.ToInt32(a * 57.2957)).ToString();
                }
                
            }

            // pneumatic variometer reading (m/s)
            if (s.Contains("vario"))
            {
                int pos1 = s.IndexOf("vario="); // posnumber where vario starts
                if (s.Substring(pos1 + 6, 1) == "-")
                    PlaneData.varioraw = s.Substring(pos1 + 6, 4);
                else
                    PlaneData.varioraw = "+" + s.Substring(pos1 + 6, 3);
            }

            // electronic variometer reading (m/s)
            if (s.Contains("evario"))
            {
                int pos1 = s.IndexOf("evario="); // posnumber where altitude starts
                if (s.Substring(pos1 + 7, 1) == "-")
                    PlaneData.varioelec = s.Substring(pos1 + 7, 4);
                else
                    PlaneData.varioelec = "+" + s.Substring(pos1 + 7, 3);
            }

            // Integrated vario (m/s)
            if (s.Contains("integrator"))
            {
                int pos1 = s.IndexOf("integrator="); // posnumber where altitude starts
                if (s.Substring(pos1 + 11, 1) == "-")
                    PlaneData.variointegrated = s.Substring(pos1 + 11, 4);
                else
                    PlaneData.variointegrated = "+" + s.Substring(pos1 + 11, 3);
            }

            // Gforce (G)
            if (s.Contains("gforce"))
            {
                int pos1 = s.IndexOf("gforce="); // posnumber where altitude starts
                 if (s.Substring(pos1 + 7, 1) == "-")
                    PlaneData.gforce = s.Substring(pos1 + 7, 4);
                else
                    PlaneData.gforce= "+" + s.Substring(pos1 + 7, 3);
            }

        }

        private void ShowConvertedData(planedata a)
        {
            textBoxBankdeg.Text = a.bank;
            textBoxIntegrated.Text = a.variointegrated;
            textBoxVario.Text = a.varioraw;
            textBoxEvario.Text = a.varioelec;
            textBoxSpeedkmh.Text = a.speed;
            textBoxHeight.Text = a.altitudebaro;
            textBoxGforce.Text = a.gforce;
        }

        public void Send2Arduino(string e)
        {
            port.Write(e);
        }

         private void buttonCom_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonCom.Text == "Connect")
                {
                    port = new SerialPort(comboBoxCom.Text, Convert.ToInt32(textBoxBaud.Text), Parity.None, 8, StopBits.One);
                    port.Open();
                    buttonCom.Text = "Disconnect";
                    arduino = true;


                }
                else
                {
                    port.Close();
                    port.Dispose();
                    buttonCom.Text = "Connect";
                    arduino = false;
                }
            }
            catch (Exception f) { }
        }
        

         
        private void buttonZEROup_Click(object sender, EventArgs e)
        {
            try
            {
                port.Write("<A");
            }
            catch (Exception f) { }
        }

        private void buttonZEROdwn_Click(object sender, EventArgs e)
        {
            try
            {
                port.Write("<D");
            }
            catch (Exception f) { }
        }

        
    }
     
}
