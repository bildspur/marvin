using UnityEngine;

public class JumpDetector : MonoBehaviour
{
    public GameObject jumpGameObject;
    public GameObject serialInputGameObject;

    public float threshold = 2.0f;

    public int bufferSize = 16;

    IJump jump;
    SerialInput input;

    bool isJumping = false;

    RingBuffer<float> buffer;

    Complex[] fftBuffer;

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
        (GameObject.Find("DebugText").GetComponent("GUIText") as GUIText).text = "Input: " + input.Value;
        DetectJump(input.Value);
    }

    void DetectJump(float value)
    {
        buffer.Add(value);

        // calculate fft
        PrepareBuffer();
        FFT.CalculateFFT(fftBuffer, false);

        for (int i = 0; i < fftBuffer.Length / 2; i++) // plot only the first half
        {
            // multiply the magnitude of each value by 2
            Debug.DrawLine(new Vector3(i, 4), new Vector3(i, 4 + (float)fftBuffer[i].magnitude * 2), Color.white);
        }
        // jump
        /*  
        Debug.Log("Jump! -> Height: ");
        jump.Jump();
		*/
    }

    void PrepareBuffer()
    {
        for (int i = 0; i < bufferSize; i++)
        {
            fftBuffer[i] = new Complex(buffer[i], 0);
        }
    }
}
