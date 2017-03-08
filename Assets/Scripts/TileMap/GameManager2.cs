using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour {

    public decimal[,] boundary;
    public double catchrange;
    public bool gps;
    public bool opponents;
    public bool points;
    public bool gameOver;
    public GameObject serverHandler;
    public GameObject tileManager;

    public Dictionary<int, GameObject> gameObjects;

    // Use this for initialization
    void Start() {
        gameObjects = new Dictionary<int, GameObject>();
        serverHandler.GetComponent<GetData2>().getConfig();
        tileManager.GetComponent<TileManager2>().StartCoroutine(tileManager.GetComponent<TileManager2>().loadTiles());
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
    }

    public void setGameOver(bool gameOver) {
        this.gameOver = gameOver;
    }

    public void addGameObject() {

    }
}