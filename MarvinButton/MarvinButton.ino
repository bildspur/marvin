/*
   Button.pde
*/

#define BUTTON_PIN 10
#define BUTTON_LED 7

bool isReady = false;
int i = 0;

void setup() {
  Serial.begin(9600);

  pinMode(BUTTON_PIN, INPUT_PULLUP);
  pinMode(BUTTON_LED, OUTPUT);

  // turn on button
  digitalWrite(BUTTON_LED, HIGH);
}

void loop() {
  if (isReady && digitalRead(BUTTON_PIN) == LOW) {
    digitalWrite(BUTTON_LED, LOW);
    sendMessage();
    isReady = false;
    usbMIDI.sendNoteOn(0, 127, 0);
  }

  if (digitalRead(BUTTON_PIN) == HIGH)
  {
    if (!isReady)
    {
      usbMIDI.sendNoteOff(0, 0, 0);
      digitalWrite(BUTTON_LED, HIGH);
    }

    // reset
    isReady = true;
  }

  delay(10);
}

void sendMessage()
{
  // send raute #
  /*
    Keyboard.set_modifier(MODIFIERKEY_ALT)s
    Keyboard.set_key1(KEY_3);
    Keyboard.send_now()
  */
  //Keyboard.print("S");
}