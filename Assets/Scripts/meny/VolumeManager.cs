using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {

	public Slider Volume;
	public AudioSource music;
	
	// Update is called once per frame
	 void Update () {

		music.volume = Volume.value;
	}
}
