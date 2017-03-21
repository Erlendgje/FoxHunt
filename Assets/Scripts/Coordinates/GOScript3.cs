using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOScript3 : MonoBehaviour {

    public decimal lt, ln;
    public int id;
	public float speed = 5f;
    public GameObject gameManager;
	private bool first;

	private void Awake() {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		first = true;
	}

	// Use this for initialization
	void Start() {
        
        
    }

    // Update is called once per frame
    void Update() {
    }

    public void setValues(decimal lt, decimal ln, int id) {

        this.lt = lt;
        this.ln = ln;
        this.id = id;

		if(first == true) {
			transform.position = makeVector();
			first = false;
		}

		transform.position = Vector3.MoveTowards(transform.position, makeVector(), speed * Time.deltaTime);
	}


	public Vector3 makeVector() {
		float z = ((float)gameManager.GetComponent<GameManager3>().easternmostPoint - (float)ln) * (gameManager.GetComponent<GameManager3>().inGameMapWidth / (float)gameManager.GetComponent<GameManager3>().coordinateMapWidth);
		float x = ((float)lt - (float)gameManager.GetComponent<GameManager3>().southernmosttPoint) * (gameManager.GetComponent<GameManager3>().inGameMapHeight / (float)gameManager.GetComponent<GameManager3>().coordinateMapHeight);
		return new Vector3(x, transform.position.y, z);
	}
}