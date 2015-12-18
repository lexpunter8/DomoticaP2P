//schakelaar aan      uit
// 1         12054575 12054574
// 2         12054573 12054572
// 3         12054571 12054570
// all       12054562 12054561   

//
#include <SPI.h>
#include <Ethernet.h>             // Ethernet library
#include <RCSwitch.h>             // RCSwitch library

#define infoPin   9     // infoPin at pin 9
#define analogPin A0    

RCSwitch mySwitch = RCSwitch();

//Set Ethernet Shield MAC address  (check yours)
byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };   // Ethernet adapter shield S. Oosterhaven

int ethPort = 3300;                                  // Take a free port (check your router)

EthernetServer server(ethPort);              // EthernetServer instance (listening on port <ethPort>).

   int maxx = 150;
   int minn = 0;

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
  Serial.println(Ethernet.localIP());

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
  // put your main code here, to run repeatedly:
  EthernetClient ethernetClient = server.available();
  Serial.println(analogRead(analogPin));
  delay(100);
  while(ethernetClient.connected()){
    digitalWrite(infoPin, HIGH);
  }

  
  setSensor('l', minn, maxx, 1);
  
}

void switches(String x, int switchN)
{
  
  if(switchN == 1){
    if(x == "uit"){
    mySwitch.send(12054574, 24);
  }
  if(x == "aan"){
    mySwitch.send(12054575, 24);
  }
  }
  if(switchN == 2){
    if(x == "uit"){
    mySwitch.send(12054572, 24);
  }
  if(x == "aan"){
    mySwitch.send(12054573, 24);
  }
  }
  if(switchN == 3){
    if(x == "uit"){
    mySwitch.send(12054570, 24);
  }
  if(x == "aan"){
    mySwitch.send(12054571, 24);
  }
  }
}


void setSensor(char sensor, int minValue, int maxValue, int swn){
  int lichtValue = analogRead(analogPin);
  if(sensor == 'l')
  {
    if(lichtValue < maxValue && lichtValue > minValue)
    {  
      switches("aan", swn);
    } 
    else 
    {
      switches("uit", swn);
    }
  }
  if(sensor == 't')
  {
    if(lichtValue < maxValue && lichtValue > minValue)
    {  
      switches("aan", swn);
    } 
    else 
    {
      switches("uit", swn);
    }
  }
}

void onOff(int s){
  if(s == 1){
    
  }
}

