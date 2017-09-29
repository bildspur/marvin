using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class AdminPanel : MonoBehaviour
{

    private Text sensitivityValue;
    private Slider sensitivitySlider;

    private JumpDetector detector;

    private SerialInput serialInput;

    private Dropdown serialPortSelection;

    List<string> ports;

    // Use this for initialization
    void Start()
    {
        ports = GetSerialPortNames();

        Debug.Log("Found " + ports.Count + " serial devices...");
        sensitivityValue = GameObject.Find("SensitivityValue").GetComponent<Text>();
        sensitivitySlider = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
        serialPortSelection = GameObject.Find("SerialPortSelection").GetComponent<Dropdown>();

        detector = GameObject.Find("Jump Detection").GetComponent<JumpDetector>();
        serialInput = GameObject.Find("SerialInput").GetComponent<SerialInput>();

        sensitivitySlider.value = detector.threshold;

        serialPortSelection.options.Clear();
        ports.ForEach(e => serialPortSelection.options.Add(new Dropdown.OptionData(e)));
        serialPortSelection.captionText.text = serialInput.deviceName;

        sensitivitySlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        serialPortSelection.onValueChanged.AddListener(delegate { SerialPortChange(); });

        ValueChangeCheck();
    }

    List<string> GetSerialPortNames()
    {
        int p = (int)System.Environment.OSVersion.Platform;
        var serialPorts = new List<string>();

        // Are we on Unix?
        if (p == 4 || p == 128 || p == 6)
        {
            var ttys = System.IO.Directory.GetFiles("/dev/", "tty.*");
            foreach (string dev in ttys)
            {
                if (dev.StartsWith("/dev/tty."))
                    serialPorts.Add(dev);
            }
        }

        return serialPorts;
    }

    public void ValueChangeCheck()
    {
        detector.threshold = sensitivitySlider.value;
        sensitivityValue.text = sensitivitySlider.value.ToString();
    }

    public void SerialPortChange()
    {
        var device = serialPortSelection.options[serialPortSelection.value].text;
        serialInput.deviceName = device;
        serialInput.RestartSerial();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
