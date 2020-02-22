/* AccelStepper library can be downloaded here :
 *  http://www.airspayce.com/mikem/arduino/AccelStepper/index.html
 */

// STEP 4 : Interfacing with FS or P3D //

// SLAVE //

#include <Wire.h>
#include <AccelStepper.h>
AccelStepper flapsGauge(AccelStepper::FULL4WIRE, 2, 4, 3, 5); // Driver pins In1 In2 In3 In4 on D5 D4 D3 D2

int receivedNb = -1; // -1 : no gauge update needed

void decodeNumber() {
  receivedNb = Wire.read(); // High 8 bits
  receivedNb |= Wire.read()<<8; // Low 8 bits
}

void setup() {
  flapsGauge.setMaxSpeed(500);
  Wire.begin(9); // This card (slave) address is 9
  Wire.onReceive(decodeNumber); // Call decodeNumber function when a transmission is received
}

void loop() {
  if (receivedNb!=-1) {
    flapsGauge.moveTo( map(receivedNb, 0, 16383, 0, 2048*3/4) ); // 3/4 turn for flaps full
    receivedNb = -1;
  }

  flapsGauge.setSpeed(500); // Spindle speed
  flapsGauge.runSpeedToPosition(); // Calculate if a step is due

}
