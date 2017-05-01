import processing.serial.*;

Serial port;
float lastValue = 0;

final String ARDUINO_MEGA = "cu.usb";

RingBuffer buffer;
JumpDetector detector = new JumpDetector();

void setup()
{
  size(500, 500, FX2D);
  
  buffer = new RingBuffer(1000);

  String[] interfaces = Serial.list();
  String portName = interfaces[0];
  for (int i = 0; i < interfaces.length; i++)
  {
    if (interfaces[i].contains(ARDUINO_MEGA))
    {
      println("Found: [" + i + "] " + interfaces[i]);
      portName = interfaces[i];
    }
  }

  println("Connecting to: " + portName);
  port = new Serial(this, portName, 115200);
}

void draw()
{
  background(55);
  
  float distance = getDistance();
  buffer.add(distance);
  
  float lx = 0;
  float ly = height;
  
  float h = 50;
  
  // visualize buffer
  for(int i = 1; i < buffer.buffer.length; i++)
  {
     float value =  buffer.buffer[i];
     
     float x = map(i, 0, buffer.buffer.length, 0, width);
     float y = map(value, 4, 10, height - 200, h);
     
     noFill();
     stroke(0, 255, 0);
     strokeWeight(2f);
     
     line(lx, ly, x, y);
     
     if(buffer.index == i)
     {
        stroke(255, 0, 0);
        strokeWeight(1f);
        line(x, h, x, height);
     }
     
     lx = x;
     ly = y;
  }
  
  // jump detector
  detector.process(distance);
  detector.visualiseFFT();

  fill(255);
  textSize(20);
  textAlign(CENTER, CENTER);
  text(distance + " cm", width / 2, 20);
}

float getDistance()
{
  if (port.available() > 0) 
  {
    String serialValue = port.readStringUntil('\n');
    if (serialValue != null)
    {
      lastValue = float(serialValue.trim());
    }
  }

  return lastValue;
}