using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackHome : MonoBehaviour {


	//når spilleren kolliderer med collidern blir han/hun sendt til neste bane, som er Olympus
	public void BackHomeButton(){
		

		Application.LoadLevel(0);

	}
}
