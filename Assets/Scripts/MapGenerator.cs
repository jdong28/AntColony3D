using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform tilePrefab;
    public Vector2 mapSize;

    [Range(0, 1)]
    public float borderPercentage;

    // Use this for initialization
    void Start () {
        DrawMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DrawMap () {

        Transform mapHolder = new GameObject("MapHolder").transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++) {
            for (int y = 0; y < mapSize.y; y++) {
                Vector3 newPosition = new Vector3(0.5f + x, 0, 0.5f + y);
                Transform newTile = Instantiate(tilePrefab, newPosition, Quaternion.Euler(90, 0, 0));
                newTile.localScale = Vector3.one * (1 - borderPercentage);
                newTile.parent = mapHolder;
            }
        }
    }
}
