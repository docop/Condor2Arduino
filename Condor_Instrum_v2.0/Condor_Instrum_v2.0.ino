
/*
    This code is based on Jim's great "Link2fs_Multi". Without Jim I would still be in the dark.
    Jimspage.co.nz
*/

#include <Servo.h> // link to the servo library. 
#include <AccelStepper.h> // link to the advanced Stepper library. Accelstepper is not default you need to install it.

#include <Wire.h> // needed for the liquid crystal display (LCD)
#include <LiquidCrystal_I2C.h> // LCD display
#include <TM1638plus.h> // library for the 8 segments display / 8 buttons, 8 led red/green 
#include <TM1638.h> //older library for the 8 segments display / 8 buttons, 8 led red/green 

#define motorPin1  8      // IN1 on the ULN2003 driver
#define motorPin2  9      // IN2 on the ULN2003 driver
#define motorPin3  10     // IN3 on the ULN2003 driver
#define motorPin4  11     // IN4 on the ULN2003 driver
#define Servopin 7        // Pin 7 PWM on Arduino
#define  STROBE_TM1 4     // STRB0 of board1 is om pin 4
#define  STROBE_TM2 5     // STRB1 of board 2 is on pin 5
#define  CLOCK_TM 2       // CLK of all boards is on pin 2
#define  DIO_TM 3         // DIO of all boards is on pin 3
#define TM_BRT 0x02       // set the brightness of board (0-7)

//*************************************************************************
TM1638plus tm(STROBE_TM1, CLOCK_TM , DIO_TM); // define object of TM1638
TM1638plus tm2(STROBE_TM2, CLOCK_TM , DIO_TM); // define object of TM1638
//*************************************************************************
/*
  void reset();
  void brightness(uint8_t brightness)--> Sets the brightness level on a scale of brightness = 0 to 7.
  uint8_t readButtons(void)-->Read buttons returns a byte with value of buttons 1-8 b7b6b5b4b3b2b1b0
  void setLED(uint8_t position, uint8_t value)-->Set an LED, pass it LED position 0-7 and value 0 or 1
  void displayText(const char *text)-->send Text to Seven segments, passed char array pointer
  void displayASCII(uint8_t position, uint8_t ascii)
  void displayASCIIwDot(uint8_t position, uint8_t ascii)
  void displayHex(uint8_t position, uint8_t hex)
  void display7Seg(uint8_t position, uint8_t value);
 **************************************************************************
*/

TM1638 tm3(DIO_TM, CLOCK_TM, STROBE_TM1);
TM1638 tm4(DIO_TM, CLOCK_TM, STROBE_TM2);
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
    void setDisplayToHexNumber(unsigned long number, byte dots, boolean leadingZeros = true,
    const byte numberFont[] = NUMBER_FONT)
    /** Set the display to a unsigned decimal number (with or without leading zeros)
    void setDisplayToDecNumber(unsigned long number, byte dots, boolean leadingZeros = true,
    const byte numberFont[] = NUMBER_FONT)
    /** Set the display to a signed decimal number (with or without leading zeros)
    void setDisplayToSignedDecNumber(signed long number, byte dots, boolean leadingZeros = true,
    const byte numberFont[] = NUMBER_FONT)
    /** Set the display to a unsigned binary number
    void setDisplayToBinNumber(byte number, byte dots,
    const byte numberFont[] = NUMBER_FONT)

    /** Set the LED at pos to color (TM1638_COLOR_RED, TM1638_COLOR_GREEN or both)
    virtual void setLED(byte color, byte pos)
    /** Set the LEDs. MSB byte for the green LEDs, LSB for the red LEDs
    void setLEDs(word led)

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
// *********************************************
// My Servo is a Hitec HS-211 servo
// Datasheet shows Max Travel (out of box)202 deg
// Vario Range will be -5 <> +5
// range 10 m/s for 200 deg
// 1 m/s = 20 deg of travel
// zero point = 100
// + 5 m/s = 5 * 20 + 100 = 200
// -5 m/s = -5 * 20 + 100 = 0
// X m/s = X * StpVario + 100;
// *********************************************
int StepVario = 20; // 1 ms/s = 20 steps on the servo

AccelStepper ACSpeedStepper(AccelStepper::FULL4WIRE, 8, 10, 9, 11); // refer to the accelstepper library how to set up your stepepr motor
// 12 rpm. rotations per minute.
// 1 rotation is 5 sec with my motor (i timed it).
// 60/5 = 12 revolutions per minute.
// *********************************************
// 1 revolution = 195 km/h (for an ASK-21 gauge)
// 1 rev = 2048 steps (for my steppermotor)
// 2048 steps = 195 km/h
//  2048/195 = 1 km/h results in
// 10,50 steps = 1 km/h
// *********************************************
float stpSpeed = 10.50; //how many steps per km/h

int CodeIn;// used in the serial reads
uint8_t buttons;

String lcdtekst = "Condor2Arduino";

bool homing = false;
bool lcdon = true;
bool TM1638on = true;


//Vario variables
double Vario = 0.0;
double eVario = 0.0;
double iVario = 0.0;
double Gforce = 0;


String SpeedRead = "";
String VarioRead = "";
String eVarioRead = "";
String iVarioRead = "";
String AltRead = "";
String GforceRead = "";
String CompassRead = "";
String BankRead = "";
String PitchRead = "";

int AltOld = 0;
int AltNew = 0;
double VarioOld = 0.0;
double VarioNew = 0.0;
int SpeedOld = 0;
int SpeedNew = 0;
int CompOld = 0;
int CompNew = 0;
int Bankint = 0;
int Pitchint = 0;
int Compassint = 0;

int altl, spdl, hdgl, bnkl, pitl, varrl, varel, varil, gfol;
int alth, spdh, hdgh, bnkh, pith, varrh, vareh, varih, gfoh;
int alt, spd, hdg, bnk, pit;
double varr, vare, vari, gfo;



void setup()
{
  MyServo.attach(Servopin); // my servo is attached to pin 7
  MyServo.write(100);// Range is 0-200. Zero point is halfway = 100

  ACSpeedStepper.setMaxSpeed(400.0); //maxium number of steps per second. must be >0. my motor: 5 seconds for 2048 steps. == 2048/5 = 409,6 steps per sec
  ACSpeedStepper.setAcceleration(400.0); // I dont want accel/decel
  ACSpeedStepper.setSpeed(400.0); // set it at max
  ACSpeedStepper.setCurrentPosition(0); // this needs to be in a homing routine. For now I assume the current position = 0 km/h

  if (lcdon) {
    lcd.init();                  // initialiseer het LCD scherm
    lcd.backlight();             // zet de backlight aan
    lcd.clear();                 // maak leeg
    lcd.setCursor(0, 0);         // cursor linksboven
    lcd.print(lcdtekst);         // print tekst in var lcdtekst
  }

  if (TM1638on)
  {
    /*
      tm.reset();
      tm2.reset();
      tm.brightness(TM_BRT);
      tm2.brightness(TM_BRT);
      tm.displayText("Condor 2");
      tm2.displayText("Arduino");
    */
    tm3.setupDisplay(true, 2);
    tm4.setupDisplay(true, 2);
    tm3.clearDisplay();
    tm4.clearDisplay();
    tm3.setDisplayToString("Condor 2", 0, 0);
    tm4.setDisplayToString("Arduino", 0, 0);
  }

  Serial.begin(19200);
}

