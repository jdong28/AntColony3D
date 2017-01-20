using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform tilePrefab;
    public Vector2 mapSize;
    public Transform borderPrefab;
    public Transform [,] mapTile;

    FoodController foodController;

    [Range(0, 1)]
    public float borderPercentage;

    // Use this for initialization
    void Start () {
        mapTile = new Transform[(int) mapSize.x + 1 , (int) mapSize.y + 1 ];
        DrawMap();
        foodController = GetComponent<FoodController>();
        foodController.GenerateFood(10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DrawMap () {

        Transform mapHolder = new GameObject("MapHolder").transform;
        mapHolder.parent = transform;

        Transform borderHolder = new GameObject("BorderHolder").transform;
        borderHolder.parent = transform;

        for (int y = 1; y < mapSize.y + 1; y++) {
            for (int x = 1; x < mapSize.x + 1; x++) {
                Vector3 newPosition = new Vector3(-0.5f + x, 0, -0.5f + y);
                Transform newTile = Instantiate(tilePrefab, newPosition, Quaternion.Euler(90, 0, 0));
                newTile.localScale = Vector3.one * (1 - borderPercentage);
                newTile.parent = mapHolder;
                mapTile[x, y] = newTile;

                //Generate Border Tiles
                if (x == 1 || x == mapSize.x || y ==1 || y == mapSize.y) {
                    Vector3 newBorderPosition = new Vector3(newPosition.x, 0.5f, newPosition.z);
                    Transform newBorder = Instantiate(borderPrefab, newBorderPosition, Quaternion.Euler(0,0,0));
                    newBorder.parent = borderHolder;
                }
            }
        }
    }
}
