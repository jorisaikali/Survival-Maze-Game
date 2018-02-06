using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShift : MonoBehaviour {

    private GameObject[] wallList;
    private int NumOfWalls;
    private int WallsToShift;

	// Use this for initialization
	void Start () {
        wallList = GameObject.FindGameObjectsWithTag("Walls");
        NumOfWalls = wallList.Length;
        WallsToShift = 0;
    }


    private void ShiftWalls() {
        if (NumOfWalls % 2 == 0)
        {
            WallsToShift = Random.Range(0, NumOfWalls / 6);
        } else
        {
            WallsToShift = Random.Range(0, (NumOfWalls - 1) / 6);
        }

        for (int i = 0; i <= WallsToShift; i++) {
            GameObject x = wallList[Random.Range(0, NumOfWalls)];
            wallList[Random.Range(0, NumOfWalls)].transform.position = new Vector3(x.transform.position.x, x.transform.position.y + 10, x.transform.position.z);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
