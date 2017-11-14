using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour {

	public float lifetime = 1.0f;

	// Use this for initialization
	void Start () {
		Destroy(gameObject,lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
