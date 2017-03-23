using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayGame : MonoBehaviour {
	
	

	void Awake ()
	{
		//Henter inn hvem karakter man valgte
		GetComponent<PickPlayerScript>();
		DontDestroyOnLoad (this);
	}
	
	//Starter spillet med karakteren man valgte
	public static void LoadLevel(GameObject player, int level)
	{
		
		Application.LoadLevel (1);


			
	}
	
}
