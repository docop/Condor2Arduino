/*  OACSP library and command gateway can be downloaded here :
 *  https://github.com/apoloval/open-airbus-cockpit
 *  Official site : (broken links)
 *  http://openairbuscockpit.org
 *  (Software > Arduino libraries > OACSP.H)
 */

// STEP 4 : Interfacing with FS or P3D //

// MASTER //

#include <Wire.h>
#include <oacsp.h>

void setup() {
  Wire.begin(8); // This card (master) address is 8
  OACSP.begin("FlapsGauge");
  OACSP.observeOffset (0x0BE0, OAC::OFFSET_UINT32); // FSUIPC offset 0x0BE0 : raw flaps pos (0 UP - 16383 FULL)
  // Offset 0x0BE0 is 4 bytes length -> 4x8=32 bits -> UINT32
}

void loop() {
  OACSP.pollEvent();

  if (OAC::OffsetUpdateEvent* event = OACSP.offsetUpdateEvent(0x0BE0)) { // Change detected in offset 0x0BE0
    int rawFsuipcFlapsPos = event->value; // FSUIPC raw value, from 0 to 16383
    Wire.beginTransmission(9); // Transmit to slave, addr 9
    Wire.write (rawFsuipcFlapsPos); // Low 8 bits
    Wire.write (rawFsuipcFlapsPos>>8); // Hi 8 bits
    Wire.endTransmission();
  }
}
