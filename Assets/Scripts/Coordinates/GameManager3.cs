using System.Collections;
using System.Collections.Generic;
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


    public double catchrange;
    public bool gps;
    public bool opponents;
    public bool points;
    public bool gameOver;

    public GameObject serverHandler;
    public GameObject tile;

    public Dictionary<int, GameObject> gameObjects;

    // Use this for initialization
    void Start() {
        gameObjects = new Dictionary<int, GameObject>();
        serverHandler.GetComponent<GetData3>().getConfig();
		serverHandler.GetComponent<GetData3>().startUpdate();
	}

    // Update is called once per frame
    void Update() {

    }

    public void setSettings(decimal[,] boundary, double catchrange, bool gps, bool opponents, bool points) {
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

    }

    public void setGameOver(bool gameOver) {
        this.gameOver = gameOver;
    }

    public void addGameObject() {

    }

}