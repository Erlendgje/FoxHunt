using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour {

    public GameObject test;
    public float latitude;
    public float longitude;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        gameObject.GetComponent<Renderer>().material.color = Color.blue;

        transform.position += Vector3.forward * Time.deltaTime;
        transform.position += Vector3.right * Time.deltaTime;


    }

}