void loop()
{
  if (Serial.available() > 0)
  {
    if (Serial.available() > 19)
    {
      if (Serial.read() == 255) //serialdata[0]
      {
        altl = Serial.read();//serialdata[1]
        alth = Serial.read();//serialdata[2]
        spdl == Serial.read();//serialdata[3]
        spdh = Serial.read();//serialdata[4]
        hdgl = Serial.read();//serialdata[5]
        hdgh = Serial.read();//serialdata[6]
        pitl = Serial.read();//serialdata[7]
        pith = Serial.read();//serialdata[8]
        bnkl = Serial.read();//serialdata[9]
        bnkh = Serial.read();//serialdata[10]
        varrl = Serial.read();//serialdata[11]
        varrh = Serial.read();//serialdata[12]
        varel = Serial.read();//serialdata[13]
        vareh = Serial.read();//serialdata[14]
        varil = Serial.read();//serialdata[15]
        varih = Serial.read();//serialdata[16]
        gfol = Serial.read();//serialdata[17]
        gfoh = Serial.read();//serialdata[18]

        alt = altl << 8 | alth; //decode altitude
        spd = spdl << 8 | spdh; //decode speed (kmph)
        hdg = hdgl << 8 | hdgh; //decode compass
        pit = pitl << 8 | pith - 90; //decode pitch (deg)
        bnk = bnkl << 8 | bnkh - 180; ////decode bank (deg)
        varr = ((varrl << 8 | varrh) / 10.0) - 99.9; //decode raw vario m/s
        vare = ((varel << 8 | vareh) / 10.0) - 99.9; //decode elec vario m/s
        vari = ((varil << 8 | varih) / 10.0) - 99.9; //decode integrated vario m/s
        gfo = ((gfol << 8 | gfoh) / 10.0) - 9.9; //decode gforce
      }
    }
  }

  if (homing) // used to set the speed to zero position with software.
  {
    ACSpeedStepper.runSpeed();
  }
  else //normal situation.
  {
    // Set the SPEED
    SpeedNew=spd;
    ACSpeedStepper.moveTo(SpeedNew * stpSpeed); // where to go
    ACSpeedStepper.run(); //go

    // Set the VARIO
    Vario=vari;
    MyServo.write(Vario * StepVario + 100); //

    if (lcdon)
    {
     lcd.setCursor(0, 1);
     lcd.print(spd);
     lcd.setCursor(13, 1);
     lcd.print(alt);
    }

  }
}


/*
    case 'A'://Found the second identifier
      {
        homing = true;
        ACSpeedStepper.setSpeed(100);
        break;
      }
    case 'D'://Found the second identifier
      {
        homing = false;
        ACSpeedStepper.setCurrentPosition(0);
        ACSpeedStepper.setSpeed(400);
        break;
*/


void LCD_A(String a)
{
  lcd.setCursor(0, 1);
  lcd.print(a);

}
void LCD_B(String b)
{
  lcd.setCursor(13, 1);
  lcd.print(b);
}
void TMDisplay(String a, String b)
{
  tm3.setDisplayToString(b, 0, 4);
  tm4.setDisplayToString(a, 0, 4);
}
void TMDisplay1(String a, String b)
{
  tm3.setDisplayToString(a, 0, 0);
  tm3.setDisplayToString(b, 0, 3);
}
void TMDisplay2(String a, String b)
{
  tm4.setDisplayToString(a, 0, 0);
  tm4.setDisplayToString(b, 0, 3);
}
