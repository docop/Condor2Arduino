using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Bovenstaande is bij mij standaard gevuld als ik een nieuw project aanmaak.
// Onderstaande 2 using regels moet je toevoegen. (conform http://www.prepar3d.com/SDKv4/LearningCenter.php)
// *************************************************************************
// Documentatie van SDK ===> Of online via bovenstaande URL
// Of via p3D (De SIM zelf) BOVENIN MENUBALK Help - Learning Center
// *************************************************************************
using LockheedMartin.Prepar3D.SimConnect;
using System.Runtime.InteropServices;
// Als je een error hebt bij build: controleer dan je References - Add - Browse --> p3d sdk directory
// bij mij is dat e:\Program Files\Lockheed Martin\Prepar3D v4 SDK 4.5.13.32097\lib\SimConnect\managed\LockheedMartin.Prepar3D.SimConnect.dll
// verder heb ik in de target build X64 gezet ipv default X86
// Properties - Build - Platform Target

// *************************************************************************
// V3.1:
// SPD routine gecorrigeerd
// begin gemaakt met koppeling naar PMDG - 737
// *************************************************************************


namespace P3DConnect 
{
    public partial class Form1 : Form // automatisch aangemaakt als je nieuwe winform project aanmaakt
    {


        const int WM_USER_SIMCONNECT = 0x0402; // User-defined win32 event. Dit lijkt altijd zo te moeten staan. Ik weet niet wat het is en waarom.
        SimConnect simconnect = null; // SimConnect object
       
        enum DEFINITIONS: int
        {
            Struct1,
            DEFINITION_5 // right brake
           
        }

        enum DATA_REQUESTS
        {
            REQUEST_1
        };

        public enum hSimconnect : int
        {
            group1
        }
        
        private enum EVENT_ID
       {
            EVENT_SIMSTART, // Trigger als sim gestopt was en weer 'loopt' (bijv. na kijken op de map)
            EVENT_SIMSTOP, // Trigger als de Sim stopt
            EVENT_PAUSED, // Zelf toegevoegd om te kijken of ik een EVENT kon toevoegen.
            EVENT_UNPAUSED, // Zelf toegevoegd om te kijken of ik een EVENT kon toevoegen.EVENT_FLAPSup,
            EVENT_FLAPSup, 
            EVENT_FLAPSdn,
            EVENT_COM1co,
            EVENT_COM1SBset,
            EVENT_XPONDer,
            EVENT_NAV1OBS,
            EVENT_APon,
            EVENT_APoff,
            EVENT_LIGHTSset,
            EVENT_setGEAR_UP,
            EVENT_setGEAR_DOWN,
            EVENT_setPARKING_BRAKES,
            EVENT_setGEAR_TOGGLE,
            EVENT_setGEAR_SET,
            EVENT_setHDG,
            EVENT_setSPD
        }

