using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public Transform SpawnTest;
    public bool Spawned = false;
    public int i;
   
    // Use this for initialization
    void Start () {

  
        if (Spawned == false)
        {
            Spawned = true;
            Instantiate(SpawnTest, new Vector3(0, 20, i), Quaternion.identity);
                
        }
		
	}


    // Update is called once per frame
    void Update () {



        
	}
}
