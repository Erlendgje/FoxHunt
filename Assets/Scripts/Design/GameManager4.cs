﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

public class GameManager4 : MonoBehaviour {

	//Variables used for game calculations
    public decimal[,] boundary;
    public decimal southernmosttPoint;
    public decimal northernmostPoint;
    public decimal westernmostPoint;
    public decimal easternmostPoint;
    public decimal coordinateMapHeight;
    public decimal coordinateMapWidth;
    public float inGameMapHeight;
    public float inGameMapWidth;
    public float scale;
	public int userID;

	//Game settings from server
    public double catchrange;
    public bool gps;
    public bool opponents;
    public bool points;
    public bool gameOver;

    public string date;
    public string highScore;

    public Text scoreText;

    //Objects
    public GameObject serverHandler;
    public GameObject tile;
	//public GameObject tree;
	public GameObject cam;
    public Terrain terrain;

    public Dictionary<int, GameObject> gameObjects;
    public Dictionary<int, TextMesh> scores;

    // Use this for initialization
    void Start() {
        gameObjects = new Dictionary<int, GameObject>();
        scores = new Dictionary<int, TextMesh>();
        //Getting server settings and starting constant update
        serverHandler.GetComponent<GetData4>().getConfig();
        serverHandler.GetComponent<GetData4>().startUpdate();
        //Henter dato den dagen
        date = System.DateTime.Now.Date.ToString();
    }

    // Update is called once per frame
    void Update() {
		//moving camera after player
		cam.transform.position = gameObjects[userID].transform.position + new Vector3(-2,15,0);
    }

    public void UpdateScore(int id, int score, string name)
    {
        TextMesh temp;
        if (id == userID)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            if (!scores.TryGetValue(id, out temp))
            {
				scores.Add(id, gameObjects[id].GetComponentInChildren<TextMesh>());
			}
			scores[id].text = name + "\r\n" + "Score: " + score;
		}
    }

    public void SetSettings(decimal[,] boundary, double catchrange, bool gps, bool opponents, bool points) {
        this.boundary = boundary;
        this.catchrange = catchrange;
        this.gps = gps;
        this.opponents = opponents;
        this.points = points;

		//Getting highest and lowest value from the maps coordinates
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

		//getting height of map in coordinates
        coordinateMapHeight = northernmostPoint - southernmosttPoint;
        coordinateMapWidth = easternmostPoint - westernmostPoint;

        //Creating map/tile
        tile = GameObject.CreatePrimitive(PrimitiveType.Plane);
        tile.transform.localScale = new Vector3(Vector3.one.x + scale * (float)coordinateMapWidth, Vector3.one.y, Vector3.one.z + scale * (float)coordinateMapHeight);
        inGameMapHeight = tile.GetComponent<Renderer>().bounds.size.x;
        inGameMapWidth = tile.GetComponent<Renderer>().bounds.size.z;
        tile.transform.position = new Vector3(inGameMapHeight/2, 0, inGameMapWidth/2);
		//Increasing tile size so camera cant see outside the map
		tile.transform.localScale = new Vector3(tile.transform.localScale.x + 4, Vector3.one.y, tile.transform.localScale.z + 4);

        //Creating a terrain with grass (Prefab)
        Vector3 terrainPosition = new Vector3(-20, 0, -20);
        terrain = Instantiate(terrain, terrainPosition, tile.transform.rotation);
        terrain.GetComponent<Renderer>().material.color = Color.green;


        //Making a fence around the map
        for (int i = 0; i < boundary.Length/2; i++) {

			Vector3 corner1;
			Vector3 corner2;
			float difference;
			float differenceX;
			float differenceY;


			//Getting 2 points that decide where the fence will be built
			corner1 = MakeVector((float)boundary[1, i], (float)boundary[0, i]);

			if (i < boundary.Length/2 - 1) {
				corner2 = MakeVector((float)boundary[1, i + 1], (float)boundary[0, i + 1]);
			}
			else {
				corner2 = MakeVector((float)boundary[1, 0], (float)boundary[0, 0]);
			}

			//How long the fence should be and how far it should be between each object.
			difference = Vector3.Distance(corner1, corner2);
			differenceX = Math.Abs(corner1.x - corner2.x) / (difference / 2);
			differenceY = Math.Abs(corner1.z - corner2.z) / (difference / 2);

			//Making the fence
			for (int k = 0; k < difference/2; k++) {
                //Fence positions
				float x = 0;
				float y = 0;
                


                if (corner1.x > corner2.x) {
					x = corner1.x - differenceX * k + (UnityEngine.Random.Range(-0.2f, 0.2f));
				}else {
					x = corner1.x + differenceX * k + (UnityEngine.Random.Range(-0.2f, 0.2f));
				}

				if(corner1.z > corner2.z) {
					y = corner1.z - differenceY * k + (UnityEngine.Random.Range(-0.5f, 0.5f));
				}else {
					y = corner1.z + differenceY * k + (UnityEngine.Random.Range(-0.5f, 0.5f));
				}
                //Spawner forskjellige trær som gjerde, ved bruk av RNG
                int number = UnityEngine.Random.Range(1,3);
				GameObject tree = Instantiate(Resources.Load("Tree" + number), new Vector3(x, 0.1f, y), transform.rotation) as GameObject;

               
			}
		}
        //World bush positions
        for (int j = 0; j < 100; j++)
        {
            float a = UnityEngine.Random.Range(-20, 40);
            float b = UnityEngine.Random.Range(-20, 40);

            //Try not to be inside the map
            if (0 < a && 25 > a)
            {
                a = UnityEngine.Random.Range(-20, 40);
            }

            if (0 < b && 25 > b)
            {
                b = UnityEngine.Random.Range(-20, 40);
            }

            //Instantiate bush
            GameObject bush = Instantiate(Resources.Load("Busk"), new Vector3(a, 0.1f, b), transform.rotation) as GameObject;
            bush.GetComponent<Renderer>().material.color = Color.green;
        }

    }

    public void SetGameOver(bool gameOver) {

        this.gameOver = gameOver;
        saveHighScore();

    }

    public void AddGameObject() {

    }
    //Saving high score
    public void saveHighScore()
    {
        //If high score file don't exist, make one
        if (!File.Exists(Application.persistentDataPath + "/playerHighScore.dat")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerHighScore.dat");
            highScore = scores.ToString() + date  + "\n";

            bf.Serialize(file, highScore);
            file.Close();
        }
        else
        {
            //If it exists, update the file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerHighScore.dat", FileMode.Open);
            
            highScore = scores.ToString() + date + "\n";
            bf.Serialize(file, highScore);
            file.Close();
        }

        

    }
    //Loading high score
    //Dette må vel flyttes til en annen script, der den loader high scoren
    public void loadHighscore()
    {
        if (File.Exists(Application.persistentDataPath + "/playerHighScore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerHighScore.dat", FileMode.Open);
 
            highScore = bf.Deserialize(file).ToString();
            file.Close();

        }

    }

	//Konverting irl coordinates to Vector3
	public Vector3 MakeVector(float ln, float lt) {
		float z = ((float)easternmostPoint - ln) * (inGameMapWidth / (float)coordinateMapWidth);
		float x = (lt - (float)southernmosttPoint) * (inGameMapHeight / (float)coordinateMapHeight);
		return new Vector3(x, 0, z);
	}

}