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

	// Use this for initialization
	void Start ()
	{
		jump = jumpGameObject.GetComponent (typeof(IJump)) as IJump;
		input = serialInputGameObject.GetComponent (typeof(SerialInput)) as SerialInput;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("Value: " + input.Value);


		if (input.Value > Threshold && !isJumping) {
			jump.Jump ();
			isJumping = true;
		}

		if (input.Value <= Threshold)
			isJumping = false;
	}
}
