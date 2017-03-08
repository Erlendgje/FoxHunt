using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gps : MonoBehaviour {

    public float lat, lon;

    // Use this for initialization
    IEnumerator Start(){
        //Check if gps is on
        while (!Input.location.isEnabledByUser) {
            yield return new WaitForSeconds(1f);
        }

        Input.location.Start(10f, 5f);

        //Trying to connect to gps service
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSeconds(1f);
            maxWait--;
        }

        if (maxWait < 1) {
            yield break;
        }

        //Check if connection failed
        if (Input.location.status == LocationServiceStatus.Failed) {
            yield break;
        }
        else {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
        }

        //Check if gps is on
        while (Input.location.isEnabledByUser) {
            yield return new WaitForSeconds(1f);
        }

        //Restart function if gps is turned off
        yield return StartCoroutine(Start());
    }

    // Update is called once per frame
    void Update(){
        
    }
}