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

namespace Condor2Arduino
{

    public struct planedata //For now the communication to Arduino is by string. Option I want to explore is using bytes. ToDo
    {
        public string speed; // converted to km/h
        public string altitudebaro; // m or feet depends on Condor setting
        public string bank; // converted from radians to degrees
        public string pitch; // converted from radians to degrees
        public string varioraw; //pneumatic variometer reading
        public string variointegrated; //vario integrator value
        public string varioelec; //electronic variometer reading
        public string gforce; 
        public string kompas;
    }

    public struct SENDBYTES // first attempt in sending and converting data with bytes. ToDo!!
    {
        public Byte Speed;
        public Byte Altitudbaro;
        public Byte speed; // converted to km/h
        public Byte altitudebaro; // m or feet depends on Condor setting
        public Byte bank; // converted to degrees
        public Byte varioraw; //pneumatic variometer reading
        public Byte variointegrated; //vario integrator value
        public Byte varioelec; //electronic variometer reading
        public Byte gforce;
        public Byte kompas;
    }
    
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Portfiller(); // Get the Serial ports
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
                if (!serialconnect.connected) //if not connected then..
                {
                    backgroundWorker1.RunWorkerAsync();
                    label1.Text = "Connected";
                    btnConnect.Text = "Disconnect";
                    timer1.Enabled = true;
                }
                else // we are connected so ..
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

