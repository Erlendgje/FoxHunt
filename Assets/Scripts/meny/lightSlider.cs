using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightSlider : MonoBehaviour {

	public Slider mainSlider;
	float brightNess = 0.5f;

	//Invoked when a submit button is clicked.
	 public void Lightsetting()
	{
		brightNess = mainSlider.value;
		RenderSettings.ambientLight = new Color (brightNess, brightNess, brightNess, 1);

		Debug.Log (mainSlider.value);
	}

}
