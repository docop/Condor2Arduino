#include <Servo.h> // link to the servo library. 
#include <AccelStepper.h> // link to the advanced Stepper library. Accelstepper is not default you need to install it.
#include <Wire.h> // needed for the liquid crystal display (LCD)
#include <LiquidCrystal_I2C.h> // LCD display
#include <TM1638.h> // library for the 8 segments display / 8 buttons, 8 led red/green 
#include <SwitecX25.h> // library for X27 stepper motor

#define motorPin1  8      // IN1 on the ULN2003 driver
#define motorPin2  9      // IN2 on the ULN2003 driver
#define motorPin3  10     // IN3 on the ULN2003 driver
#define motorPin4  11     // IN4 on the ULN2003 driver
#define Servopin 7        // Pin 7 PWM on Arduino
#define  STROBE_TM1 4     // STRB0 of board1 is om pin 4
#define  STROBE_TM2 5     // STRB1 of board 2 is on pin 5
#define  STROBE_TM3 12     // STRB1 of board 3 is on pin 12

#define  CLOCK_TM 2       // CLK of all boards is on pin 2
#define  DIO_TM 3         // DIO of all boards is on pin 3
#define TM_BRT 0x02       // set the brightness of board (0-7)

#define STEPS (315*3)

TM1638 tm1(DIO_TM, CLOCK_TM, STROBE_TM1);
TM1638 tm2(DIO_TM, CLOCK_TM, STROBE_TM2);
TM1638 tm3(DIO_TM, CLOCK_TM, STROBE_TM3);
word leds [17] = {0, 256, 768, 1792, 3840, 7936, 16128, 32512, 65280, 1, 3, 7, 15, 31, 63, 127, 255};
//https://tronixstuff.com/2012/03/11/arduino-and-tm1638-led-display-modules/

/*Set the display (segments and LEDs) active or off and intensity (range from 0-7).
  setupDisplay(boolean active, byte intensity)

  Set a single display at pos (starting at 0) to a digit (left to right)
  setDisplayDigit(byte digit, byte pos, boolean dot, const byte numberFont[] = NUMBER_FONT)

  Clear  a single display at pos (starting at 0, left to right)
  clearDisplayDigit(byte pos, boolean dot)

  Set the display to the values (left to right)
  setDisplay(const byte values[], unsigned int length = 8)

  Clear the display
  clearDisplay()

  Set the display to the string (defaults to built in font)
  setDisplayToString(const char* string, const word dots = 0, const byte pos = 0,
    const byte font[] = FONT_DEFAULT)

  Instantiate a tm1638 module specifying the display state, the starting intensity (0-7) data, clock and stobe pins.
    TM1638(byte dataPin, byte clockPin, byte strobePin, boolean activateDisplay = true, byte intensity = 7)

    /** Set the display to a unsigned hexadecimal number (with or without leading zeros)
    setDisplayToHexNumber(unsigned long number, byte dots, boolean leadingZeros = true,
    const byte numberFont[] = NUMBER_FONT)

    /** Set the display to a unsigned decimal number (with or without leading zeros)
    setDisplayToDecNumber(unsigned long number, byte dots, boolean leadingZeros = true,
    const byte numberFont[] = NUMBER_FONT)

    /** Set the display to a signed decimal number (with or without leading zeros)
   setDisplayToSignedDecNumber(signed long number, byte dots, boolean leadingZeros = true,
    const byte numberFont[] = NUMBER_FONT)

    /** Set the display to a unsigned binary number
    setDisplayToBinNumber(byte number, byte dots,
    const byte numberFont[] = NUMBER_FONT)

    /** Set the LED at pos to color (TM1638_COLOR_RED, TM1638_COLOR_GREEN or both)
   setLED(byte color, byte pos)
    /** Set the LEDs. MSB byte for the green LEDs, LSB for the red LEDs
    setLEDs(word led)

    /** Returns the pressed buttons as a bit set (left to right)
    virtual byte getButtons()
*/
//LiquidCrystal_I2C lcd(0x27, 20, 4);
LiquidCrystal_I2C lcd(0x3F, 16, 2);  // define object of LCD
/* LCD
   Stel hier in welke chip en foromaat LCD je hebt
   Gebruik 0x27 als je chip PCF8574 hebt van NXP
   Gebruik 0x3F als je chip PCF8574A hebt van Ti (Texas Instruments)
   De laatste twee getallen geven het formaat van je LCD aan
   bijvoorbeeld 20x4 of 16x2

 *  *
   PIN AANSLUITINGEN

   SDA is serial data
   SCL is serial clock

   GND --> GND wit
   VCC --> 5V  grijs
   SDA --> A4  rood
   SCL --> A5  blauw


   I2C aansluitingen per Arduino:
   Uno, Ethernet    A4 (SDA), A5 (SCL)
   Mega2560         20 (SDA), 21 (SCL)
   Leonardo          2 (SDA),  3 (SCL)
   Due              20 (SDA), 21 (SCL) of SDA1, SCL1

*/
Servo MyServo; // define an object of servomotor
Servo MyServo2; // define an object of servomotor
// *********************************************
// My Servo is a Hitec HS-211 servo. Not the fastest but for prototyping it will do.
// Datasheet shows Max Travel (out of box)202 deg
// Vario Range will be -5 <> +5
// range 10 m/s for 200 deg
// 1 m/s = 20 deg of travel
// zero point = 100
// + 5 m/s = 5 * 20 + 100 = 200
// -5 m/s = -5 * 20 + 100 = 0
// X m/s = X * StpVario + 100;
// 1 step = 0,05 m/s
//
//-1.3 m/s = -1.3 * 20 +100 = -26+100=74
//-1.4 m/s = -1.4 * 20 + 100 =-28+100=72
// *********************************************600
int StepVario = 20; // 1 ms/s = 20 steps on the servo --> 0.1 m/s = 2 steps on the servo


