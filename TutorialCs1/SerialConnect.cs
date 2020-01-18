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
    class SerialConnect
    {
        public UdpClient Ontvanger = null;
        public IPEndPoint listenEndPoint=null;
        public byte[] receivedData;
        public bool connected = false;
        
        public void CondorConnect(Int32 poortnaam) // I have put the asynchronous threading in the calling as a background worker
        {
            try
            {
                if (!connected)
                {
                    Ontvanger = new UdpClient(poortnaam); //create new client using port 'poort'
                    Ontvanger.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);
                    listenEndPoint = new IPEndPoint(IPAddress.Any, poortnaam);
                    connected = true;
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
                connected = false;
            }
            catch { }
        }

        public string GetCondorData() // I have put the threading in the calling
        {
            try
            {
                if (Ontvanger!=null)
                receivedData = Ontvanger.Receive(ref listenEndPoint); 
                return (Encoding.ASCII.GetString(receivedData));
            }
            catch
            {
                return "-1";
            }
        }

        
   
    } // end class
} // end namespace

