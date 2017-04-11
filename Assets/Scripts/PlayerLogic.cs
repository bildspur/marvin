using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour, IJump
{
	Rigidbody2D rb;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown ("space"))
			JumpOnce ();
	}

	public void JumpOnce ()
	{
		rb.AddForce (transform.up * 300);
	}

	public void Jump ()
	{
		rb.AddForce (transform.up * 500);
	}
}
