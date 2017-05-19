using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchController : MonoBehaviour {
	public Camera topCamera;
	public Camera MainCamera;

	private float pushTime = 0.31f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Tab)  && pushTime > 0.3f) {
			pushTime = 0;
			if (topCamera.depth != 1) {
				topCamera.depth = 1;
			} else {
				topCamera.depth = -1;
			}
		}
		pushTime += Time.deltaTime;
		
	}
}