        // this is how you declare a data structure so that
        // simconnect knows how to fill it/read it.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct Struct1 
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]// this is how you declare a fixed size string
            public string Titlex;
            public float flapsx;
            public float altitudex;
            public float IASx;
            public float com1x;
            public float com1sbx;
            public float xponderx;
            public float nav1obsx;
            public float aponoffx;
            public float Gear_Leftx;
            public float Gear_Nosex;
            public float Gear_Rightx;
            public float HDGset;
        };

       

        // Variables:
       string flaps;
        string altitude;
        string ias;
        string com1;
        string com1sb;
        string xponder;
        string nav1obs;
        string aponoff;
        string titleout;
        string Gear_Left;
        string Gear_Nose;
        string Gear_Right;
        string hdg;
        uint val;
        public Form1()
        {
            InitializeComponent();
            setButtons(true, false, false); // truukje om de knoppen aan/uit te zetten.
        }
        
        // Simconnect client will send a win32 message when there is 
        // a packet to process. ReceiveMessage must be called to
        // trigger the events. This model keeps simconnect processing on the main thread.
        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_USER_SIMCONNECT)
            {
                if (simconnect != null) // zolang er een simconnect instantie bestaat (=je verbonden bent) doe ...
                {
                    simconnect.ReceiveMessage();
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        private void setButtons(bool bConnect, bool bGet, bool bDisconnect) //zet knoppen aan of uit. handige routine. vergeet niet om andere knoppen als je die toevoegt hier bij te zetten. 
        { // connected false true true
            buttonConnect.Enabled = bConnect;
            buttonDisconnect.Enabled = bDisconnect;
            timer1.Enabled = bGet;
            buttonAP.Enabled = bGet;
            buttonFlaps.Enabled = bGet;
            buttonLights.Enabled = bGet;
            buttonSet.Enabled = bGet;
        }

        private void closeConnection() // sluit de connectie
        {
            if (simconnect != null) //als verbonden is ja dan != betekent not =
            {
                // Unsubscribe from all the system events 
                simconnect.UnsubscribeFromSystemEvent(EVENT_ID.EVENT_SIMSTART);
                simconnect.UnsubscribeFromSystemEvent(EVENT_ID.EVENT_SIMSTOP);
                simconnect.UnsubscribeFromSystemEvent(EVENT_ID.EVENT_PAUSED); 
                simconnect.UnsubscribeFromSystemEvent(EVENT_ID.EVENT_UNPAUSED); 

                // Dispose serves the same purpose as SimConnect_Close()
                simconnect.Dispose(); // gooi de simconnect instantie weg. nette manier van coderen. Als je iets niet gebruikt: opruimen.
                simconnect = null; // misschien overbodig, maar zet de simconnect instantie op null.(leeg)
                labelStatus.Text="Connection closed";
            }
        }

        // Set up all the SimConnect related data definitions and event handlers
        private void initDataRequest()
            // deze functie zorgt er voor dat je programma de juiste data en events ontvangt.
            // je zegt in feite hee simconnect ik wil een signaal krijgen x, y , z gebeurt en/of ik wil data x, y en Z lezen
        {
            try
            {

                simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(simconnect_OnRecvOpen); // listen to connect msgs 
                simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(simconnect_OnRecvQuit); // listen to quit msgs.
                simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(simconnect_OnRecvException); // listen to exceptions
               
                // define a data structure. AddtoDataDefinition.. Dit is een lastig verhaal die ik nog steeds niet goed begrijp.
                // Soms krijg je geen of ongewenste data.. Uit ervaring weet ik inmiddels dat de volgorde van de data er toe doet.
                // Voorbeeld hieronder: Eerst title, dan flaps.. net zoals je in de struct Struct1 heb benoemd.
                // en nee dit begrijp ik nog niet ToDo: Uitzoeken.

                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Title", "", SIMCONNECT_DATATYPE.STRING256, 0F, 8);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "TRAILING EDGE FLAPS LEFT PERCENT", "degrees", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Indicated Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "Airspeed Indicated", "knots", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "COM ACTIVE FREQUENCY:1", "MHz", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "COM STANDBY FREQUENCY:1", "MHz", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "TRANSPONDER CODE:1", "Enum", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "NAV OBS:1", "degrees", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "AUTOPILOT MASTER", "Bool", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "GEAR LEFT POSITION", "percent", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "GEAR CENTER POSITION", "percent", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "GEAR RIGHT POSITION", "percent", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(DEFINITIONS.Struct1, "AUTOPILOT HEADING LOCK DIR", "degrees", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED);
                
                simconnect.AddToDataDefinition(DEFINITIONS.DEFINITION_5, "GEAR CENTER POSITION", "position", SIMCONNECT_DATATYPE.FLOAT32, 0F, SimConnect.SIMCONNECT_UNUSED); // vergeet deze even. ToDo: uitwerken
                
                // je registreert het... 
                simconnect.RegisterDataDefineStruct<Struct1>(DEFINITIONS.Struct1);
                // en je verzoekt om data.. Is dit goed Nederlands?
                simconnect.RequestDataOnSimObject(DATA_REQUESTS.REQUEST_1, DEFINITIONS.Struct1, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_PERIOD.VISUAL_FRAME, 0, 0, 0, 0);
		       // Dus: Add, Register, Request

                // Subscribe to system events 
                simconnect.SubscribeToSystemEvent(EVENT_ID.EVENT_SIMSTART, "SimStart"); // Zie hfd. Simconnect API- SubscribeToSystemEvent - parameters
                simconnect.SubscribeToSystemEvent(EVENT_ID.EVENT_SIMSTOP, "SimStop");
                simconnect.SubscribeToSystemEvent(EVENT_ID.EVENT_PAUSED, "Paused"); // toegevoegd aan voorbeeld als test
                simconnect.SubscribeToSystemEvent(EVENT_ID.EVENT_UNPAUSED, "Unpaused"); // toegevoegd aan voorbeeld als test
         
                // Map User EVENTS to Flightsim Events
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_FLAPSup, "FLAPS_UP");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_FLAPSdn, "FLAPS_DOWN");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_APon, "AUTOPILOT_ON");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_APoff, "AUTOPILOT_OFF");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_LIGHTSset, "ALL_LIGHTS_TOGGLE");

                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_COM1co, "COM_STBY_RADIO_SWAP"); //COM_STBY_RADIO_SWAP 
               
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_COM1SBset, "COM_STBY_RADIO_SET"); //Sets COM frequency (BCD Hz) 123.45 = in BCD: 0x12345 hoe converteer je van string naar BCD? ik weet het niet 
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_XPONDer, "XPNDR_SET");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_NAV1OBS, "VOR1_SET");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_setGEAR_UP, "GEAR_UP");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_setGEAR_DOWN, "GEAR_DOWN");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_setPARKING_BRAKES, "PARKING_BRAKES");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_setGEAR_TOGGLE, "GEAR_TOGGLE");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_setGEAR_SET, "GEAR_SET");
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_setHDG, "HEADING_BUG_SET"); // Set heading hold reference bug (degrees)
                simconnect.MapClientEventToSimEvent(EVENT_ID.EVENT_setSPD, "AP_SPD_VAR_SET"); //AP_SPD_VAR_SET Sets airspeed reference (in knots) 
                // listen to events 
                simconnect.OnRecvEvent += new SimConnect.RecvEventEventHandler(simconnect_OnRecvEvent);
                // catch a simobject data request
                simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(simconnect_OnRecvSimobjectDataBytype);
            }
            catch (COMException ex)
            {
                labelStatus.Text=ex.Message;
            }
        }
        
        void simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            labelStatus.Text="Connected to Prepar3D";
        }


        void simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data) // The case where the user closes Prepar3D
        {
            labelStatus.Text="Prepar3D has exited";
            closeConnection();
        }

        void simconnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            labelStatus.Text="Exception received: " + data.dwException;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) // The case where the user closes the client
        {
            closeConnection();
        }
        
        void simconnect_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT recEvent) //Ik ontvang een Event. Deze functie bepaalt wat er mee moet gebeuren
        {
            switch (recEvent.uEventID)
            {
                case (uint)EVENT_ID.EVENT_SIMSTART: // Eventid 0

                    labelStatus.Text="Sim running";
                    break;

                case (uint)EVENT_ID.EVENT_SIMSTOP: // Eventid 1

                    labelStatus.Text="Sim stopped";
                    break;

                case (uint)EVENT_ID.EVENT_PAUSED: // Eventid 3
                    labelStatus.Text="Pause ON"; // 
                    break;

                case (uint)EVENT_ID.EVENT_UNPAUSED: // Eventid 4
                    labelStatus.Text="Pause OFF"; // 
                    break;
            }
        } 
        void simconnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)  // Ik ontvang Data
        {
            switch ((DATA_REQUESTS)data.dwRequestID)
            {
                case DATA_REQUESTS.REQUEST_1:
                    Struct1 s1 = (Struct1)data.dwData[0];
                    flaps = String.Format("{0:000}", s1.flapsx);
					altitude = String.Format("{0:00000}", s1.altitudex);
					ias = String.Format("{0:000}", s1.IASx);
					com1 = String.Format("{0:00.000}", s1.com1x);
					com1sb = String.Format("{0:00.000}", s1.com1sbx);
					xponder = String.Format("{0:0000}", s1.xponderx);
                    aponoff = String.Format("{0:0}", s1.aponoffx);
                    nav1obs = String.Format("{0:000}", s1.nav1obsx);
					titleout = s1.Titlex;
                   //geardata
                    Gear_Left = String.Format("{0:000}", s1.Gear_Leftx);
                    Gear_Nose = String.Format("{0:000}", s1.Gear_Nosex);
                    Gear_Right = String.Format("{0:000}", s1.Gear_Rightx);
                    hdg = String.Format("{0:000}", s1.HDGset);

                    //show data
                    textBoxFlaps.Text=flaps;
                    textBoxAlt.Text=altitude;
                    textBoxIAS.Text=ias;
                    textBoxCom.Text=com1;
                    textBoxNose.Text = Gear_Nose;
                    textBoxLeft.Text = Gear_Left;
                    textBoxRight.Text = Gear_Right;
                    textBoxHDG.Text = hdg;
                    labelTitle.Text = titleout;
                    break;

                default:
                    labelStatus.Text= "Unknown DATA ID received: " + data.dwRequestID;
                    break;
            }
        }

 
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (simconnect == null) // als er nog geen simconnect instantie of object is dan
            {
                try
                {
                    // the constructor is similar to SimConnect_Open in the native API
                    simconnect = new SimConnect("P3D Connectie tests", this.Handle, WM_USER_SIMCONNECT, null, 0);
                    setButtons(false, true, true); // connectie gelukt. zet de andere knoppen aan
                    initDataRequest(); // instellen abonnementen op de simconnect service (zoals data, events)

                }
                catch (COMException ex) //wat te doen als er een exception optreedt bij proberen te connecten
                {
                    labelStatus.Text= "Unable to connect to Prepar3D:\n\n" + ex.Message;
                }
            }
            else // als - dan - anders
            {
                setButtons(true, false, false); 
                labelStatus.Text= "Error - try again";
                closeConnection();
                
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            setButtons(true, false, false);
            closeConnection();
            
        }

         private void buttonFlaps_Click(object sender, EventArgs e)
        {
            if (flaps != "000")// ontvang je van de sim. zie hierboven bij simconnect_OnRecvSimobjectDataBytype 
            {
                try { simconnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.EVENT_FLAPSup, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY); }
                catch { };
            }
            else
            {
                try { simconnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.EVENT_FLAPSdn, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY); }
                catch { };
            }
        }

        private void buttonAP_Click(object sender, EventArgs e)
        {
            if (aponoff == "0") // ontvang je van de sim. zie hierboven bij simconnect_OnRecvSimobjectDataBytype 
            {
                try { simconnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.EVENT_APon, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY); }
                catch { };
            }
            if (aponoff == "1")// ontvang je van de sim. zie hierboven bij simconnect_OnRecvSimobjectDataBytype 
            {
                try { simconnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.EVENT_APoff, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY); }
                catch { };
            }
        }

        private void buttonLights_Click(object sender, EventArgs e)
        {
            try { 
                simconnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.EVENT_setGEAR_TOGGLE, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
             }
            catch { };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            simconnect.RequestDataOnSimObjectType(DATA_REQUESTS.REQUEST_1, DEFINITIONS.Struct1, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
        }

        private void buttonSet_Click(object sender, EventArgs e) // deze functie is in onderhoud.
        {
            val = uint.Parse(textBoxSetHDG.Text); // converteer de gewenste hdg naar een integer
                try 
                { 
                    simconnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.EVENT_setHDG, val, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                }
                   
        catch { };
        }

        private void buttonSetSpd_Click(object sender, EventArgs e)
        {
            val = uint.Parse(textBoxSetSpd.Text); // converteer de gewenste hdg naar een integer
            try
            {
                simconnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.EVENT_setSPD, val, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
            }
           catch { };
        }


        
        
        private void buttonCOM_Click(object sender, EventArgs e)
        {
            try
            {
                // en hoe dit moet is lastig. Je wil een string (123.45) omzetten naar een BCD16 waarde (0x12345) en die moet als uint (0x12345) naar p3d toe? prfft.
                // zelfde geldt voor NAV. Internet iets gevonden. de Bcd2Dec(uint num) routine converteert het. testen toont aan dat het werkt.. niets meer aan doen.
                // Alternatief is increment/decrement gebruiken, wat eigenlijk ook zo werkt met een draaiknop.
                decimal t = decimal.Parse(textBoxSetCom.Text)*100; // eerst x 100 om van de komma af te komen. 123,45 --> 12345
                val = Dec2Bcd(Decimal.ToUInt32(t)); //dan maak ik van de deciamal een uint en die stuur ik naar Decimal.ToUInt32()
               simconnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.EVENT_COM1SBset, val, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
            }
            catch { };
        }
        
        //Here's some code in C# if you need to convert the encoded number to non encoded, and vice-versa (original source by Arne Bartels, modified by me for .NET C#): 
        //Converts from binary coded decimal to integer 
        public static uint Bcd2Dec(uint num)
        {
            return HornerScheme(num, 0x10, 10);
        }
        // Converts from integer to binary coded decimal 
        public static uint Dec2Bcd(uint num)
        {
            return HornerScheme(num, 10, 0x10);
        }
        static private uint HornerScheme(uint Num, uint Divider, uint Factor)
        {
            uint Remainder = 0, Quotient = 0, Result = 0;
            Remainder = Num % Divider; Quotient = Num / Divider;
            if (!(Quotient == 0 && Remainder == 0))
                Result += HornerScheme(Quotient, Divider, Factor) * Factor + Remainder;
            return Result;
        }
        //Hope this helps,Etienne
    }
}




  




