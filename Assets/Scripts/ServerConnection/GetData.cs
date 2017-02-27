using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class GetData : MonoBehaviour {

    string url = "http://asia.hiof.no/foxhunt-servlet/getConfig";

    // Use this for initialization
    void Start(){
        getData();
    }

    // Update is called once per frame
    void Update(){

    }

    void getData() {

        XmlDocument xmlData = new XmlDocument();
        xmlData.Load(url);

        XmlNodeList objectList = xmlData.GetElementsByTagName("gameObject");
        
        
        foreach(XmlNode gameObject in objectList) {
            Debug.Log(gameObject.Attributes["class"].Value);
        }
    }
}