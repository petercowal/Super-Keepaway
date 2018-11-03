using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTexts : MonoBehaviour {

    float t = 0f;

    Vector3 scale;

	// Use this for initialization
	void Start () {
        scale = transform.localScale;
        transform.localScale = new Vector2(0.01f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
        t = t + Time.deltaTime;
        if (t < 0.25f)
        {
            transform.localScale = scale * 4 * t;
        }
        else if (t < 1)
        {
            transform.localScale = scale;
        }
        else if (t < 1.5f)
        {
            transform.localScale = scale * 2 * (1.5f - t);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
