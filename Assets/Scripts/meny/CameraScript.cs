using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraScript : MonoBehaviour {

	public Material mt;

	//private RawImage image;
	//private WebCamTexture cam;
	//private AspectRatioFitter ass;


	// Use this for initialization
	void Start () {


		WebCamDevice[] device = WebCamTexture.devices;
		WebCamTexture wct = new WebCamTexture ();
		if (device.Length > 0) {
			wct.deviceName = device [0].name;
			wct.Play ();
		
		}

		mt.mainTexture = wct;
		//for( int i = 0 ; i < devices.Length ; i++ )
			//Debug.Log(devices[i].name);        
	}
}
