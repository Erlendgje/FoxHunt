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

    public void SetValues(decimal lt, decimal ln, int id, int score) {

		this.id = id;

		if(lt < gmScript.northernmostPoint && lt > gmScript.southernmosttPoint && ln < gmScript.easternmostPoint && ln > gmScript.westernmostPoint) {
			this.lt = lt;
			this.ln = ln;
			this.score = score;


			if (first == true) {
				transform.position = gmScript.MakeVector((float)ln, (float)lt);
				first = false;
			}
			this.GetComponent<Renderer>().enabled = true;
			transform.position = Vector3.MoveTowards(transform.position, gmScript.MakeVector((float)ln, (float)lt), speed * Time.deltaTime);
		}
		else {
			this.GetComponent<Renderer>().enabled = false;
		}
	}
}