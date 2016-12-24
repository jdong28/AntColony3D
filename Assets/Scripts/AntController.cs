using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {


    Rigidbody myRB;
    Vector3 newPos;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() {
        myRB.MovePosition(newPos);
    }
}