            if (condordata != "-1") // -1 is my primitive exception handler. Send "-1" when something goes wrong.
            {
                label1.Text = "Receiving data";
                if (condordata != null)
                {
                    ConvertCondorData(condordata);
                    ShowConvertedData(PlaneData);


                    // ************** Build & Send Arduino Serialdata String
                    string Send2Arduino =
                          "<S" + PlaneData.speed //000-999
                        + "<V" + PlaneData.varioraw //I have limited  it to +9.9 and -9.9
                        + "<E" + PlaneData.varioelec//I have  limited it to +9.9 and -9.9
                        + "<I" + PlaneData.variointegrated//I have limited it to +9.9 and -9.9
                        + "<A" + PlaneData.altitudebaro//0000 9999
                        + "<K" + PlaneData.kompas//000 360
                        + "<B" + PlaneData.bank//-180+180
                        + "<P" + PlaneData.pitch//-090 +090
                        + "<G" + PlaneData.gforce;//I have limited it to +9.9 and -9.9

                    textBoxTestData.Text = Send2Arduino; // show it. for testing only
                    if (arduino) // if connected to arduino
                    {
                        port.Write(Send2Arduino); // send it
                    }
                }
            }
            else // something went wrong
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
                try
                {
                    {
                        int pos1 = s.IndexOf("airspeed="); // posnumber where airspeed starts
                        extract = s.Substring(pos1 + 9, 4);
                        double a = (double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat)) * 3.6; // in km/h
                        speedkmph = Convert.ToInt32(a);
                        
                        if (speedkmph < 0) PlaneData.speed = "000";
                        if (speedkmph < 10)
                            PlaneData.speed = "000" + speedkmph.ToString(); // speed 1 km/h --> 0001 km/h - we want 4 chars in the string
                        if (speedkmph < 100 && speedkmph >= 10)
                            PlaneData.speed = "00" + speedkmph.ToString();
                        if (speedkmph >= 100)
                            PlaneData.speed = "0" + speedkmph.ToString(); 
                        if (speedkmph >= 999)
                            PlaneData.speed = "999"; 
                  }
                }
                catch { PlaneData.speed = "X"; }

            // Altitude (m or ft according to units selected)
            if (s.Contains("altitude"))
            {
                try
                {
                    int pos1 = s.IndexOf("altitude="); // posnumber where altitude starts
                    extract = s.Substring(pos1 + 9, 6);

                    int a = Convert.ToInt32((double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat))); // meters afgerond naar beneden 
                    
                    if (a <= 0)
                        PlaneData.altitudebaro = "0000"; //no negative alt to arduino
                    if (a < 10 && a > 0)
                        PlaneData.altitudebaro = "000" + a.ToString(); // alt 9m--> 0009m - we want 4 chars in the string
                    if (a < 100 && a >= 10)
                        PlaneData.altitudebaro = "00" + a.ToString(); // alt 90m --> 0090m
                    if (a < 1000 && a >= 100)
                        PlaneData.altitudebaro = "0" + a.ToString(); // alt 900m --> 0900m
                    if (a >= 1000 && a < 9999)
                        PlaneData.altitudebaro = a.ToString(); // alt 9000m --> 9000m
                    if (a >= 9999) PlaneData.altitudebaro = "9999"; // limit 9999 m
                }
                catch { PlaneData.altitudebaro = "X"; }
            }

            // Bankangle (radians) --> deg
            if (s.Contains("bank"))
                try
                {
                    int pos1 = s.IndexOf("bank="); // posnumber where bank starts
                    extract = s.Substring(pos1 + 5, 6);
                    if (extract.Substring(0, 3) == "0\r")
                    {
                        PlaneData.bank = "0";
                    }
                    else
                    {
                        double a = double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat);
                        Int32 b = (Convert.ToInt32(a * 57.2957));
                        if (b >= 0 && b < 10) PlaneData.bank = "+000"+ b.ToString();
                        if (b >= 10 && b < 100) PlaneData.bank = "+00" + b.ToString();
                        if (b >= 100 && b < 180) PlaneData.bank = "+0" + b.ToString();
                        if (b < 0 && b > -10) PlaneData.bank = "-000" + (b * -1).ToString();
                        if (b <= -10 && b > -100) PlaneData.bank = "-00" + (b * -1).ToString();
                        if (b <= -100 && b > -180) PlaneData.bank = "-0" + (b * -1).ToString();

                    }
                }
                catch { PlaneData.bank = "X"; }
            
            // pitchangle (radians) --> deg
            if (s.Contains("pitch"))
                try
                {
                    int pos1 = s.IndexOf("pitch="); // posnumber where altitude starts
                    extract = s.Substring(pos1 + 6, 6);
                    if (extract.Substring(0,3)== "0\r")
                    {
                        PlaneData.pitch = "0";
                    }
                    else
                    {
                        double a = double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat);
                        Int32 b = (Convert.ToInt32(a * 57.2957));
                        if (b >= 0 && b < 10) PlaneData.pitch = "+00" + b.ToString();
                        if (b >= 10 && b < 90) PlaneData.pitch = "+0" + b.ToString();
                        if (b < 0 && b > -10) PlaneData.pitch = "-00" + (b * -1).ToString();
                        if (b <= -10 && b > -90) PlaneData.pitch = "-0" + (b * -1).ToString();
                    }
                }
                catch { PlaneData.pitch = "X"; }

            // pneumatic variometer reading (m/s)
            if (s.Contains("vario"))
                try
                {
                    int pos1 = s.IndexOf("vario="); // posnumber where vario starts
                    if (s.Substring(pos1 + 6, 1) == "-") 
                        PlaneData.varioraw = s.Substring(pos1 + 6, 4);
                    else
                        PlaneData.varioraw = "+" + s.Substring(pos1 + 6, 3);
                    }
                catch { PlaneData.varioraw = "X"; }

            // electronic variometer reading (m/s)
            if (s.Contains("evario"))
                try
                {
                    int pos1 = s.IndexOf("evario="); // posnumber where string starts
                    if (s.Substring(pos1 + 7, 1) == "-")
                        PlaneData.varioelec = s.Substring(pos1 + 7, 4);
                    else
                        PlaneData.varioelec = "+" + s.Substring(pos1 + 7, 3);
                }
                catch { PlaneData.varioelec = "X"; }

            // Integrated vario (m/s)
            if (s.Contains("integrator"))
                try
                {
                    int pos1 = s.IndexOf("integrator="); // posnumber where string starts
                    if (s.Substring(pos1 + 11, 1) == "-")
                        PlaneData.variointegrated = s.Substring(pos1 + 11, 4);
                    else
                        PlaneData.variointegrated = "+" + s.Substring(pos1 + 11, 3);
                }
                catch { PlaneData.variointegrated = "X"; }

            // Gforce (G)
            if (s.Contains("gforce"))
                try
                {
                    int pos1 = s.IndexOf("gforce="); // posnumber where string starts
                    if (s.Substring(pos1 + 7, 1) == "-")
                        PlaneData.gforce = s.Substring(pos1 + 7, 4);
                    else
                        PlaneData.gforce = "+" + s.Substring(pos1 + 7, 3);
                }
                catch { PlaneData.gforce = "X"; }

            // Heading Compass
            if (s.Contains("compass")) //degrees
                try
                {
                    int pos1 = s.IndexOf("compass="); // posnumber where string starts
                    extract = s.Substring(pos1 + 8, 5);
                    double temp = double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat); // complicated mess...everyone has a regional difference in decimal.. . or ,
                    Int32 temp2 = Convert.ToInt32(Math.Round(temp));
                    if (temp2 < 10) PlaneData.kompas = "00" + temp2.ToString();
                    if (temp2 < 100 && temp2 >= 10) PlaneData.kompas = "0" + temp2.ToString();
                    if (temp2 >= 100) PlaneData.kompas = temp2.ToString();
                }
                catch { PlaneData.kompas = "X"; }
        }

        private void ShowConvertedData(planedata a)
        {
            textBoxBankdeg.Text = a.bank;
            textBoxPitch.Text = a.pitch;
            textBoxIntegrated.Text = a.variointegrated;
            textBoxVario.Text = a.varioraw;
            textBoxEvario.Text = a.varioelec;
            textBoxSpeedkmh.Text = a.speed;
            textBoxHeight.Text = a.altitudebaro;
            textBoxGforce.Text = a.gforce;
            textBoxCompass.Text = a.kompas;
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
