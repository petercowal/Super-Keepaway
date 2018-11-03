﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    public bool jump = false;
    public float accelerate = 100f;
    public float airAccelerate = 25f;
    public float runSpeed = 10f;
    public float jumpSpeed = 12f;

    private bool grounded = false;

    private float baseAirVelocity;

    private Rigidbody2D rb;
    private CapsuleCollider2D col;


    public string joystickID = "1";

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (grounded)
        {
            if (Input.GetButtonDown("Jump_" + joystickID))
            {
                jump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.BoxCast(transform.position, col.size, 0, Vector2.down, 0.05f, LayerMask.GetMask("Ground"));


        float h = Input.GetAxis("Horizontal_" + joystickID);

        if (Mathf.Abs(h) > 0.5f)
        {
            if (grounded)
            {
                rb.velocity = new Vector2(Mathf.Sign(h) * runSpeed, rb.velocity.y);
            }
            else
            {
                rb.AddForce(Mathf.Sign(h) * airAccelerate * Vector2.right);
                if (Mathf.Abs(rb.velocity.x) > runSpeed)
                {
                    rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * runSpeed, rb.velocity.y);
                }
            }
        }
        else if (grounded)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        } else
        {
            if (Mathf.Abs(rb.velocity.x) > 0.1f)
                rb.AddForce(-Mathf.Sign(rb.velocity.x) * airAccelerate * Vector2.right);
        }

        if (grounded)
        {
            rb.gravityScale = 3;
            if (jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jump = false;
            }
        }
        else
        {
            if (rb.velocity.y > 0 )
            {
                if (Input.GetButton("Jump_" + joystickID) && rb.velocity.y > 3)
                {
                    rb.gravityScale = 2;
                }
                else
                {
                    rb.gravityScale = 10;
                }
            }
            else
            {
                rb.gravityScale = 4;
            }
        }
    }
}
