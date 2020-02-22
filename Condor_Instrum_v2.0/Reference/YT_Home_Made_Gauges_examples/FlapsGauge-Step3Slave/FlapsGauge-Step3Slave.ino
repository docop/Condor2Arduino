// STEP 3 : Making 2 cards communicate using I2C protocol //

// SLAVE // 

#include <Wire.h>
int receivedNb = 0;

void decodeNumber() {
  receivedNb = Wire.read();
}

void setup() {
  pinMode (13, OUTPUT); // Arduino integrated led which will flash
  Wire.begin(9); // This card (slave) addr is 9
  Wire.onReceive(decodeNumber); // Call decodeNumber function when transmission is received
}

void loop() {
  if (receivedNb>0) { // Transmission received -> flash integrated led
    for (int i=0; i<receivedNb; i++) {
      digitalWrite (13, HIGH);
      delay(300);
      digitalWrite (13, LOW);
      delay(300);
    }
    receivedNb = 0;
  }
}
