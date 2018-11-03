using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentBar : MonoBehaviour {

    public float value = 3;

    public Transform segment1;
    public Transform segment2;
    public Transform segment3;
	
	// Update is called once per frame
	void Update () {
        segment1.localScale = new Vector2(Mathf.Clamp(value, 0, 1), 1);
        segment2.localScale = new Vector2(Mathf.Clamp(value - 1, 0, 1), 1);
        segment3.localScale = new Vector2(Mathf.Clamp(value - 2, 0, 1), 1);
    }
}
