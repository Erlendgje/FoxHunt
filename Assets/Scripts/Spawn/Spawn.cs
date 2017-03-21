using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public Transform SpawnTest;
    public bool Spawned = false;
    public float latitude;
    public float longitude;
   
    // Use this for initialization
    void Start () {

  
        if (Spawned == false)
        {
            Spawned = true;
            Instantiate(SpawnTest, new Vector3(-2, 100, -2), Quaternion.identity);
                
        }
		
	}


    // Update is called once per frame
    void Update () {



        
	}
}
