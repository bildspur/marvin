using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    Rigidbody2D rigid;
    GameObject planet;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        planet = GameObject.FindGameObjectWithTag("Planet");

        // set initial force
        rigid.AddForce(Vector2.up * 150, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
		// look at 2d
		LookAt2D(planet.transform);
    }

	public void OnCollisionEnter2D(Collision2D other)
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyKiller"))
		{
            Destroy(this.gameObject);
        }
    }

	void LookAt2D(Transform target)
	{
        Quaternion rotation = Quaternion.LookRotation
             (target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
	}
}
