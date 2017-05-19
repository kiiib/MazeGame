using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour {
	public Material BlueWall;
	void OnCollisionEnter(Collision other) {
		this.GetComponent<Renderer>().material = BlueWall;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