SwitecX25 ACSpeedStepper(STEPS,8,9,10,11);
// standard X27.168 range 315 degrees at 1/3 degree steps
// maxsteps: 945 --> X27 can only turn 315° =>  315° * 3° = 945 steps
// max speed glider: 300 kmph --> 945/300 = 3.15 steps per km
float stpSpeed = 3.15;

/*
AccelStepper ACSpeedStepper(AccelStepper::FULL4WIRE, 8, 10, 9, 11);
// *********************************************
// 1 revolution = 200 km/h (for my gauge)
// 1 rev = 2048 steps (for my steppermotor)
// 2048 steps = 200 km/h
// 2048/200 = 1 km/h results in 10,24 steps per 1 km/h
// *********************************************
float stpSpeed = 10.24; //how many steps per km/h
*/

byte buttons1 = 0;
byte buttons2 = 0;
//byte buttons3=0;
byte oldbuttons1 = 0;
byte oldbuttons2 = 0;
//byte oldbuttons3=0;
byte page1 = 1;
byte page2 = 1;
//byte page3=1;

unsigned long milstart, milstart2 = 0;
bool changed;

bool homing = false;
bool lcdon = false;
bool TM1638on = true;

int altl, spdl, hdgl, bnkl, pitl, varrl, varel, varil, gfol, yawl;
int alth, spdh, hdgh, bnkh, pith, varrh, vareh, varih, gfoh, yawh;
int alt, spd, hdg, bnk, pit;
double varr, vare, vari, gfo, yaw;


