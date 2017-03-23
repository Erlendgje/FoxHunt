using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merknapp : MonoBehaviour {

	public GameObject MerKnapp;
	public void Openmeny () {

		MerKnapp.SetActive (!MerKnapp.activeSelf);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
