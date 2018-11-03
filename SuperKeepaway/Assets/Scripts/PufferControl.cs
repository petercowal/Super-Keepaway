using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferControl : MonoBehaviour {

    public float knockbackTime;
    public Vector2 knockbackDir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;
            GetComponent<Rigidbody2D>().AddForce(knockbackDir);
        }
	}
}