void setup()
{
  MyServo.attach(Servopin); // my servo is attached to pin 7
  MyServo.write(100);// Range is 0-200. Zero point is halfway = 100
  MyServo2.attach(6); // my s2nd servo is attached to pin 6
  MyServo2.write(100);// Range is 0-200. Zero point is halfway = 100

  //ACSpeedStepper.setMaxSpeed(600.0); //maxium number of steps per second. must be >0. my motor: 3 seconds for 2048 steps. == 2048/3 = 620.6 steps per sec
  //ACSpeedStepper.setAcceleration(600.0); // I dont want accel/decel
  //ACSpeedStepper.setCurrentPosition(0); // this needs to be in a homing routine. For now I assume the current position = 0 km/h

  ACSpeedStepper.zero();
  ACSpeedStepper.setPosition(STEPS/2);
  
  if (lcdon)
  {
    lcd.init();                  // initialiseer het LCD scherm
    lcd.backlight();             // zet de backlight aan
    lcd.clear();                 // maak leeg
    lcd.setCursor(0, 0);         // cursor linksboven
  }
  else
  { // lcd is off
    lcd.clear();
    lcd.noBacklight();
  }

  if (TM1638on)
  {
    tm1.setupDisplay(true, 1);
    tm2.setupDisplay(true, 1);
    tm3.setupDisplay(true, 1);

    tm1.clearDisplay();
    tm2.clearDisplay();
    tm3.clearDisplay();

    tm1.setLEDs(0xFF00);
    tm2.setLEDs(0xFF00);
    tm3.setLEDs(0xFF00);
    byte values[] = { 99, 99, 99, 99, 99, 99, 99, 99 };
    tm1.setDisplay(values);
    tm2.setDisplay(values);
    tm3.setDisplay(values);
  }
  else // its off
  {
    tm1.clearDisplay(); // clears the display, ot the leds
    tm2.clearDisplay();

    tm1.setupDisplay(false, 2);
    tm2.setupDisplay(false, 2);

  }

  Serial.begin(19200);
}

void loop()
{
  if (Serial.available() > 0) // there is data in the buffer
  {
    if (Serial.available() > 21) // wait until it is filled with all the bytes we need. (21 total)
    {
      if (Serial.read() == 255) //we found the start serialdata[0]
      {
        altl = Serial.read();//serialdata[1] // read altitute
        alth = Serial.read();//serialdata[2]
        spdl = Serial.read();//serialdata[3]// read speed
        spdh = Serial.read();//serialdata[4]
        hdgl = Serial.read();//serialdata[5]// read compass
        hdgh = Serial.read();//serialdata[6]
        pitl = Serial.read();//serialdata[7]// read pitch
        pith = Serial.read();//serialdata[8]
        bnkl = Serial.read();//serialdata[9]// read bank
        bnkh = Serial.read();//serialdata[10]
        varrl = Serial.read();//serialdata[11] // read vario raw
        varrh = Serial.read();//serialdata[12]
        varel = Serial.read();//serialdata[13]// read vario elec
        vareh = Serial.read();//serialdata[14]
        varil = Serial.read();//serialdata[15]// read vario integrated
        varih = Serial.read();//serialdata[16]
        gfol = Serial.read();//serialdata[17]// read Gforce
        gfoh = Serial.read();//serialdata[18]
        yawl = Serial.read(); //serialdata[19] // read yawstringangle
        yawh = Serial.read(); //serialdata[20]
        //All is read --> convert the bytes into proper data
        alt = altl << 8 | alth; //decode altitude (m)
        spd = spdl << 8 | spdh; //decode speed (in kmph)
        hdg = hdgl << 8 | hdgh; //decode compass (deg)
        pit = (pitl << 8 | pith) - 90; //decode pitch (deg)
        bnk = (bnkl << 8 | bnkh) - 180; ////decode bank (deg)
        varr = ((varrl << 8 | varrh) / 10.0) - 100.0; //decode raw vario (m/s)
        vare = ((varel << 8 | vareh) / 10.0) - 100.0; //decode elec vario (m/s)
        vari = ((varil << 8 | varih) / 10.0) - 100.0; //decode integrated vario (m/s)
        gfo = ((gfol << 8 | gfoh) / 10.0) - 10.0; //decode gforce
        yaw = ((yawl << 8 | yawh)) - 50.0; //decode yawstringangle (deg) [-99,99] but more in the range of [-10 , 10]

        int temp = spd * stpSpeed;
        //ACSpeedStepper.moveTo(temp); // where to go
        ACSpeedStepper.setPosition(temp);
        
        if (vari < -5) vari = -4.9;
        if (vari > 5) vari = 4.9;

        //debug info
        if (lcdon)
        {
          lcd.setCursor(0, 0);
          lcd.print("var:" + String(varr)); //or (String(val, DEC)
          lcd.setCursor(0, 1);
          lcd.print("var i:" + String(vari)); // for floats.
        }

        if (TM1638on)
        {
          tm1.setDisplayToSignedDecNumber(alt, 0, false);
          tm2.setDisplayToSignedDecNumber(spd, 0, false);
          tm3.setDisplayToSignedDecNumber(hdg, 0, false);
          tm1.setLEDs(0);
          tm3.setLEDs(0);

          // Set the Yawstring
          YawLeds (tm2, yaw);
        }
      }
    }
  }

  //Set the Speeddial
  //*****************

  //ACSpeedStepper.run(); //go
   ACSpeedStepper.update();
   
  // Set the VARIO
  //*****************
  MyServo.write(varr * StepVario + 100); //0.1 m/s *20 +100 = 102
  MyServo2.write(vari * StepVario + 100); //
  /*
    if (TM1638on)
    {

     buttons2 = tm2.getButtons();
     if (buttons2 != 0) //buttons are pressed sir
     {
       if (buttons2 != oldbuttons2) //and it is not the same one
       {
         oldbuttons2 = buttons2;
         page2 = buttons2;
         tm1.clearDisplay();
         tm2.clearDisplay();
         tm1.setLEDs(0);
         tm2.setLEDs(0);
       }
     }

     switch (page2)
     {
       case 1: //left most button
         {
           TMDint(tm1, "spd", spd);
           break;
         }
       case 2: // button 2
         {
           TMDint(tm2, "alt", alt);
           break;
         }
       case 4: // button 3
         {
           TMDint(tm2, "bank", bnk);
           break;
         }
       case 8: // button 4
         {
           TMDint(tm2, "pit", pit);
           break;
         }
       case 16: // button 5
         {
           TMDint(tm2, "hdg", hdg);
           break;
         }
       case 32: // button 6
         {
           TMDfloat(tm1, "G", gfo);
           break;
         }
     }

    }
  */

}//end loop

