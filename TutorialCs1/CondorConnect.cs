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
    class CondorConnect
    {
        public struct PLANEDATA // A handy variable to store all relevant plane data into.
        {
            public string raw;
            public bool check;
            public int speedias;
            public int altitudebaro;
            public int bank;
            public int pitch;
            public int compass;
            public int yawstring;
            public double varioraw;
            public double varioint;
            public double varioelec;
            public double gforce;
            public byte[] serialdata;
        }
        
        public PLANEDATA converted;
        
        public double rad2deg = 57.2957; // to convert radians into degrees
        
        public UdpClient Ontvanger = null;
        public IPEndPoint listenEndPoint=null;
        public byte[] receivedData;
        public bool condorconnected = false;
        
        public void CondorConnector(Int32 poortnaam) // I have put the asynchronous threading in the calling as a background worker
        {
            try
            {
                if (!condorconnected)
                {
                    Ontvanger = new UdpClient(poortnaam); //create new client using port 'poort'
                    Ontvanger.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);
                    listenEndPoint = new IPEndPoint(IPAddress.Any, poortnaam);
                    condorconnected = true;
                    // important!
                    converted.serialdata = new byte[21];
                    // **********************************
                }
                
            }
            catch { } // no exception catch coded. 
        }
        
        public void CondorDisConnect() // I have put the asynchronous threading in the calling as a background worker
        {
            try
            {
                Ontvanger.Close();
                Ontvanger = null;
                listenEndPoint = null;
                condorconnected = false;
            }
            catch { }
        }

        public PLANEDATA GetCondorData() // I have put the asynchronous threading in the calling!
        {
            string extract = "";
            string s = "";
            int tempi = 0;
            int pos1;
            double temp;

            try
            {
                if (Ontvanger != null)
                {
                    receivedData = Ontvanger.Receive(ref listenEndPoint);
                    s = (Encoding.ASCII.GetString(receivedData));
                    converted.raw = s;
                    converted.serialdata[0]=255;
                    // airspeed incl wind m/s --> km/h

                    // Altitude (m or ft according to units selected)
                    if (s.Contains("altitude"))
                    {
                        pos1 = s.IndexOf("altitude="); // posnumber where altitude starts
                        extract = s.Substring(pos1 + 9, 6);
                        int a = Convert.ToInt16((double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat))); // meters afgerond naar beneden 
                        if (a <= 0) a = 0; //I dont want negative or too big altitude. so I limit the range
                        if (a >= 10000) a = 9999;
                        converted.altitudebaro = a;
                        //Altitudebaro [1][2] Range [0,9999]
                        converted.serialdata[1] = Convert.ToByte((converted.altitudebaro >> 8) & 0x00FF);
                        converted.serialdata[2] = Convert.ToByte(converted.altitudebaro & 0x00FF); //decode in arduino
                    } 
                    if (s.Contains("airspeed"))
                    {
                        pos1 = s.IndexOf("airspeed="); // posnumber where airspeed starts
                        extract = s.Substring(pos1 + 9, 4);
                        temp = (double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat)) * 3.6; // in km/h
                        if (temp < 0) temp = 0.0; //I dont want negative or too big speeds. so I limit the range
                        if (temp >= 1000) temp = 999.0;
                        converted.speedias = Convert.ToInt16(temp);
                        //Speed [3][4] Range [0, 999]
                        converted.serialdata[3] = Convert.ToByte((converted.speedias >> 8) & 0x00FF);
                        converted.serialdata[4] = Convert.ToByte(converted.speedias & 0x00FF);
                        
                    }
                    // Heading Compass
                    if (s.Contains("compass")) //degrees
                    {
                        pos1 = s.IndexOf("compass="); // posnumber where string starts
                        extract = s.Substring(pos1 + 8, 5);
                        temp = double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat);
                        converted.compass = Convert.ToInt16(Math.Round(temp)); //range [0, 359]
                        //Compass [5] [6] Range [0,360]
                        converted.serialdata[5] = Convert.ToByte((converted.compass >> 8) & 0x00FF);
                        converted.serialdata[6] = Convert.ToByte(converted.compass & 0x00FF);
                    }
                    // pitchangle (radians) --> deg
                    if (s.Contains("pitch"))
                    {
                        pos1 = s.IndexOf("pitch="); // posnumber where pitch starts
                        extract = s.Substring(pos1 + 6, 6);
                        temp = double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat);
                        Int16 b = (Convert.ToInt16(temp * rad2deg)); // convert from rads to degrees
                        converted.pitch = b; //range [-90, 90]
                        //Pitch [7][8] Range [-90,90]
                        tempi = Convert.ToInt16(Math.Round(converted.pitch + 90.0)); //decode in Arduino!
                        converted.serialdata[7] = Convert.ToByte((tempi >> 8) & 0x00FF);
                        converted.serialdata[8] = Convert.ToByte(tempi & 0x00FF);
                    }
                    // Bankangle (radians) --> deg
                    if (s.Contains("bank"))
                    {
                        pos1 = s.IndexOf("bank="); // posnumber where bank starts
                        extract = s.Substring(pos1 + 5, 6);
                        temp = double.Parse(extract, CultureInfo.InvariantCulture.NumberFormat);
                        Int16 b = (Convert.ToInt16(temp * -1 * rad2deg)); // I want leftbank to be negative value
                        converted.bank = b;// range [-180, 180]
                        //Bank [9][10] Range [-180,180]
                        tempi = Convert.ToInt16(Math.Round(converted.bank + 180.0)); //decode in Arduino!
                        converted.serialdata[9] = Convert.ToByte((tempi >> 8) & 0x00FF);
                        converted.serialdata[10] = Convert.ToByte(tempi & 0x00FF);
                    }
                    
                    // pneumatic variometer reading (m/s)
                    if (s.Contains("vario"))
                    {
                        pos1 = s.IndexOf("vario="); // posnumber where vario starts
                        converted.varioraw = double.Parse(s.Substring(pos1 + 6, 5), CultureInfo.InvariantCulture.NumberFormat);
                        if (converted.varioraw >= 10) converted.varioraw = 9.9; // I want to linit the data
                        if (converted.varioraw <= -10) converted.varioraw = -9.9; // I want to linit the data
                        //VarioRaw [11][12] // Range [-99.9, 99.9]-->[-999,999] decode in Arduino!
                        tempi = Convert.ToInt16(Math.Round((converted.varioraw + 100.0) * 10));
                        converted.serialdata[11] = Convert.ToByte((tempi >> 8) & 0x00FF);
                        converted.serialdata[12] = Convert.ToByte(tempi & 0x00FF);
                    }
                    // electronic variometer reading (m/s)
                    if (s.Contains("evario"))
                    {
                        pos1 = s.IndexOf("evario="); // posnumber where string starts
                        converted.varioelec = double.Parse(s.Substring(pos1 + 7, 5), CultureInfo.InvariantCulture.NumberFormat);
                        if (converted.varioelec >= 10) converted.varioelec = 9.9;// I want to linit the data
                        if (converted.varioelec <= -10) converted.varioelec = -9.9;// I want to linit the data
                        //Varioelec [13][14] // Range [-99.9, 99.9]-->[-999,999] decode in Arduino!
                        tempi = Convert.ToInt16(Math.Round((converted.varioelec + 100.0) * 10));
                        converted.serialdata[13] = Convert.ToByte((tempi >> 8) & 0x00FF);
                        converted.serialdata[14] = Convert.ToByte(tempi & 0x00FF);
                    }
                    // integrated vario (m/s)
                    if (s.Contains("integrator"))
                    {
                        pos1 = s.IndexOf("integrator="); // posnumber where string starts
                        converted.varioint = double.Parse(s.Substring(pos1 + 11, 5), CultureInfo.InvariantCulture.NumberFormat); //-1.23 or 1.234
                        if (converted.varioint >= 10) converted.varioint = 9.9;// I want to linit the data
                        if (converted.varioint <= -10) converted.varioint = -9.9;// I want to linit the data
                        //Varioint [15][16] // Range [-99.9, 99.9]-->[-999,999] decode in Arduino!
                        tempi = Convert.ToInt16(Math.Round((converted.varioint + 100.0) * 10));
                        converted.serialdata[15] = Convert.ToByte((tempi >> 8) & 0x00FF);
                        converted.serialdata[16] = Convert.ToByte(tempi & 0x00FF);
                    }
                    // Gforce (G)
                    if (s.Contains("gforce"))
                    {
                        pos1 = s.IndexOf("gforce="); // posnumber where string starts
                        converted.gforce = double.Parse(s.Substring(pos1 + 7, 4), CultureInfo.InvariantCulture.NumberFormat);
                        if (converted.gforce >= 10) converted.gforce = 9.9;// I want to linit the data
                        if (converted.gforce <= -10) converted.gforce = -9.9;// I want to linit the data
                        //GForce[17][18] Range [-9.9,9,9] -->[-99,99] decode in Arduino!
                        tempi = Convert.ToInt16(Math.Round((converted.gforce + 10.0) * 10));
                        converted.serialdata[17] = Convert.ToByte((tempi >> 8) & 0x00FF);
                        converted.serialdata[18] = Convert.ToByte(tempi & 0x00FF);
                    }
                   
                    // Yawstring
                    if (s.Contains("yawstringangle"))
                    {
                        pos1 = s.IndexOf("yawstringangle="); // posnumber where string starts
                        temp = double.Parse(s.Substring(pos1 + 15, 5), CultureInfo.InvariantCulture.NumberFormat);
                        converted.yawstring = (Convert.ToInt16(temp * rad2deg)); // yawstring angle in degrees (int)
                        //Yawstring [19][20] Range [-50, 50]-->[0-100]
                        tempi = Convert.ToInt16(Math.Round(converted.yawstring + 50.0)); //decode in Arduino!
                        converted.serialdata[19] = Convert.ToByte((tempi >> 8) & 0x00FF);
                        converted.serialdata[20] = Convert.ToByte(tempi & 0x00FF);
                    }
                    // extra info on logic behind this: A Byte cannot hold doubles. it is an unsigned int. 
                    // floats are multiplied to get rid of the decima: 1.45 * 100 = 145
                    // negatives are added to get rid of the negative: -5.4 --> +10 = 5.4 * 10 = 54. Decode: /10 =5.4 10 = -5.4
                    
                    converted.check = true;

                }     // end if onvanger not null
            } // end try
            catch
            {
                converted.check = false;
            }
            return converted;
        }
 
    } // end class
} // end namespace

