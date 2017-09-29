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

    List<string> ports = new List<string>(SerialPort.GetPortNames());

    // Use this for initialization
    void Start()
    {
        Debug.Log("Found " + ports.Count + " serial devices...");
        sensitivityValue = GameObject.Find("SensitivityValue").GetComponent<Text>();
        sensitivitySlider = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
        serialPortSelection = GameObject.Find("SerialPortSelection").GetComponent<Dropdown>();

        detector = GameObject.Find("Jump Detection").GetComponent<JumpDetector>();
        serialInput = GameObject.Find("SerialInput").GetComponent<SerialInput>();

        sensitivitySlider.value = detector.threshold;

        serialPortSelection.options.Clear();
        serialPortSelection.AddOptions(ports);

        sensitivitySlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        serialPortSelection.onValueChanged.AddListener(delegate { SerialPortChange(); });

        ValueChangeCheck();
    }

    public void ValueChangeCheck()
    {
        detector.threshold = sensitivitySlider.value;
        sensitivityValue.text = sensitivitySlider.value.ToString();
    }

    public void SerialPortChange()
    {
        Debug.Log("New Port: " + serialPortSelection.itemText);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
