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
using System.Runtime.InteropServices;

namespace Condor2Arduino
{
    class SerialConnect
    {
        public UdpClient Ontvanger;
        public IPEndPoint listenEndPoint;
        public byte[] receivedData;
        public int poort;
        public SerialPort port;
        public bool connected = false;
        public bool arduino = false;

        public void CondorConnect()   
    {
            try
            {
                if (!connected)
                {
                    poort = Convert.ToInt32(textBox1.Text); //converts port string to variable  poort as int32
                    Ontvanger = new UdpClient(poort); //create new client using port 'poort'
                    Ontvanger.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);
                    listenEndPoint = new IPEndPoint(IPAddress.Any, poort);
                    connected = true;
                }
                else
                {
                    Ontvanger.Close();
                    connected = false;
                }
            }
            catch (Exception f) { } // no exception catch coded. 
        }
        
    }
}
