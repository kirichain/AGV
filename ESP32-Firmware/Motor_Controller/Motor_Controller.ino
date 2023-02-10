#include <SerialCommand.h>

SerialCommand sCmd;   

int motor_speed = 255;

int ENB = D5;
int IN3 = D6;
int IN4 = D7;

int ENA = D0;
int IN1 = D1;
int IN2 = D2;

void setup() {
  //Setting motors pin
  pinMode(ENA, OUTPUT);
  pinMode(IN1, OUTPUT);
  pinMode(IN2, OUTPUT); 
  pinMode(ENB, OUTPUT);
  pinMode(IN3, OUTPUT);
  pinMode(IN4, OUTPUT); 
  
  digitalWrite(IN1, LOW);
  digitalWrite(IN2, LOW);
  digitalWrite(IN3, LOW);
  digitalWrite(IN4, LOW);

  Serial.begin(115200);
  while (!Serial) {
    
  }
  
  Serial.println();
  Serial.println("Ready");
  sCmd.addCommand("forward", forward);
  sCmd.addCommand("backward", backward);
  sCmd.addCommand("turn-left", turn_left);
  sCmd.addCommand("turn-right", turn_right);
  sCmd.addCommand("speed", set_speed);
  sCmd.addCommand("interval", interval);
  sCmd.addCommand("checkName", checkName);
  sCmd.setDefaultHandler(unrecognized);      // Handler for command that isn't matched  (says "What?")
}

void loop() {
  sCmd.readSerial();
}

void forward() {
  digitalWrite(IN1, LOW);
  digitalWrite(IN2, HIGH);

  digitalWrite(IN3, LOW);
  digitalWrite(IN4, HIGH);
  
  analogWrite(ENA, motor_speed);
  analogWrite(ENB, motor_speed);
  delay(100);
  stop();  
}

void backward() {
  digitalWrite(IN1, HIGH);
  digitalWrite(IN2, LOW);

  digitalWrite(IN3, HIGH);
  digitalWrite(IN4, LOW);

  analogWrite(ENA, motor_speed);
  analogWrite(ENB, motor_speed);
  delay(100);
  stop();
}

void turn_left() {
  digitalWrite(IN1, HIGH);
  digitalWrite(IN2, LOW);

  digitalWrite(IN3, LOW);
  digitalWrite(IN4, HIGH);
  
  analogWrite(ENA, motor_speed);
  analogWrite(ENB, motor_speed);
  delay(100);
  stop();  
}

void turn_right() {
  digitalWrite(IN1, LOW);
  digitalWrite(IN2, HIGH);

  digitalWrite(IN3, HIGH);
  digitalWrite(IN4, LOW);
  
  analogWrite(ENA, motor_speed);
  analogWrite(ENB, motor_speed);
  delay(100);
  stop();  
}

void stop() {
  digitalWrite(IN1, LOW);
  digitalWrite(IN2, LOW);
  digitalWrite(IN3, LOW);
  digitalWrite(IN4, LOW);
}

void set_speed() {
  char *arg;

  Serial.println("Setting speed");
  arg = sCmd.next();
  if (arg != NULL) {
    motor_speed = atoi(arg);
    Serial.print("Motor speed is set to: ");
    Serial.println(motor_speed);
  }
  else {
    Serial.println("No speed argument");
  }
}

void interval() {
  char *arg;
  int interval;
  int i;

  Serial.println("Setting interval");
  arg = sCmd.next();
  if (arg != NULL) {
    interval = atoi(arg);
    Serial.print("Interval is set to: ");
    Serial.println(interval);
    i = 0;
    while (i < (interval*10)) {
      i++;
      forward();           
    }     
  }
  else {
    Serial.println("No interval argument");
  }
}

// This gets set as the default handler, and gets called when no other command matches.
void unrecognized(const char *command) {
  Serial.println("What?");
}

void checkName() {
  Serial.println("motor-controller");
}