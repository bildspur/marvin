using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLogic : MonoBehaviour {
	public float rotationSpeed = 1;
	GameObject planet;

	// Use this for initialization
	void Start () {
		planet = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		planet.transform.Rotate (0, 0, rotationSpeed);
	}
}
