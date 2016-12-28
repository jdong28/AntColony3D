using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {


    Rigidbody myRB;

    float[,] pTable;
    PheroMap pMap;

    bool moving;
    Vector3 newPos;
    Vector3 prevPos;
    List<MovementDirection<Vector3, float>> moveList;

    public float speed = 1f;

    // Use this for initialization
    void Start() {
        InitializeMoves();

        GameObject map = GameObject.Find("Map");

        pMap = map.GetComponent<PheroMap>();
        pTable = map.GetComponent<PheroMap>().pheromoneTable;
        myRB = GetComponent<Rigidbody>();

        prevPos = myRB.position;
        newPos = FindNextTile(prevPos);
        depositPheromone();
        moving = true;
    }

    void InitializeMoves() {
        moveList = new List<MovementDirection<Vector3, float>>() {
        new MovementDirection<Vector3, float>(Vector3.forward, 0),
        new MovementDirection<Vector3, float>(Vector3.back, 0),
        new MovementDirection<Vector3, float>(Vector3.left, 0),
        new MovementDirection<Vector3, float>(Vector3.right, 0),
        };
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetButtonDown("Fire1")) {

        //    // Create ray with direction
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    // Convert to world coordinates and draw line
        //    //Vector3 mposition = Input.mousePosition;
        //    //mposition.z = 25;
        //    //mposition = Camera.main.ScreenToWorldPoint(mposition);
        //    //Debug.DrawLine(ray.origin, mposition, Color.blue);

        //    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //    float rayDistance;

        //    if (groundPlane.Raycast(ray, out rayDistance)) {
        //        Vector3 point = ray.GetPoint(rayDistance);
        //        int pointx = Mathf.CeilToInt(point.x);
        //        int pointz = Mathf.CeilToInt(point.z);
        //    }
        //}
    }

    private void FixedUpdate() {

        // not moving, deposit pheromone
        if (!moving) {
            newPos = FindNextTile(prevPos);
            prevPos = myRB.position;
            moving = true;

            depositPheromone();

        }
        // moving
        else if ((myRB.position - newPos).sqrMagnitude > float.Epsilon) {
            //keep moving
            float step = speed * Time.deltaTime;
            Vector3 nextFramePosition = Vector3.MoveTowards(myRB.position, newPos, step);
            myRB.MovePosition(nextFramePosition);
        }
        // arrived at next tile
        else {
            moving = false;
        }
    }

    // add pheromone and update color of tile
    void depositPheromone() {
        Vector3 curTilePos = GetTileCoord(myRB.position);
        pTable[(int)curTilePos.x, (int)curTilePos.z]++;

        pMap.UpdateTileColor((int) curTilePos.x, (int) curTilePos.z);

        Debug.Log("deposited pheromone");
    }

    // find neighbour tile position in world coords with highest pheromone value
    // highest pheromone value for now..
    Vector3 FindNextTile(Vector3 previousPos) {
        Vector3 curTilePos = GetTileCoord(myRB.position);
        int x = (int)curTilePos.x;
        int z = (int)curTilePos.z;

        int move_x = 0;
        int move_z = 0;
        float pheroTotal = 0;

        MovementDirection<Vector3, float> moveToBeDeleted = new MovementDirection<Vector3, float>();
        foreach (MovementDirection<Vector3, float> move in moveList) {
            Vector3 testPos = new Vector3(myRB.position.x + (int)move.Direction.x, 0.5f, myRB.position.z + (int)move.Direction.z);

            // if move results in previous state, remove from list of possible moves
            if (testPos == previousPos) {
                moveToBeDeleted = move;
            }
        }
        if (moveToBeDeleted != null) {
            moveList.Remove(moveToBeDeleted);
        }

        // random number [0,1]
        float diceroll = Random.value;

        // calculate sum of all neighbour tile pheromone values
        for (int i = -1; i <= 1; i += 1) {
            for (int j = -1; j <= 1; j += 1) {
                if (i == 0 || j == 0) {
                    pheroTotal = pheroTotal + pTable[x + i, z + j];
                }
            }
        }

        // calculate cumulative probability of each move
        foreach (MovementDirection<Vector3, float> move in moveList) {
            if (pheroTotal != 0) {
                // probability = phero value of 1 tile / combined phero value of neighbour tiles
                float prob = pTable[x + (int)move.Direction.x, z + (int)move.Direction.z] / pheroTotal;
                move.Probability = prob;
            }
        }

        // choose move using probability
        foreach (MovementDirection<Vector3, float> move in moveList) {
            if (diceroll <= move.Probability) {
                move_x = (int) move.Direction.x;
                move_z = (int) move.Direction.z;
            }
        }

        // if all moves equally likely then choose randomly
        if (move_x == 0 && move_z == 0) {
            int diceroll2 = Random.Range(0, moveList.Count - 1);
            move_x = (int) moveList[diceroll2].Direction.x;
            move_z = (int) moveList[diceroll2].Direction.z;
        }

        // reinitialize move list because one move was removed
        InitializeMoves();

        Vector3 curNewPos = new Vector3(myRB.position.x + move_x, 0.5f, myRB.position.z + move_z);
        return curNewPos;
    }

    Vector3 GetTileCoord( Vector3 pos ) {
        int x = Mathf.CeilToInt(pos.x);
        int z = Mathf.CeilToInt(pos.z);

        return new Vector3(x, 0.5f, z);
    }

    // Gets pheromone value of tile
    float GetPheromone( Vector3 antPos) {
        int x = Mathf.CeilToInt(antPos.x);
        int z = Mathf.CeilToInt(antPos.z);

        return pTable[x, z];
    }
}
