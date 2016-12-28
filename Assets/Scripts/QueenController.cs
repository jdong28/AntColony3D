using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenController : MonoBehaviour {

    public Transform ants;
    public Vector3 spawnPosition;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            CreateAnt();         
        }
    }

    void CreateAnt() {
        GameObject ant = (GameObject)Instantiate(ants.gameObject, spawnPosition, Quaternion.Euler(0, 0, 0));
    }
}