void YawLeds(TM1638 m, double a)
{
  // Set the Yawstring
  //*****************
  //https://www.mathsisfun.com/binary-decimal-hexadecimal-converter.html

  if (a == 0) m.setLEDs(0);
  //Yaw Right:
  if (a > 0.0 && a < 2.0) m.setLEDs(4096);
  if (a >= 2.0 && a < 4.0) m.setLEDs(8192);
  if (a >= 4.0 && a < 6.0) m.setLEDs(16384);
  if (a >= 8.0) m.setLEDs(32768);
  //Yaw Left:
  if (a <= -8.0) m.setLEDs(256);
  if (a >= -6.0 && a < -4.0) m.setLEDs(512);
  if (a >= -4.0 && a < -2.0) m.setLEDs(1024);
  if (a >= -2.0 && a < 0.0) m.setLEDs(2048);
}


void TMDfloat(TM1638 m, String v, double val)
{

  m.setDisplayToString(v, 0, 0);
  m.setDisplayToString(String(val, 1), 0, 4);
}

void TMDint(TM1638 m, String v, int val)
{
  m.setDisplayToString(v, 0, 0);
  if (val <= -100 && val > -1000)
  {
    m.setDisplayToString(String(val, DEC), 0, 4);
  }
  if (val <= -10 && val > -100)
  {
    m.clearDisplayDigit(4, false);
    m.setDisplayToString(String(val, DEC), 0, 5);
  }
  if (val < 0 && val > -10)
  {
    m.clearDisplayDigit(4, false);
    m.clearDisplayDigit(5, false);
    m.setDisplayToString(String(val, DEC), 0, 6);
  }
  if (val < 10 && val >= 0)
  {
    m.clearDisplayDigit(4, false);
    m.clearDisplayDigit(5, false);
    m.clearDisplayDigit(6, false);
    m.setDisplayToString(String(val, DEC), 0, 7);
  }
  if (val < 100 && val >= 10)
  {
    m.clearDisplayDigit(4, false);
    m.clearDisplayDigit(5, false);
    m.setDisplayToString(String(val, DEC), 0, 6);
  }
  if (val < 1000 && val >= 100)
  {
    m.clearDisplayDigit(4, false);
    m.setDisplayToString(String(val, DEC), 0, 5);
  }
  if (val < 10000 && val >= 1000)
  {
    m.setDisplayToString(String(val, DEC), 0, 4);
  }
}
