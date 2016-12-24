using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheroMap : MonoBehaviour {

    int rows;
    int cols;

    public float[,] pheromoneTable;

    // Use this for initialization
    void Start () {
        rows = (int) GetComponent<MapGenerator>().mapSize.x;
        cols = (int)GetComponent<MapGenerator>().mapSize.y;

        pheromoneTable = new float[rows, cols];
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
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

                Debug.Log(pointx + ", " + pointz + " Pheromone Value: " + pheromoneTable[pointx, pointz]);
            }
        }
    }
}
