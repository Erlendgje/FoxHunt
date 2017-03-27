using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager3 : MonoBehaviour {

    public decimal[,] boundary;
    public decimal southernmosttPoint;
    public decimal northernmostPoint;
    public decimal westernmostPoint;
    public decimal easternmostPoint;
    public decimal coordinateMapHeight;
    public decimal coordinateMapWidth;
    public float inGameMapHeight;
    public float inGameMapWidth;
    public float scale = 1000;
	public int userID = 101;


    public double catchrange;
    public bool gps;
    public bool opponents;
    public bool points;
    public bool gameOver;

    public GameObject serverHandler;
    public GameObject tile;
	public GameObject tree;
	public GameObject cam;

    public Dictionary<int, GameObject> gameObjects;

    // Use this for initialization
    void Start() {
        gameObjects = new Dictionary<int, GameObject>();
        serverHandler.GetComponent<GetData3>().getConfig();
		serverHandler.GetComponent<GetData3>().startUpdate();
	}

    // Update is called once per frame
    void Update() {
		cam.transform.position = gameObjects[userID].transform.position + new Vector3(-2,15,0);
    }

    public void SetSettings(decimal[,] boundary, double catchrange, bool gps, bool opponents, bool points) {
        this.boundary = boundary;
        this.catchrange = catchrange;
        this.gps = gps;
        this.opponents = opponents;
        this.points = points;


        for (int i = 0; i < boundary.Length/2; i++) {

            if (i == 0) {
                southernmosttPoint = boundary[0, i];
                northernmostPoint = boundary[0, i];
                westernmostPoint = boundary[1, i];
                easternmostPoint = boundary[1, i];
            }

            if (southernmosttPoint > boundary[0, i]) {
                southernmosttPoint = boundary[0, i];
            }

            if (northernmostPoint < boundary[0, i]) {
                northernmostPoint = boundary[0, i];
            }

            if (westernmostPoint > boundary[1, i]) {
                westernmostPoint = boundary[1, i];
            }

            if (easternmostPoint < boundary[1, i]) {
                easternmostPoint = boundary[1, i];
            }
        }

        coordinateMapHeight = northernmostPoint - southernmosttPoint;
        coordinateMapWidth = easternmostPoint - westernmostPoint;

        tile = GameObject.CreatePrimitive(PrimitiveType.Plane);
        tile.transform.localScale = new Vector3(Vector3.one.x + scale * (float)coordinateMapWidth, Vector3.one.y, Vector3.one.z + scale * (float)coordinateMapHeight);
        inGameMapHeight = tile.GetComponent<Renderer>().bounds.size.x;
        inGameMapWidth = tile.GetComponent<Renderer>().bounds.size.z;
        tile.transform.position = new Vector3(inGameMapHeight/2, 0, inGameMapWidth/2);
		tile.transform.localScale = new Vector3(tile.transform.localScale.x + 4, Vector3.one.y, tile.transform.localScale.z + 4);


		for(int i = 0; i < boundary.Length/2; i++) {

			Vector3 corner1;
			Vector3 corner2;
			float difference;
			float differenceX;
			float differenceY;

			corner1 = MakeVector((float)boundary[1, i], (float)boundary[0, i]);

			if (i < boundary.Length/2 - 1) {
				corner2 = MakeVector((float)boundary[1, i + 1], (float)boundary[0, i + 1]);
			}
			else {
				corner2 = MakeVector((float)boundary[1, 0], (float)boundary[0, 0]);
			}

			difference = Vector3.Distance(corner1, corner2);

			differenceX = Math.Abs(corner1.x - corner2.x) / (difference / 2);
			differenceY = Math.Abs(corner1.z - corner2.z) / (difference / 2);


			for (int k = 0; k < difference/2; k++) {

				float x = 0;
				float y = 0;

				if (corner1.x > corner2.x) {
					x = corner1.x - differenceX * k + (UnityEngine.Random.Range(-0.5f, 0.5f));
				}else {
					x = corner1.x + differenceX * k + (UnityEngine.Random.Range(-0.5f, 0.5f));
				}

				if(corner1.z > corner2.z) {
					y = corner1.z - differenceY * k + (UnityEngine.Random.Range(-0.5f, 0.5f));
				}else {
					y = corner1.z + differenceY * k + (UnityEngine.Random.Range(-0.5f, 0.5f));
				}
				Instantiate(tree, new Vector3(x, 0.5f, y), transform.rotation);
			}
		}
	}

    public void SetGameOver(bool gameOver) {
        this.gameOver = gameOver;
    }

    public void AddGameObject() {

    }

	public Vector3 MakeVector(float ln, float lt) {
		float z = ((float)easternmostPoint - ln) * (inGameMapWidth / (float)coordinateMapWidth);
		float x = (lt - (float)southernmosttPoint) * (inGameMapHeight / (float)coordinateMapHeight);
		return new Vector3(x, 0, z);
	}

}