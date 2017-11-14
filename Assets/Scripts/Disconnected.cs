using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disconnected : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("Disconnected from server: " + info);
		Destroy(gameObject);
        
    }
}
