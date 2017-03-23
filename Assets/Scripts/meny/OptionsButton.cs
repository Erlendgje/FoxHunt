using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour {

	//når spilleren kolliderer med collidern blir han/hun sendt til neste bane, som er Olympus
	public void Options(){


		Application.LoadLevel(1);

	}
}
