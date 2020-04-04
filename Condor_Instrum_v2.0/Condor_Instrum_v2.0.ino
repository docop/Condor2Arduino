#include <Servo.h> // link to the servo library. 
#include <Wire.h> // needed for the liquid crystal display (LCD)
#include <Stepper.h>

#define motorPin1  8      // IN1 on the ULN2003 driver
#define motorPin2  9      // IN2 on the ULN2003 driver
#define motorPin3  10     // IN3 on the ULN2003 driver
#define motorPin4  11     // IN4 on the ULN2003 driver
#define Servopin 7        // Pin 7 PWM on Arduino

#define STEPS (315*3) // 945
// the x27 stepper lists 1/3deg per step.=3 steps per deg = 3*360 steps per full rotation = 1080 steps per revolution.
// 1080/360*315 (max) = 945.
const int X27StepsPerRevolution = 1080;
const int maxSteps = 945; // X27 can only turn 315°

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

Stepper ACSpeedStepper(X27StepsPerRevolution, 8, 9, 10, 11);
  

bool homing = false;
bool ServoOn = true;

int altl, spdl, hdgl, bnkl, pitl, varrl, varel, varil, gfol, yawl;
int alth, spdh, hdgh, bnkh, pith, varrh, vareh, varih, gfoh, yawh;
int alt, spd, hdg, bnk, pit;
double varr, vare, vari, gfo, yaw;
int spdval,pos;
int maxspeed=300;
int maxsteps=630;
void setup()
{
  if (ServoOn)
  {
    MyServo.attach(Servopin); // my servo is attached to pin 7
    MyServo.write(100);// Range is 0-200. Zero point is halfway = 100
    MyServo2.attach(6); // my s2nd servo is attached to pin 6
    MyServo2.write(100);// Range is 0-200. Zero point is halfway = 100
  }
 
  pos = 0;
  spdval=0;
  ACSpeedStepper.setSpeed(25);
  ACSpeedStepper.step (-1*maxsteps);


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

        if (spd>maxspeed) spd=maxspeed;
        spdval = map(spd, 0, maxspeed, 0, maxsteps);

        if (vari < -5) vari = -4.9;
        if (vari > 5) vari = 4.9;
      }
    }
  }

  //Set the Speeddial
  //*****************
  
  if (abs(spdval - pos) >= 2)
  { //if diference is greater than 2 steps.
    if ((spdval - pos) > 0)
    {
      ACSpeedStepper.step(+1);      // move one step to the right.
      pos++;
    }
    if ((spdval - pos) < 0)
    {
      ACSpeedStepper.step(-1);       // move one step to the left.
      pos--;
    }
  }
  // Set the VARIO
  //*****************
  if (ServoOn)
  {
   
   MyServo.write(varr * StepVario + 100); //0.1 m/s *20 +100 = 102
   MyServo2.write(vari * StepVario + 100); //
  }
 
}//end loop
