using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {

    public Transform food;
    Vector2 mapBounds;

    // Use this for initialization
    void Start () {
        MapGenerator map = GetComponent<MapGenerator>();
        mapBounds = map.mapSize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateFood(int amount) {
        List<Vector2> randomPositions = new List<Vector2>();
        
        //Generate x = amount number of unique random numbers
        for (int x = 0; x < amount; x++) {
            int randomX = Random.Range(2, (int)mapBounds.x);
            int randomY = Random.Range(2, (int)mapBounds.y);
            Vector2 randomPos = new Vector2(randomX, randomY);

            while (randomPositions.Contains(randomPos)) {
                randomX = Random.Range(2, (int)mapBounds.x);
                randomY = Random.Range(2, (int)mapBounds.y);
                randomPos.x = randomX;
                randomPos.y = randomY;
            }
            randomPositions.Add(randomPos);
        }

        for (int n = 0; n < amount; n++) {
            float posX = randomPositions[n].x + 0.5f;
            float posY = randomPositions[n].y + 0.5f;

            Instantiate(food, new Vector3(posX, 0.5f, posY), Quaternion.identity);
        }
    }
}
