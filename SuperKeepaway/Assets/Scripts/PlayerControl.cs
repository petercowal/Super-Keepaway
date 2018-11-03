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

    public Transform groundCheck;

    public string joystickID = "1";

    public int team = 1;

    public float knockbackTime = 0f;


    public int animationState = 0;

    private Animator animator;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (grounded && knockbackTime <= 0f)
        {
            if (Input.GetButtonDown("Jump_" + joystickID))
            {
                jump = true;
            }
        }
        animator.SetInteger("state", animationState);
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.GetMask("Ground"));

        float h = Input.GetAxis("Horizontal_" + joystickID);

        if (knockbackTime <= 0f)
        {

            if (Mathf.Abs(h) > 0.5f)
            {

                transform.localScale = new Vector2(Mathf.Sign(h) * Mathf.Abs(transform.localScale.x), transform.localScale.y);

                if (grounded)
                {
                    rb.velocity = new Vector2(Mathf.Sign(h) * runSpeed, rb.velocity.y);
                    animationState = 1;
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
                animationState = 0;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
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
                if (rb.velocity.y > 0)
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
        } else
        {
            rb.gravityScale = 2;
            knockbackTime -= Time.deltaTime;
        }
    }
}
