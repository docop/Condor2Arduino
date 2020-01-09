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
    public class SerialConnect
    {
        public SerialConnect()
        {
            
        UdpClient Ontvanger;
        IPEndPoint listenEndPoint;
        byte[] receivedData;
        int poort;
        SerialPort port;
        bool connected = false;
        bool arduino = false;
        
        
        //

        }
    }
}
