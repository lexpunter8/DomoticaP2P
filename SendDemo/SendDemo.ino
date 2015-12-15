/*
  Example for different sending methods
<<<<<<< HEAD
  lex was hier
=======
  Bauke was hier
>>>>>>> 654a4f79402bab8522b65e667d066e66242dfed4
  http://code.google.com/p/rc-switch/
  
*/

#include <RCSwitch.h>

RCSwitch mySwitch = RCSwitch();

void setup() {

  Serial.begin(9600);
  
  // Transmitter is connected to Arduino Pin #10  
  mySwitch.enableTransmit(6);

  // Optional set pulse length.
   mySwitch.setPulseLength(320);
  
  // Optional set protocol (default is 1, will work for most outlets)
    mySwitch.setProtocol(1);
  
  // Optional set number of transmission repetitions.
   mySwitch.setRepeatTransmit(15);
  
}

void loop() {


  /* Same switch as above, but using decimal code */
  mySwitch.send(12054575, 24);
  Serial.println("send");
  delay(1000);  
  mySwitch.send(12054574, 24);
  delay(1000);  


  delay(2000);
}
