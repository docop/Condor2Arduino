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
using System.Threading; // threading

namespace Condor2Arduino
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
    
    public partial class Form1 : Form
    {
        
      public Form1()

        {
            InitializeComponent();
            Portfiller();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerReportsProgress = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
        }
       
        public SerialPort port;
        public bool arduino = false;
        int speedkmph;
        string condordata; 
        string extract;
        planedata PlaneData;
        SerialConnect serialconnect = new SerialConnect();

        public void btnConnect_Click(object sender, EventArgs e) //event when buttonConnect UDP is clicked
        {
                if (!serialconnect.connected)
                {
                    backgroundWorker1.RunWorkerAsync();
                    label1.Text = "Connected";
                    btnConnect.Text = "Disconnect";
                    timer1.Enabled = true;
                }
                else // we are connected so we disconnect
                {
                    backgroundWorker2.CancelAsync();
                    backgroundWorker1.CancelAsync();
                    label1.Text = "disconnected";
                    btnConnect.Text = "Connect";
                    serialconnect.CondorDisConnect();
                    timer1.Enabled = false;
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // if (serialconnect.connected) condordata = serialconnect.GetCondorData();
            textBox2.Text = condordata;

            if (condordata != "-1") 
            {
                label1.Text = "Receiving data";
                if (condordata!=null)
                ConvertCondorData(condordata);
                ShowConvertedData(PlaneData);
                
                // ************** Build & Send Arduino Serial String
                string Send2Arduino = 
                    "<S" + PlaneData.speed + 
                    "<V" + PlaneData.varioraw + 
                    "<A" +PlaneData.altitudebaro +
                    "<G" +PlaneData.gforce;
                    // the actual string to send to the serial port

                if (arduino)
                {
                    port.Write(Send2Arduino); // send it
                    textBoxTestData.Text = Send2Arduino;
                } // show it. testing only
            }
            else
            {
                label1.Text = "No Data Received";

            }
        }

        public void Portfiller()
        {
            foreach (string s in SerialPort.GetPortNames())
                comboBoxCom.Items.Add(s);
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
                    PlaneData.altitudebaro = "00" + a.ToString(); // alt 90m --> 0090m
                if (a < 1000 && a >= 100)
                    PlaneData.altitudebaro = "0" + a.ToString(); // alt 900m --> 0900m
                if (a >= 1000)
                    PlaneData.altitudebaro = a.ToString(); // alt 9000m --> 9000m
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

        public void Send2Arduino(string str)
        {
            try
            {
                port.Write(str);
            }
            catch { }
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
                port.Write("=A");
            }
            catch (Exception f) { }
        }

        private void buttonZEROdwn_Click(object sender, EventArgs e)
        {
            try
            {
                port.Write("=D");
            }
            catch (Exception f) { }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!serialconnect.connected)
            {
                serialconnect.CondorConnect(Convert.ToInt32(textBox1.Text));
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (serialconnect.connected)
            {
                condordata = serialconnect.GetCondorData();
            }
        }

        
    }
     
}
