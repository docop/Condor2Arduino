// STEP 3 : Making 2 cards communicate using I2C protocol //

// MASTER // 

#include <Wire.h>

void setup() {
  Serial.begin(9600);
  Wire.begin(8); // This card (master) addr is 8
}

void loop() {
  int randomNumber = random(1,10); // Decide a random number from 1 to 10

  Serial.println ("Deciding a random number : " + String(randomNumber));
  delay(2000);

  Serial.println ("Sending number to slave ...");
  delay(2000);

  Wire.beginTransmission(9); // Send to slave, addr 9
  Wire.write(randomNumber);
  Wire.endTransmission();
  delay(10000);
}
