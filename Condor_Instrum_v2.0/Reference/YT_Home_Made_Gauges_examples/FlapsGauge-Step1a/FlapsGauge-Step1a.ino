/* AccelStepper library can be downloaded here :
 *  http://www.airspayce.com/mikem/arduino/AccelStepper/index.html
 */

// STEP 1a : Controlling a stepper //

#include <AccelStepper.h>
AccelStepper gauge1(AccelStepper::FULL4WIRE, 8, 10, 9, 11); // Driver pins In1 In2 In3 In4 on D5 D4 D3 D2

boolean firstLoop = true;
boolean secondLoop = false;

void setup() {
  gauge1.setMaxSpeed(500);
}

void loop() {
  if (firstLoop) { // Done only once on first loop
    gauge1.moveTo(2048); // 2048 steps = 1 complete rotation
    firstLoop = false;
    secondLoop = true;
  }

  // Now trying to reverse rotation before firstLoop target position reached
  
  if (secondLoop && millis()>2000) { // Done only once on second loop
    gauge1.moveTo(0); // Returns to initial position
    secondLoop = false;
  }
  
  gauge1.setSpeed(500); // Spindle speed
  gauge1.runSpeedToPosition(); // Calculate if a step is due

}
