using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public int maxFoodValue;
    int currentFoodValue;

    float maxHeight;

	// Use this for initialization
	void Start () {
        currentFoodValue = maxFoodValue;
        maxHeight = transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        currentFoodValue = currentFoodValue - 1;
        Vector3 newScale = transform.localScale;
        float newHeight = maxHeight * currentFoodValue / maxFoodValue;

        if (newHeight <= 0) {
            Destroy(gameObject);
        }

        newScale.y = newHeight;
        transform.localScale = newScale;
    }
}
