using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour {

   public float maxGravDist = 4.0f;
     public float maxGravity = 35.0f;
 
     GameObject[] planets;

	 Rigidbody2D rigid;
 
     void Start () {
         planets = GameObject.FindGameObjectsWithTag("Planet");
		 rigid = GetComponent<Rigidbody2D>();
     }
     
     void FixedUpdate () {
         foreach(var planet in planets) {
             float dist = Vector3.Distance(planet.transform.position, transform.position);

                Debug.Log("checking $planet");

             if (dist <= maxGravDist) {
                 Vector3 v = planet.transform.position - transform.position;
                 rigid.AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);
             }
         }
     }
	
	// Update is called once per frame
	void Update () {
		
	}
}
