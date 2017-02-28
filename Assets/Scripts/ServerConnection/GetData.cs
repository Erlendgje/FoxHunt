using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class GetData : MonoBehaviour {

    public GameObject spawn, fox, hunter, obstacle, gameManager;

    // Use this for initialization
    void Start(){
        getConfig();
    }

    // Update is called once per frame
    void Update(){

    }

    public void getConfig() {

        string url = "http://asia.hiof.no/foxhunt-servlet/getConfig";

        XmlDocument xmlData = new XmlDocument();
        xmlData.Load(url);

        XmlNodeList pointList = xmlData.GetElementsByTagName("point");


        double[,] boundary = new double[2,4];
        int i = 0;
        foreach(XmlNode point in pointList) {
            boundary[0,i] = double.Parse(point.Attributes["lat"].Value);
            boundary[1,i] = double.Parse(point.Attributes["lon"].Value);
            i++;
        }

        XmlNodeList settings = xmlData.GetElementsByTagName("display");

        foreach (XmlNode setting in settings) {

            double catchrange = double.Parse(setting.Attributes["catchrange"].Value);
            bool gps = bool.Parse(setting.Attributes["gps"].Value);
            bool opponents = bool.Parse(setting.Attributes["opponents"].Value);
            bool points = bool.Parse(setting.Attributes["points"].Value);

            gameManager.GetComponent<GameManager>().setSettings(boundary, catchrange, gps, opponents, points);
        }

        getGameObjects(xmlData, true);
    }

    public void getState() {

        string url = "http://asia.hiof.no/foxhunt-servlet/getState";

        XmlDocument xmlData = new XmlDocument();
        xmlData.Load(url);

        XmlNodeList messages = xmlData.GetElementsByTagName("msg");

        foreach (XmlNode msg in messages) {
           // GameSettings.gameOver = bool.Parse(msg.Attributes["gameOver"].Value);
        }

        getGameObjects(xmlData, false);
    }

    public void getGameObjects(XmlDocument xmlData, bool startGame) {

        XmlNodeList gameObjects = xmlData.GetElementsByTagName("gameObject");

        foreach (XmlNode gameObject in gameObjects) {
            if (startGame) {
                switch (gameObject.Attributes["class"].Value) {
                    case "Fox":
                        Instantiate(fox, spawn.transform.position, spawn.transform.rotation);
                        break;
                    case "Hunter":
                        Instantiate(hunter, spawn.transform.position, spawn.transform.rotation);
                        break;
                    case "Obstacle":
                        Instantiate(obstacle, spawn.transform.position, spawn.transform.rotation);
                        break;
                }
            }


        }
    }
}