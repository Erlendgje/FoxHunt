using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOScript : MonoBehaviour {

    public double lt, ln;
    public int id;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void setValues(double lt, double ln, int id) {

        this.lt = lt;
        this.ln = ln;
        this.id = id;
    }
}