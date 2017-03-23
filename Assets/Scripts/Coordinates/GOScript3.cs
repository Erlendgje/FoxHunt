using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOScript3 : MonoBehaviour {

    public decimal lt, ln;
    public int id;
	private float speed = 50f;
	public int score;
    public GameObject gameManager;
	private GameManager3 gmScript;
	private bool first;

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

    public void setValues(decimal lt, decimal ln, int id, int score) {

		this.id = id;

		if(lt < gmScript.northernmostPoint && lt > gmScript.southernmosttPoint && ln < gmScript.easternmostPoint && ln > gmScript.westernmostPoint) {
			this.lt = lt;
			this.ln = ln;
			this.score = score;


			if (first == true) {
				transform.position = makeVector();
				first = false;
			}
			this.GetComponent<Renderer>().enabled = true;
			transform.position = Vector3.MoveTowards(transform.position, makeVector(), speed * Time.deltaTime);
		}
		else {
			this.GetComponent<Renderer>().enabled = false;
		}
	}


	public Vector3 makeVector() {
		float z = ((float)gameManager.GetComponent<GameManager3>().easternmostPoint - (float)ln) * (gameManager.GetComponent<GameManager3>().inGameMapWidth / (float)gameManager.GetComponent<GameManager3>().coordinateMapWidth);
		float x = ((float)lt - (float)gameManager.GetComponent<GameManager3>().southernmosttPoint) * (gameManager.GetComponent<GameManager3>().inGameMapHeight / (float)gameManager.GetComponent<GameManager3>().coordinateMapHeight);
		return new Vector3(x, transform.position.y, z);
	}
}