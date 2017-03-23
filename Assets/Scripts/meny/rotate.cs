using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {




	void FixedUpdate () 
		{
		//Objektene som dette blir plassert på vil rotere i y aksen
		transform.Rotate (new Vector3 (0.0f, 30.0f, 0.0f) * Time.deltaTime);
		}
	}
