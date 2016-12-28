using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheroMap : MonoBehaviour {

    int rows;
    int cols;

    // stores tiles in an array, index starts at 0
    Transform[,] mapTiles;
    Color lerpedColor;
    Color originalColor;
    
    // used to lerp color
    public float maxPheroValue;
    public Color finalColor;

    // pheromone data in an array, index starts at 1
    // index of this matches with tile index
    public float[,] pheromoneTable;

    // Use this for initialization
    void Start () {

        // delay because map has to be generated first
        Invoke("delayedStart", 0.5f);
    }

    void delayedStart () {
        mapTiles = GetComponent<MapGenerator>().mapTile;
        originalColor = GetComponent<MapGenerator>().tilePrefab.gameObject.GetComponent<Renderer>().sharedMaterial.color;

        rows = (int)GetComponent<MapGenerator>().mapSize.x;
        cols = (int)GetComponent<MapGenerator>().mapSize.y;

        pheromoneTable = new float[rows + 1, cols + 1];
        for (int i = 0; i < rows+1; i++) {
            for (int j = 0; j < cols+1; j++) {
                pheromoneTable[i, j] = 0;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        GetTileCoord();
	}

    void GetTileCoord () {
        if (Input.GetButtonDown("Fire1")) {

            // Create ray with direction
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Convert to world coordinates and draw line
            //Vector3 mposition = Input.mousePosition;
            //mposition.z = 25;
            //mposition = Camera.main.ScreenToWorldPoint(mposition);
            //Debug.DrawLine(ray.origin, mposition, Color.blue);

            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance)) {
                Vector3 point = ray.GetPoint(rayDistance);
                int pointx = Mathf.CeilToInt(point.x);
                int pointz = Mathf.CeilToInt(point.z);

                Debug.DrawLine(ray.origin, point, Color.blue);

                pheromoneTable[pointx, pointz]++;
                //DecreasePheromones(0.5f);

                UpdateTileColor(pointx, pointz);

                Debug.Log(pointx + ", " + pointz + " Pheromone Value: " + pheromoneTable[pointx, pointz]);
            }
        }
    }

    public void UpdateTileColor (int tileX, int tileZ) {
        Renderer rend = mapTiles[tileX, tileZ].GetComponent<Renderer>();
        lerpedColor = Color.Lerp(originalColor, finalColor, pheromoneTable[tileX, tileZ] / maxPheroValue);
        rend.material.color = lerpedColor;
    }

    public void DecreasePheromones (float decreaseVal) {
        for (int i = 1; i < rows+1; i++) {
            for (int j = 1; j < cols+1; j++) {
                pheromoneTable[i, j] = pheromoneTable[i, j] - decreaseVal;
                UpdateTileColor(i, j);
            }
        }
    }
}
