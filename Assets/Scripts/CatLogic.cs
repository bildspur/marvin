using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatLogic : MonoBehaviour
{

    Rigidbody2D rb;

    bool jumping = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Jump()
    {
        if (!jumping)
        {
            jumping = true;
            //animator.SetTrigger("StartJump");
            rb.AddForce(transform.up * 300);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            //animator.SetTrigger("StopJump");
            jumping = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("enemy hit by marvin");
        }
    }
}
