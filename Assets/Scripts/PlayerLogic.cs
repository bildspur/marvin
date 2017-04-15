﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour, IJump
{
    Rigidbody2D rb;
    Animator animator;

    bool jumping = false;

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
            Jump();
    }

    public void Jump()
    {
        if (!jumping)
        {
            jumping = true;
            animator.SetBool("Jumping", true);
            rb.AddForce(transform.up * 300);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            animator.SetBool("Jumping", false);
            jumping = false;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            KillPlayer(other);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            // unload serial port

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void KillPlayer(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = collider.bounds.center;

        bool right = contactPoint.x > center.x;
        bool top = contactPoint.y > center.y;

        // enemy from the side
        if (top == false)
        {
            var gravity = GetComponent("PlanetGravity") as PlanetGravity;
            gravity.maxGravity = 0;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddTorque(15, ForceMode2D.Force);
        }
    }
}
