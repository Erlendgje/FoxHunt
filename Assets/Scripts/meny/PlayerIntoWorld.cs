using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntoWorld : MonoBehaviour {




	//Fikk hjelp av Jonas med Resources funksjonen
	void Awake ()
	{	//Henter inn hvem karakter som ble valgt fra PickPlayerScriptet
		
		GetComponent<PickPlayerScript>();
		DontDestroyOnLoad (this);
		//Klonen blir generert på satt posisjon
		Instantiate(Resources.Load(PickPlayerScript.cName), new Vector3(3f, 23f, 86f), Quaternion.identity);


	}



	

}
