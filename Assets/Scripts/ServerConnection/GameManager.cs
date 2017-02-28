using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public  double[,] boundary;
    public  double catchrange;
    public  bool gps;
    public  bool opponents;
    public  bool points;
    public  bool gameOver;

    public Dictionary<int, GameObject> gameObjects;

    // Use this for initialization
    void Start(){
        gameObjects = new Dictionary<int, GameObject>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void setSettings(double[,] boundary, double catchrange, bool gps, bool opponents, bool points) {
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