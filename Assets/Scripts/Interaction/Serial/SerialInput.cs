using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO.Ports;

public class SerialInput : MonoBehaviour
{
	public volatile int Value = 0;

	public bool isOn = true;

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
		serialPort.PortName = "/dev/tty.usbmodem1411";
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
	}

	void SerialRun ()
	{
		while (running) {
			Thread.Sleep (10);
			var input = serialPort.ReadLine ().Trim ();
			var num = 0;

			if (int.TryParse (input, out num))
				Value = num;
		}
		serialPort.Close ();
	}
}
