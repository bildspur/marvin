#include <NewPing.h>

#define TRIGGER_PIN  5
#define ECHO_PIN     4
#define MAX_DISTANCE 200

// temperature
#define THETA 22

NewPing sonar(TRIGGER_PIN, ECHO_PIN, MAX_DISTANCE);

void setup() {
  Serial.begin(115200);
}

void loop() {
  delay(30);

  float sonic = sonicSpeed(THETA);
  float pingTime = sonar.ping_median();

  float distanceMilli = (pingTime / 2.0) * sonic / 10000.0;

  /*
  Serial.print("Sensor: ");
  Serial.print(distance);
  Serial.print(" | Millis: ");
  */
  Serial.println(distanceMilli);
}

// simple sonic speed (-35 to +35C)
float sonicSpeed(float theta)
{
  return 331.5 + (0.6 * theta);
}

// precice sonic speed
float sonicSpeedExt(float theta)
{
  return 331.5 * sqrt(1 + (theta / 273.15));
}

