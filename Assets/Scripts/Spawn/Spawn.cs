using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public Transform SpawnTest;
    public bool Spawned = false;
   // public int i;
   //For fremtidig bruk
   
    // Use this for initialization
    void Start () {

  
        if (Spawned == false)
        {
            Spawned = true;
            Instantiate(SpawnTest, new Vector3(0, 100, 0), Quaternion.identity);
                
        }
		
	}


    // Update is called once per frame
    void Update () {



        
	}
}
