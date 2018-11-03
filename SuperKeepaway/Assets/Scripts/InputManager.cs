using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    // Use this for initialization
    void Start() {
        foreach (string s in Input.GetJoystickNames()){ 
            Debug.Log(s);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
