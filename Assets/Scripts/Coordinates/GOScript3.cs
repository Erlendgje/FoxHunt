using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GOScript3 : MonoBehaviour {

	//Settings for the gameobject
    public decimal lt, ln;
    public int id;
	public string name;
    private float speed = 50f;
    public int score;
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

    }

    // Update is called once per frame
    void Update() {
        
    }


	//Set values and moving the gameobject
    public void SetValues(decimal lt, decimal ln, int id, int score, string name) {


		//bug.Log(lt + ", " + ln + ", " + this.name);

        this.id = id;
        this.score = score;

		if(this.name == "") {
			this.name = name;
		}

		if (this.CompareTag("Hunter") && first == false)
        {
			gmScript.UpdateScore(id, score, this.name);
        }

        //Checking if object is located on the field irl
        if (lt < gmScript.northernmostPoint && lt > gmScript.southernmosttPoint && ln < gmScript.easternmostPoint && ln > gmScript.westernmostPoint) {
            this.lt = lt;
            this.ln = ln;

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