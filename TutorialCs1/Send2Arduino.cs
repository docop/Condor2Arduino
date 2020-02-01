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
    class Send2Arduino
    {
       public SerialPort port;
       public bool arduino = false;
       public byte[] serialdata = new byte[21];
        
        
       public bool ArduinoConnect(String name,Int32 baud)
        {
            try
            {
                port = new SerialPort(name, baud, Parity.None, 8, StopBits.One);
                port.Open();
                arduino = true;
                return true;
            }
            catch (Exception f) { return false; }
        }
        
        public bool ArduinoDisconnect()
        {
            try 
            {
                port.Close();
                port.Dispose();
                arduino = false;
                return true;
            }
            catch 
            { 
            return false;
            }
        }
        
        public bool SendByte2Arduino(byte[] data, Int32 length)
        {
            try
            {
                if (arduino) // if connected to arduino
                {
                    port.Write(data, 0, length); // send it
                    return true;
                }
                else 
                    return false;
            }
            catch { return false; }
        }
    }//end class
}//end namespace