/*DIV ERROR CODES etc
 * 
 * 
enum SIMCONNECT_EXCEPTION{
  SIMCONNECT_EXCEPTION_NONE = 0,
  SIMCONNECT_EXCEPTION_ERROR = 1,
  SIMCONNECT_EXCEPTION_SIZE_MISMATCH = 2,
  SIMCONNECT_EXCEPTION_UNRECOGNIZED_ID = 3,
  SIMCONNECT_EXCEPTION_UNOPENED = 4,
  SIMCONNECT_EXCEPTION_VERSION_MISMATCH = 5,
  SIMCONNECT_EXCEPTION_TOO_MANY_GROUPS = 6,
  SIMCONNECT_EXCEPTION_NAME_UNRECOGNIZED = 7,
  SIMCONNECT_EXCEPTION_TOO_MANY_EVENT_NAMES = 8,
  SIMCONNECT_EXCEPTION_EVENT_ID_DUPLICATE = 9,
  SIMCONNECT_EXCEPTION_TOO_MANY_MAPS = 10,
  SIMCONNECT_EXCEPTION_TOO_MANY_OBJECTS = 11,
  SIMCONNECT_EXCEPTION_TOO_MANY_REQUESTS = 12,
  SIMCONNECT_EXCEPTION_WEATHER_INVALID_PORT = 13,
  SIMCONNECT_EXCEPTION_WEATHER_INVALID_METAR = 14,
  SIMCONNECT_EXCEPTION_WEATHER_UNABLE_TO_GET_OBSERVATION = 15,
  SIMCONNECT_EXCEPTION_WEATHER_UNABLE_TO_CREATE_STATION = 16,
  SIMCONNECT_EXCEPTION_WEATHER_UNABLE_TO_REMOVE_STATION = 17,
  SIMCONNECT_EXCEPTION_INVALID_DATA_TYPE = 18,
  SIMCONNECT_EXCEPTION_INVALID_DATA_SIZE = 19,
  SIMCONNECT_EXCEPTION_DATA_ERROR = 20,
  SIMCONNECT_EXCEPTION_INVALID_ARRAY = 21,
  SIMCONNECT_EXCEPTION_CREATE_OBJECT_FAILED = 22,
  SIMCONNECT_EXCEPTION_LOAD_FLIGHTPLAN_FAILED = 23,
  SIMCONNECT_EXCEPTION_OPERATION_INVALID_FOR_OJBECT_TYPE = 24,
  SIMCONNECT_EXCEPTION_ILLEGAL_OPERATION = 25,
  SIMCONNECT_EXCEPTION_ALREADY_SUBSCRIBED = 26,
  SIMCONNECT_EXCEPTION_INVALID_ENUM = 27,
  SIMCONNECT_EXCEPTION_DEFINITION_ERROR = 28,
  SIMCONNECT_EXCEPTION_DUPLICATE_ID = 29,
  SIMCONNECT_EXCEPTION_DATUM_ID = 30,
  SIMCONNECT_EXCEPTION_OUT_OF_BOUNDS = 31,
  SIMCONNECT_EXCEPTION_ALREADY_CREATED = 32,
  SIMCONNECT_EXCEPTION_OBJECT_OUTSIDE_REALITY_BUBBLE = 33,
  SIMCONNECT_EXCEPTION_OBJECT_CONTAINER = 34,
  SIMCONNECT_EXCEPTION_OBJECT_AI = 35,
  SIMCONNECT_EXCEPTION_OBJECT_ATC = 36,
  SIMCONNECT_EXCEPTION_OBJECT_SCHEDULE = 37,
 
 * 
 * 
 * 
  enum SIMCONNECT_PERIOD{
  SIMCONNECT_PERIOD_NEVER,
  SIMCONNECT_PERIOD_ONCE,
  SIMCONNECT_PERIOD_VISUAL_FRAME,
  SIMCONNECT_PERIOD_SIM_FRAME,
  SIMCONNECT_PERIOD_SECOND,
 * 
 * SIMCONNECT_DATATYPE_INT32,64
  Specifies a 32 bit or 64 bit signed integer.

SIMCONNECT_DATATYPE_FLOAT32,64
  Specifies a 32 bit or 64 bit signed floating point number.

SIMCONNECT_DATATYPE_STRING8,32,64,128,256,260
  Specifies strings of the given length (8 characters to 260 characters)

SIMCONNECT_DATATYPE_STRINGV
  Specifies a variable length string.

SIMCONNECT_DATATYPE_INITPOSITION
  Specifies the SIMCONNECT_DATA_INITPOSITION structure.

SIMCONNECT_DATATYPE_MARKERSTATE
  Specifies the SIMCONNECT_DATA_MARKERSTATE structure.

SIMCONNECT_DATATYPE_WAYPOINT
  Specifies the SIMCONNECT_DATA_WAYPOINT structure.

SIMCONNECT_DATATYPE_LATLONALT
  Specifies the SIMCONNECT_DATA_LATLONALT structure.

SIMCONNECT_DATATYPE_XYZ
  Specifies the SIMCONNECT_DATA_XYZ structure.

 * 
 Uit een vorig leven:
 * // laatste keer dat ik het opschrijf: Heeft me weer 1 dag gekost:
    // VOLGORDE is BELANGRIJK EN MOET OVEREENKOMEN MET ADDTODEFINITION VOLGORDE 
    // EN OOK!!!!!! bools werken niet! DUS int32 voor bool en niet een bool!!!
 * 
 * 
 * handig?
 * 			SimConnect_SetDataOnSimObject(hSimConnect, DEFINITION_4, SIMCONNECT_OBJECT_ID_USER, 0, 0, sizeof(gear_failpos), &gear_failpos);
            SimConnect_SetDataOnSimObject(hSimConnect, DEFINITION_5, SIMCONNECT_OBJECT_ID_USER, 0, 0, sizeof(speed_set), &speed_set);
																											
 * 
 * // simconnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENT_ID.EVENT_setHDG, 270, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    // wat ik echter wil is dat de heading in de heading set van de AP wordt gezet. en dat lukt me nog niet
                    // just ignore!


                    //simconnect.SetDataOnSimObject(DEFINITIONS.DEFINITION_5, SimConnect.SIMCONNECT_OBJECT_ID_USER, 0, val); // hier wordt de waarde van val gezet in DEFINITION_5 en die is gemapt aan.. voor test .. aha nosegear.
                    //simconnect.SetSystemState("DialogMode", 1, 0,"Off");
                    //simconnect.SetSystemState("DialogMode", 0, 0,"On");
 * 
*/