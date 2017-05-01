using UnityEngine;

public class JumpDetector : MonoBehaviour
{
    public GameObject jumpGameObject;
    public GameObject serialInputGameObject;

    public float threshold = 1.0f;

    public int bufferSize = 16;

    public int frequencyBand = 3;

    IJump jump;
    SerialInput input;

    bool isJumping = false;

    RingBuffer<float> buffer;

    Complex[] fftBuffer;

    float maxAmp = 0;

    int c = 0;

    // Use this for initialization
    void Start()
    {
        // init buffers
        buffer = new RingBuffer<float>(bufferSize);
        fftBuffer = new Complex[bufferSize];

        jump = jumpGameObject.GetComponent(typeof(IJump)) as IJump;
        input = serialInputGameObject.GetComponent(typeof(SerialInput)) as SerialInput;
    }

    // Update is called once per frame
    void Update()
    {
        DetectJump(input.Value);
    }

    void DetectJump(float value)
    {
        buffer.Add(value);

        // calculate fft
        PrepareBuffer();
        FFT.CalculateFFT(fftBuffer, false);

        // check frequency band
        var amp = (float)fftBuffer[frequencyBand].magnitude * 2;

        // show debug info
        var scoreText = (GameObject.Find("DebugText").GetComponent("GUIText") as GUIText);
        scoreText.text = "Input: " + input.Value + "\nFFT: " + amp + "\nMax: " + maxAmp;

        if (maxAmp < amp)
            maxAmp = amp;

        if (c == 100)
        {
            c = 0;
            maxAmp = 0;
        }

        // jump
        if (amp >= threshold)
        {
            jump.Jump();
        }

        c++;
    }

    void PrepareBuffer()
    {
        for (int i = 0; i < bufferSize; i++)
        {
            fftBuffer[i] = new Complex(buffer[i], 0);
        }
    }
}
