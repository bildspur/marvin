using UnityEngine;
using System.Threading;
using System.IO.Ports;

public class SerialInput : MonoBehaviour
{
	public volatile int Value = 0;

	public bool isOn = true;

	public string deviceName = "tty.usbmodem1461";

	bool running = true;

	Thread serialThread;

	SerialPort serialPort;

	// Use this for initialization
	void Start ()
	{
		if(!isOn)
			return;

		serialThread = new Thread (new ThreadStart (SerialRun));
		running = true;

		serialPort = new SerialPort ();
		serialPort.PortName = "/dev/" + deviceName;
		serialPort.BaudRate = 115200;
		serialPort.Open ();

		serialThread.Start ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnApplicationQuit ()
	{
		running = false;
		serialThread.Join();
	}

	void SerialRun ()
	{
		while (running) {
			Thread.Sleep (20);
			var input = serialPort.ReadLine ().Trim ();
			var num = 0;

			if (int.TryParse (input, out num))
				Value = num;
		}
		serialPort.Close ();
		serialPort.Dispose();
		Debug.Log("Serial closed!");
	}
}
