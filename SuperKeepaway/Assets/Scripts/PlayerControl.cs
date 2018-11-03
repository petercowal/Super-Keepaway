using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    public bool jump = false;
    public float accelerate = 100f;
    public float airAccelerate = 10f;
    public float runSpeed = 10f;
    public float jumpSpeed = 10f;

    private bool grounded = false;
    private Rigidbody2D rb;
    private BoxCollider2D col;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.BoxCast(transform.position, col.size, 0, Vector2.down, 0.05f, LayerMask.GetMask("Ground"));


        float h = Input.GetAxis("Horizontal");

        if (Mathf.Abs(h) > 0.5f)
        {
            rb.velocity = new Vector2(Mathf.Sign(h) * runSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
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
                if (Input.GetButton("Jump"))
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
