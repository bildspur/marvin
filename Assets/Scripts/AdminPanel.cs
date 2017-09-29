using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminPanel : MonoBehaviour
{

    private Text sensitivityValue;
    private Slider sensitivitySlider;

    private JumpDetector detector;

    // Use this for initialization
    void Start()
    {
        sensitivityValue = GameObject.Find("SensitivityValue").GetComponent<Text>();
        sensitivitySlider = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
        detector = GameObject.Find("Jump Detection").GetComponent<JumpDetector>();

        sensitivitySlider.value = detector.threshold;
        sensitivitySlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        ValueChangeCheck();
    }

    public void ValueChangeCheck()
    {
        detector.threshold = sensitivitySlider.value;
        sensitivityValue.text = sensitivitySlider.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
