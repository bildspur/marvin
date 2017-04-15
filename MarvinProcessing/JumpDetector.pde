import ddf.minim.analysis.*;

public class JumpDetector
{

  FFT fft;
  RingBuffer buffer;

  public JumpDetector()
  {
    buffer = new RingBuffer(8);
    fft = new FFT(buffer.buffer.length, 60);
  }

  public void process(float value)
  {
    buffer.add(value);
    fft.forward(buffer.buffer);
  }

  public void visualiseFFT()
  {
    int specSize = fft.specSize();
    for (int i = 1; i < specSize; i++)
    {
      float amp = fft.getBand(i);

      float x = map(i, 0, specSize, 20, width - 20);
      float y = map(amp, 0, 1, height - 20, height - 200);

      stroke(255, 0, 255);
      strokeWeight(10f);
      strokeCap(SQUARE);
      line(x, y, x, height - 20);
    }

    stroke(255);
    strokeWeight(1f);
    strokeCap(SQUARE);
    line(0, height - 200, width, height - 200);
    line(0, height - 20, width, height - 20);

    float amp = fft.getBand(specSize - 1);
    println("Value: " + amp);
  }
}