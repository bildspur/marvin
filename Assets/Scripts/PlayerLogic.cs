using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour, IJump
{
    Rigidbody2D rb;
    Animator animator;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            JumpOnce();
    }

    public void JumpOnce()
    {
        animator.SetBool("Jumping", true);
        rb.AddForce(transform.up * 300);
    }

    public void Jump()
    {
        rb.AddForce(transform.up * 500);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Planet"))
		{
            animator.SetBool("Jumping", false);
        }
    }
}
