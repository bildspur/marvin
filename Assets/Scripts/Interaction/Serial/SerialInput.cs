using UnityEngine;
using System.Threading;
using System.IO.Ports;

public class SerialInput : MonoBehaviour
{
    public volatile float Value = 0;

    public bool isOn = true;

    public string deviceName = "tty.usbmodem1461";

    bool running = false;

    Thread serialThread;

    SerialPort serialPort;

    // Use this for initialization
    void Start()
    {
        // if serial monitor already exists
        var sm = GameObject.Find(this.gameObject.name);
        if (sm != null && sm != this.gameObject)
        {
            Destroy(this.gameObject);
        }

        if (!isOn)
            return;

        serialThread = new Thread(new ThreadStart(SerialRun));
        running = true;

        serialPort = new SerialPort();
        serialPort.PortName = "/dev/" + deviceName;
        serialPort.BaudRate = 115200;
        serialPort.Open();

        serialThread.Start();
        Debug.Log("serial started...");

        running = true;
    }

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationQuit()
    {
        Stop();
    }
    void OnDisable()
    {
        Stop();
    }

    void Stop()
    {
        if (running)
        {
            running = false;
            serialThread.Join();
        }
    }

    void SerialRun()
    {
        while (running)
        {
            Thread.Sleep(20);
            var input = serialPort.ReadLine().Trim();
            var num = 0f;

            if (float.TryParse(input, out num))
                Value = num;
        }
        serialPort.Close();
        serialPort.Dispose();
        Debug.Log("Serial closed!");
    }
}
