using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallLogic : MonoBehaviour, IJump
{

	Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown ("space"))
			JumpOnce ();
	}

	public void JumpOnce ()
	{
		rb.AddForce (Vector3.up * 300);
	}

	public void Jump ()
	{
		rb.AddForce (Vector3.up * 500);
	}
}
