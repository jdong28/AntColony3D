using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float zoomSensitivity = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        float yVal = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;

        // xyz does not matchup because camera has a rotation on it
        if(transform != null) {
            transform.Translate(new Vector3(xAxisValue, zAxisValue, 0));
            transform.Translate(new Vector3(0, 0, yVal));
        }
    }
}
