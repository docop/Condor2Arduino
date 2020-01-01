
/*
    This code is based on Jim's great"Link2fs_Multi". Without Jim I would still be in the dark.
    Jimspage.co.nz
*/

#include <Servo.h> // link to the servo library. 
#include <AccelStepper.h> // link to the advanced Stepper library. Accelstepper is not default you need to install it.


#define motorPin1  8      // IN1 on the ULN2003 driver
#define motorPin2  9      // IN2 on the ULN2003 driver
#define motorPin3  10     // IN3 on the ULN2003 driver
#define motorPin4  11     // IN4 on the ULN2003 driver
#define Servopin 7        // Pin 7 PWM on Arduino


Servo MyServo;

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


int SpeedOld = 0;
int SpeedNew = 0;
String SpeedRead = "";
bool homing = false;
double Vario = 0.0;
double VarioOld = 0.0;
String VarioRead = "";



void setup()
{
  MyServo.attach(Servopin); // my servo is attached to pin 7
  MyServo.write(100);// Range is 0-200. Zero point is halfway = 100

  ACSpeedStepper.setMaxSpeed(400.0); //maxium number of steps per second. must be >0. my motor: 5 seconds for 2048 steps. == 2048/5 = 409,6 steps per sec
  ACSpeedStepper.setAcceleration(400.0); // I dont want accel/decel
  ACSpeedStepper.setSpeed(400.0); // set it at max
  ACSpeedStepper.setCurrentPosition(0); // this needs to be in a homing routine. For now I assume the current position = 0 km/h

  Serial.begin(115200);
}

void loop()
{
  if (homing)
  {
    ACSpeedStepper.runSpeed();
  }
  else 
  {
  ACSpeedStepper.moveTo(SpeedNew * stpSpeed); // where to go
  ACSpeedStepper.run(); //go
  }
  
  MyServo.write(Vario * StepVario + 100); //

  // this section is copy/pasted from link2Fs. It works perfect so no changes made to Jim's code
  if (Serial.available())
  {
    // Serialdata is always: Ident(1) +Value(4)+Ident(1)+value(4) etc etc
    CodeIn = getChar(); // get the first character
    // with the first character decide where to decode what.
    if (CodeIn == '=') {
      EQUALS(); // The first identifier is "="
    }
    if (CodeIn == '<') {
      LESSTHAN(); // The first identifier is "<"
    }
    if (CodeIn == '?') {
      QUESTION(); // The first identifier is "?"
    }
    if (CodeIn == '/') {
      SLASH(); // The first identifier is "/" (Annunciators)
    }
  }
}


char getChar()// Get a character from the serial buffer
{
  while (Serial.available() == 0); // wait for data
  return ((char)Serial.read()); // Thanks Doug (Jim's comment)
}

void EQUALS() // not used
{ // The first identifier was "="
  CodeIn = getChar(); // Get another character
  switch (CodeIn) { // Now lets find what to do with it
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

void LESSTHAN()
{ // The first identifier was "<"
  CodeIn = getChar(); // Get another character" //"B" or "G"
  switch (CodeIn)
  { // Now lets find what to do with it
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
    case 'B': // here we decode our vario data // for example:<B-3.6
      {
        VarioRead = "";//""
        VarioRead += getChar(); // "-"
        VarioRead += getChar(); // 1e char "-3"
        VarioRead += getChar(); // "-3."
        VarioRead += getChar(); // "-3.6"
        Vario = VarioRead.toDouble();
        break;
      }
    case 'G': //Found the second identifier ("G" Speed ident)
      {
        SpeedRead = ""; //""
        SpeedRead += getChar();//"0"
        SpeedRead += getChar();//"01"
        SpeedRead += getChar();//"012"
        SpeedRead += getChar();//"0123"
        SpeedNew = SpeedRead.toInt();//123
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
