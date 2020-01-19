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

    public struct PLANEDATA
    {
        public int speedias;
        public int altitudebaro;
        public int bank;
        public int pitch;
        public int compass;
        public double varioraw;
        public double varioint;
        public double varioelec;
        public double gforce;
        public double yawstring;
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
        PLANEDATA glider,decoded;
        byte[] serialdata = new byte[19];
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
                    MakeGliderData(glider);
                    
                    //SendConvertedData(PlaneData);
                    DecodeData(serialdata);
                    ShowConvertedData(PlaneData);
                    SendByte2Arduino();
                    
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
                        if (a < 0) a = 0.0; //I dont want negative or too big speeds. so I limit the range
                        if (a >= 1000) a = 999.0; 
                        glider.speedias = Convert.ToInt16(a);
                    }
                }
                catch { }

            // Altitude (m or ft according to units selected)
            if (s.Contains("altitude"))
            {
                try
                {
                    int pos1 = s.IndexOf("altitude="); // posnumber where altitude starts
                    extract = s.Substring(pos1 + 9, 6);
                    int a = Convert.ToInt32((double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat))); // meters afgerond naar beneden 
                    if (a < 0) a = 0; //I dont want negative or too big altitude. so I limit the range
                    if (a >= 10000) a = 9999; 
                    glider.altitudebaro = Convert.ToInt16((double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat))); // meters afgerond naar beneden 
                  
                catch { }
            }

            // Bankangle (radians) --> deg
            if (s.Contains("bank"))
                try
                {
                    int pos1 = s.IndexOf("bank="); // posnumber where bank starts
                    extract = s.Substring(pos1 + 5, 6);
                    double a = double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat);
                    Int16 b = (Convert.ToInt16(a * -57.2957));
                    glider.bank = b;// range [-180, 180]
                }
                catch { }
            
            // pitchangle (radians) --> deg
            if (s.Contains("pitch"))
                try
                {
                    int pos1 = s.IndexOf("pitch="); // posnumber where pitch starts
                    extract = s.Substring(pos1 + 6, 6);
                    double a = double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat);
                    Int16 b = (Convert.ToInt16(a * 57.2957)); // convert from rads to degrees
                    glider.pitch = b; //range [-90, 90]
                }
                catch {  }

            // pneumatic variometer reading (m/s)
            if (s.Contains("vario"))
                try
                {
                    int pos1 = s.IndexOf("vario="); // posnumber where vario starts
                    glider.varioraw = double.Parse(s.Substring(pos1 + 6, 5), CultureInfo.InvariantCulture.NumberFormat);
                    if (glider.varioraw >= 10) glider.varioraw = 9.9; // I want to linit the data
                    if (glider.varioraw <= -10) glider.varioraw = -9.9; // I want to linit the data
                }
                catch { }

            // electronic variometer reading (m/s)
            if (s.Contains("evario"))
                try
                {
                    int pos1 = s.IndexOf("evario="); // posnumber where string starts
                    glider.varioelec= double.Parse(s.Substring(pos1 + 7, 5), CultureInfo.InvariantCulture.NumberFormat);
                    if (glider.varioelec >= 10) glider.varioelec = 9.9;// I want to linit the data
                    if (glider.varioelec <= -10) glider.varioelec = -9.9;// I want to linit the data
                }
                catch { }

            // Integrated vario (m/s)
            if (s.Contains("integrator"))
                try
                {
                    int pos1 = s.IndexOf("integrator="); // posnumber where string starts
                    glider.varioint = double.Parse(s.Substring(pos1 + 11, 5), CultureInfo.InvariantCulture.NumberFormat); //-1.23 or 1.234
                    if (glider.varioint >= 10) glider.varioint = 9.9;// I want to linit the data
                    if (glider.varioint <= -10) glider.varioint = -9.9;// I want to linit the data
                }         
                catch { PlaneData.variointegrated = "X"; }

            // Gforce (G)
            if (s.Contains("gforce"))
                try
                {
                    int pos1 = s.IndexOf("gforce="); // posnumber where string starts
                    glider.gforce = double.Parse(s.Substring(pos1 + 7, 4), CultureInfo.InvariantCulture.NumberFormat);
                    if (glider.gforce >= 10) glider.gforce = 9.9;// I want to linit the data
                    if (glider.gforce <= -10) glider.gforce = -9.9;// I want to linit the data
                }
                catch {}

            // Heading Compass
            if (s.Contains("compass")) //degrees
                try
                {
                    int pos1 = s.IndexOf("compass="); // posnumber where string starts
                    extract = s.Substring(pos1 + 8, 5);
                    double temp = double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat); 
                    Int32 temp2 = Convert.ToInt32(Math.Round(temp));
                    glider.compass = Convert.ToInt16(Math.Round(temp)); //range [0, 359]
                }
                catch { }
            
            // Gforce (G)
            if (s.Contains("yawstringangle"))
                try
                {
                    int pos1 = s.IndexOf("yawstringangle="); // posnumber where string starts
                    glider.yawstring = double.Parse(s.Substring(pos1 + 15, 4), CultureInfo.InvariantCulture.NumberFormat);
                }
                catch { }

        }

        private void MakeGliderData(PLANEDATA a)
        {
            //range of byte = 0-255. So if the data exceeds this we are fucked. we-are-fucked. this will not work.. yes it will!
            // https://www.thethingsnetwork.org/docs/devices/bytes.html
            //*****************************************************
            //Encode:
            //MyByte[0] = Convert.ToByte((MyVal >> 8) & 0x00FF);
            //MyByte[1] = Convert.ToByte(MyVal & 0x00FF);
            //*****************************************************
            // Decode: 
            // int myVal = ((int)(MyByte[0]) << 8)+MyByte[1]
            int temp = 0;
            serialdata[0] = 255; // for decoding in Arduino to check where to begin;
            
            //Altitudebaro [1] [2] Range [0,9999]

            serialdata[1] = Convert.ToByte((a.altitudebaro >> 8) & 0x00FF);
            serialdata[2] = Convert.ToByte(a.altitudebaro & 0x00FF); //decode in arduino
            //Speed [3] [4] Range [0, 999]
            serialdata[3] = Convert.ToByte((a.speedias >> 8) & 0x00FF); 
            serialdata[4] = Convert.ToByte(a.speedias & 0x00FF); //
            //Compass [5] [6] Range [0,360]
            serialdata[5] = Convert.ToByte((a.compass >> 8) & 0x00FF);
            serialdata[6] = Convert.ToByte(a.compass & 0x00FF); 
            
            //Pitch [7] [8] Range [-90,90]
            temp = Convert.ToInt16(Math.Round(a.pitch + 90.0)); //decode in Arduino!
            serialdata[7] = Convert.ToByte((temp >> 8) & 0x00FF);
            serialdata[8] = Convert.ToByte(temp & 0x00FF);
            //Bank [9] [10] Range [-180,180]
            temp = Convert.ToInt16(Math.Round(a.bank + 180.0)); //decode in Arduino!
            serialdata[9] = Convert.ToByte((temp >> 8) & 0x00FF);
            serialdata[10] = Convert.ToByte(temp & 0x00FF);
            
            //VarioRaw [11] [12] // Range [-99.9, 99.9]-->[-999,999] decode in Arduino!
            temp = Convert.ToInt16(Math.Round((a.varioraw + 99.9) * 10));
            serialdata[11] = Convert.ToByte((temp >> 8) & 0x00FF);
            serialdata[12] = Convert.ToByte(temp & 0x00FF);
            //Varioelec [13] [14] // Range [-99.9, 99.9]-->[-999,999] decode in Arduino!
            temp = Convert.ToInt16(Math.Round((a.varioelec + 99.9) * 10));
            serialdata[13] = Convert.ToByte((temp >> 8) & 0x00FF);
            serialdata[14] = Convert.ToByte(temp & 0x00FF);
            //Varioint [15] [16] // Range [-99.9, 99.9]-->[-999,999] decode in Arduino!
            temp = Convert.ToInt16(Math.Round((a.varioint + 99.9) * 10));
            serialdata[15] = Convert.ToByte((temp >> 8) & 0x00FF);
            serialdata[16] = Convert.ToByte(temp & 0x00FF);
            //GForce[17] [18] Range [-9.9,9,9] -->[-99,99] decode in Arduino!
            temp = Convert.ToInt16(Math.Round((a.gforce + 9.9) * 10));
            serialdata[17] = Convert.ToByte((temp >> 8) & 0x00FF);
            serialdata[18] = Convert.ToByte(temp & 0x00FF);
            
            /*
            ToDo!
            //Yawstring [19] [20] Range [-0.09, 0.09]
            temp = Convert.ToInt16(Math.Round((a.yawstring) * 100)+10.0);
            serialdata[19] = Convert.ToByte((temp >> 8) & 0x00FF);
            serialdata[20] = Convert.ToByte(temp & 0x00FF);
            */
            // extra info on logic behind this: A Byte cannot hold doubles. it is an unsigned int. 
            //-1.23  * 10= -12.3 --> rounded = -12 --> div 10 in arduino = -1.2
            //1.234 * 10 = 12.34 --> rounded = 12 --> div 10 in arduino = 1.2
            //20.45 * 10 = 204.5 --> rounded = 205 --> div 10 in arduino = 20.5
            //-20.4 *10 = -204.0 --> rounded = -204 --> div 10 in arduino = 20.4
         }
        private void DecodeData(byte[] b)
        {
            decoded.altitudebaro = b[1] << 8 | b[2];
            decoded.speedias = b[3] << 8 | b[4];
            decoded.compass = b[5] << 8 | b[6];
            decoded.pitch = (b[7] << 8 | b[8]) - 90; // Ik weet niet hoe ik de negatieve waarden kan doorsturen en converteren. Vandaar de optelling van de max negatieve waarde.
            decoded.bank = (b[9] << 8 | b[10]) - 180;
            decoded.varioraw = Math.Round(((b[11] << 8 | b[12]) / 10.0) - 99.9, 1);
            decoded.varioelec = Math.Round(((b[13] << 8 | b[14]) / 10.0) - 99.9, 1);
            decoded.varioint = Math.Round(((b[15] << 8 | b[16]) / 10.0) - 99.9, 1);
            decoded.gforce = Math.Round(((b[17] << 8 | b[18]) / 10.0) - 9.9);
        }
           
  
         private void SendByte2Arduino()    
         {
             if (arduino) // if connected to arduino
             {
                 port.Write(serialdata,0,19); // send it
             }
         }

        private void ShowConvertedData(planedata a)
        {
            textBoxDecodeAlt.Text = decoded.altitudebaro.ToString();
            textBoxDecodeBank.Text = decoded.bank.ToString();
            textBoxDecodeCompass.Text = decoded.compass.ToString();
            textBoxDecodeGforce.Text = decoded.gforce.ToString();
            textboxDecodeSpeed.Text = decoded.speedias.ToString();
            textBoxDecodevarElec.Text = decoded.varioraw.ToString();
            textBoxDecodeVarint.Text = decoded.varioint.ToString();
            textBoxDecodeVarraw.Text = decoded.varioraw.ToString();
            textBoxDecodePitch.Text = decoded.pitch.ToString();
           // textBoxyawstring.Text = decoded.yawstring.ToString();
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
