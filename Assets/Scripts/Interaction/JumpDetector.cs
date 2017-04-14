using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetector : MonoBehaviour
{
    public GameObject jumpGameObject;
    public GameObject serialInputGameObject;

    public int Threshold = 15;

    IJump jump;
    SerialInput input;

    bool isJumping = false;

    public int low = 5;
    public int high = 8;

    int lowValue = 0;
    int highValue = 0;

    bool lowTriggered = false;
    bool highTriggered = false;


    // Use this for initialization
    void Start()
    {
        jump = jumpGameObject.GetComponent(typeof(IJump)) as IJump;
        input = serialInputGameObject.GetComponent(typeof(SerialInput)) as SerialInput;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log ("Value: " + input.Value);

		(GameObject.Find("DebugText").GetComponent("GUIText") as GUIText).text = "Input: " + input.Value;

        DetectJump(input.Value);
    }

    void DetectJump(int value)
    {
        if (value <= low)
        {
            lowTriggered = true;
            highTriggered = false;

            lowValue = value;
        }

        if (value >= high)
        {
            highTriggered = true;
        }

        if (highTriggered && lowTriggered)
        {
            highValue = value;
			int height = highValue - lowValue;

			if(height < high)
			{
				// jump
				Debug.Log("Jump! -> Height: " + height);
				jump.Jump();
			}

            // reset
            lowTriggered = false;
            highTriggered = false;
        }
    }
}
