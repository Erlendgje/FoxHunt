using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOScript3 : MonoBehaviour {

	//Settings for the gameobject
    public decimal lt, ln;
    public int id;
    private float speed = 50f;
    public int score;
    public int highScore;
    public GameObject gameManager;
    private GameManager3 gmScript;
    private bool first;

	//Finding GameManager
    private void Awake() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gmScript = gameManager.GetComponent<GameManager3>();
        first = true;
    }

    // Use this for initialization
    void Start() {
        //Henter gamle high score, foreløpig 0 fra start
        highScore = PlayerPrefs.GetInt("highscore", highScore);
    }

    // Update is called once per frame
    void Update() {
        //Oppdaterer high scoren
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
        }
    }


	//Set values and moving the gameobject
    public void SetValues(decimal lt, decimal ln, int id, int score) {

        this.id = id;

		//Checking if object is located on the field irl
        if (lt < gmScript.northernmostPoint && lt > gmScript.southernmosttPoint && ln < gmScript.easternmostPoint && ln > gmScript.westernmostPoint) {
            this.lt = lt;
            this.ln = ln;
            this.score = score;

			//If its first time to enter field, teleport object to position
            if (first == true) {
                transform.position = gmScript.MakeVector((float)ln, (float)lt);
                first = false;
            }
            this.GetComponent<Renderer>().enabled = true;
            transform.position = Vector3.MoveTowards(transform.position, gmScript.MakeVector((float)ln, (float)lt), speed * Time.deltaTime);
        }
        else {
            this.GetComponent<Renderer>().enabled = false;
			first = true;
        }
    }

    
}