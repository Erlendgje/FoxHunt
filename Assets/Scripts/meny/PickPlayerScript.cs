using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PickPlayerScript : MonoBehaviour {

	private List<GameObject> Karakterer;

	private int Velger = 0;

	public static string cName;

	void Awake()
	{
		PlayerPrefs.SetInt ("newGame", 1);
	}

	//Det blir laget en liste med 3 objekter man kan velge mellom 
	//når man trykker på knappen med en av de tre int så blir spilleren i den rekkefølgen satt til aktiv,
	//mens de andre blir satt til deaktive.

	private void Start()
	{
		Karakterer = new List<GameObject> ();
		foreach (Transform t in transform) 
		{
			Karakterer.Add(t.gameObject);
			t.gameObject.SetActive(false);

		}

		Karakterer [Velger].SetActive (true);
	}

	public void Velg(int valgt)
	{
		if (valgt == Velger)
			return;

		if(valgt < 0 || valgt >= Karakterer.Count)
			return;
		Karakterer [Velger].SetActive (false);
		Velger = valgt;
		Karakterer [Velger].SetActive (true); 
	}

	//Fikk hjelp av Jonas
	//Gjør at karakteren blir "lagret" og det blir kloner ut av den i neste bane (Det er objekter i hver bane som henter inn hvem som ble valgt)
	public void SpillAv()
	{
			cName = Karakterer [Velger].name;
			PlayGame.LoadLevel (Karakterer [Velger], 1);
			
	}




}



// Kommet fram til dette ved hjelp fra tutorial "https://www.youtube.com/watch?v=T-AbCUuLViA"

