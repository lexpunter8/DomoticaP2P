#include <SPI.h>
#include <Ethernet.h>             // Ethernet library
#include <RCSwitch.h> 

#define infoPin   9
#define analogPin A0

//Set Ethernet Shield MAC address  (check yours)
byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };   // Ethernet adapter shield S. Oosterhaven

int ethPort = 3300;                                  // Take a free port (check your router)

EthernetServer server(ethPort);              // EthernetServer instance (listening on port <ethPort>).

void setup() {
  // put your setup code here, to run once:
  pinMode(infoPin, OUTPUT);
  pinMode(analogPin, INPUT);
  
  Serial.begin(9600);
  Serial.println("started");
  Serial.println(Ethernet.begin(mac));

  if(Ethernet.begin(mac) == 0){
    Serial.println("Could not obtain IP");
    while(true){
      //Serial.println("test");
    }
  }

  Serial.println("Connected");

  server.begin();

  Serial.print("IP; ");
  Serial.print(Ethernet.localIP());
}

void loop() {
  // put your main code here, to run repeatedly:
  EthernetClient ethernetClient = server.available();
  Serial.println(analogRead(analogPin));
  delay(100);
  while(ethernetClient.connected()){
    digitalWrite(infoPin, HIGH);
  }
}
