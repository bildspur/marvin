public class RingBuffer
{
  float[] buffer;
  int index = -1;
  
  public RingBuffer(int size)
  {
      buffer = new float[size];
  }
  
  public void add(float value)
  {
    index = (index + 1) % buffer.length;
    buffer[index] = value;
  }
}