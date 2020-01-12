
/*
    This code is based on Jim's great"Link2fs_Multi". Without Jim I would still be in the dark.
    Jimspage.co.nz
*/

#include <Servo.h> // link to the servo library. 
#include <AccelStepper.h> // link to the advanced Stepper library. Accelstepper is not default you need to install it.

#include <Wire.h> // needed for the liquid crystal display (LCD)
#include <LiquidCrystal_I2C.h> // LCD display
#include <TM1638plus.h> // library for the 8 segments display / 8 buttons, 8 led red/green 


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

// **************************************************************************
TM1638plus tm(STROBE_TM1, CLOCK_TM , DIO_TM); // define object of TM1638
TM1638plus tm2(STROBE_TM2, CLOCK_TM , DIO_TM); // define object of TM1638
//void reset();
//void brightness(uint8_t brightness)--> Sets the brightness level on a scale of brightness = 0 to 7.
//uint8_t readButtons(void)-->Read buttons returns a byte with value of buttons 1-8 b7b6b5b4b3b2b1b0
//void setLED(uint8_t position, uint8_t value)-->Set an LED, pass it LED position 0-7 and value 0 or 1
//void displayText(const char *text)-->send Text to Seven segments, passed char array pointer
//void displayASCII(uint8_t position, uint8_t ascii)
//void displayASCIIwDot(uint8_t position, uint8_t ascii)
//void displayHex(uint8_t position, uint8_t hex)
//void display7Seg(uint8_t position, uint8_t value);
// **************************************************************************

LiquidCrystal_I2C lcd(0x3F, 16, 2);  // define object of LCD

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
String lcdtekst = "Condor2Arduino";
uint8_t buttons;

//Speedvaribles:
int SpeedOld = 0;
int SpeedNew = 0;
String SpeedRead = "";
bool homing = false;

bool lcdon = false;
bool TM1638on=true;

//Vario variables
double Vario = 0.0;
double VarioOld = 0.0;
String VarioRead = "";

//Altituted variables
int AltOld=0;
int AltNew=0;
String AltRead="";

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
  tm.reset();
  tm2.reset();
  tm.brightness(TM_BRT);
  tm2.brightness(TM_BRT);
  tm.displayText("Condor 2");
  tm2.displayText("Arduino");
  
  }
  Serial.begin(115200);
}

void loop()
{
   // this section is copy/pasted from link2Fs. It works perfect so no changes made to Jim's code
  if (Serial.available())
  {
    // Serialdata is always: Ident(1) +Value(4)+Ident(1)+value(4) etc etc
    CodeIn = getChar(); // get the first character
    // with the first character decide where to decode what.
    if (CodeIn == '=') 
    {
      EQUALS(); // The first identifier is "="
    }
    if (CodeIn == '<') 
    {
      LESSTHAN(); // The first identifier is "<"
    }
    if (CodeIn == '?') 
    {
      QUESTION(); // The first identifier is "?"
    }
    if (CodeIn == '/') 
    {
      SLASH(); // The first identifier is "/" (Annunciators)
    }
  } // end if serial available
  
  if (homing) // used to set the speed to zero position with software.
  {
    ACSpeedStepper.runSpeed();
  }
  else //normal situation.
  {
  // Set the SPEED
  ACSpeedStepper.moveTo(SpeedNew * stpSpeed); // where to go
  ACSpeedStepper.run(); //go
  }
  // Set the VARIO
  MyServo.write(Vario * StepVario + 100); //

  //Print data 2 lcd
  if (lcdon)
  {
    LCD(SpeedRead, VarioRead);
  }

  if (TM1638on)
  {
    TMDisplay(AltRead, SpeedRead);
  }
}


char getChar()// Get a character from the serial buffer
{
  while (Serial.available() == 0); // wait for data
  return ((char)Serial.read()); // Thanks Doug (Jim's comment)
}

void EQUALS() // used here for setting servo to zero position. I have no auto zero option yet.
{ // The first identifier was "="
  CodeIn = getChar(); // Get another character
  switch (CodeIn) { // Now lets find what to do with it
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
      }
    case 'C':
      //Do something
      break;
  }
}

void LESSTHAN() 
{ // The first identifier was "<"
  CodeIn = getChar(); // Get another character" //"B" or "G"
  switch (CodeIn)
  { // Now lets find what to do with it
    // example input string <S0139<V-2.5<A1470<G+1.5
   case 'V': // here we decode our vario data // for example:<V-3.6
      {
        VarioRead = "";
        VarioRead += getChar(); // "-" or "+"
        VarioRead += getChar(); // 1e char "-3"
        VarioRead += getChar(); // "-3."
        VarioRead += getChar(); // "-3.6"
        Vario = VarioRead.toDouble();
        break;
      }
    case 'S': //Found the second identifier (Speed) //format <S123
      {
        SpeedRead = "";
        SpeedRead += getChar();//"0"
        SpeedRead += getChar();//"01"
        SpeedRead += getChar();//"013"
        SpeedRead += getChar();//"0139"
        SpeedNew = SpeedRead.toInt();//123
        break;
      }
    case 'A': //Found the second identifier (Altitude) //format <A1234
      {
        AltRead = "";
        AltRead += getChar();//"1"
        AltRead += getChar();//"12"
        AltRead += getChar();//"123"
        AltRead += getChar();//"1234"
        AltNew = AltRead.toInt();//1234
        break;
      }
  }
}

void QUESTION()
{ // The first identifier was "?"
  CodeIn = getChar(); // Get another character
  switch (CodeIn)
  { // Now lets find what to do with it
    case 'A'://Found the second identifier
      //Do something
      break;

    case 'B':
      //Do something
      break;

    case 'C':
      //Do something
      break;
  }
}
void SLASH() {   // The first identifier was "/" (Annunciator)
  //Do something
}

void LCD(String a, String b)
{
  lcd.setCursor(0, 1);
  lcd.print(a);
  lcd.setCursor(9, 1);
  lcd.print(b);
}

void TMDisplay(String a, String b)
{
 const char *c = a.c_str();
 const char *d = b.c_str();
  tm.displayText("        ");
  tm2.displayText("        ");
  tm.displayText(c);
  tm2.displayText(d);
//Serial.print(a);
//Serial.print(b);
 
}
