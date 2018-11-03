using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferControl : MonoBehaviour {

    public float knockbackTime;
    public Vector2 knockbackDir;
    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
 
    }

    // Update is called once per frame
    void FixedUpdate () {
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
}
