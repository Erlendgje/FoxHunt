using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private double lt, ln;
    private int id, caught;

    // Use this for initialization
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void setValues(double lt, double ln, int id, int caught) {

        this.lt = lt;
        this.ln = ln;
        this.id = id;
        this.caught = caught;

    }
}