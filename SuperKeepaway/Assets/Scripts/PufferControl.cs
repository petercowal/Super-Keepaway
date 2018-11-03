using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferControl : MonoBehaviour {

    public float knockbackTime;
    public Vector2 knockbackDir;
    private Animator animator;


    public int team = 0;

    private Rigidbody2D rb;

    public SpriteRenderer aura;

    private bool active = true;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (team)
        {
            case 1:
                aura.color = new Color(245, 153, 229);
                break;
            case 2:
                aura.color = new Color(0, 154, 217);
                break;
            case 3:
                aura.color = new Color(248, 144, 92);
                break;
            case 4:
                aura.color = new Color(99, 111, 232);
                break;
            default:
                aura.color = new Color(200, 210, 220);
                break;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (team == 0)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 0.8f;
        }

        if (knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;
            GetComponent<Rigidbody2D>().AddForce(knockbackDir);
            animator.SetInteger("state", 1);

            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(knockbackDir.y, knockbackDir.x)*Mathf.Rad2Deg, Vector3.forward);
        }
        else
        {
            animator.SetInteger("state", 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (active && collision.transform.CompareTag("Floor"))
        {
            //display winner!
            active = false;
        }
    }
}
