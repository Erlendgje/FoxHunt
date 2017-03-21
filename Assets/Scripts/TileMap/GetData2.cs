using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class GetData2 : MonoBehaviour {

    public GameObject spawn, fox, hunter, obstacle, gameManager;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void getConfig() {

        string url = "http://asia.hiof.no/foxhunt-servlet/getConfig";

        XmlDocument xmlData = new XmlDocument();
        xmlData.Load(url);



        //Henter ut map ariale
        XmlNodeList pointList = xmlData.GetElementsByTagName("point");
        decimal[,] boundary = new decimal[2, 4];
        int i = 0;
        foreach (XmlNode point in pointList) {
            boundary[0, i] = decimal.Parse(point.Attributes["lat"].Value);
            boundary[1, i] = decimal.Parse(point.Attributes["lon"].Value);
            i++;
        }


        //Henter ut settings fra xmlfila
        XmlNodeList settings = xmlData.GetElementsByTagName("display");

        foreach (XmlNode setting in settings) {

            double catchrange = double.Parse(setting.Attributes["catchrange"].Value);
            bool gps = bool.Parse(setting.Attributes["gps"].Value);
            bool opponents = bool.Parse(setting.Attributes["opponents"].Value);
            bool points = bool.Parse(setting.Attributes["points"].Value);

            gameManager.GetComponent<GameManager2>().setSettings(boundary, catchrange, gps, opponents, points);
        }


        //Lager spillobjekter i scenen
        XmlNodeList gameObjects = xmlData.GetElementsByTagName("gameObject");

        GameObject go = gameManager;

        foreach (XmlNode gameObject in gameObjects) {


            switch (gameObject.Attributes["class"].Value) {
                case "Fox":
                    go = Instantiate(fox, spawn.transform.position, spawn.transform.rotation);
                    break;
                case "Hunter":
                    go = Instantiate(hunter, spawn.transform.position, spawn.transform.rotation);
                    break;
                case "Obstacle":
                    go = Instantiate(obstacle, spawn.transform.position, spawn.transform.rotation);
                    break;
            }

            int id = int.Parse(gameObject.Attributes["id"].Value);
            gameManager.GetComponent<GameManager2>().gameObjects.Add(id, go);
        }

        getGameObjects(gameObjects);
    }




    public void getState() {

        string url = "http://asia.hiof.no/foxhunt-servlet/getState";

        XmlDocument xmlData = new XmlDocument();
        xmlData.Load(url);


        //Sjekker om spillet er ferdig
        XmlNodeList messages = xmlData.GetElementsByTagName("msg");

        foreach (XmlNode msg in messages) {
            bool gameOver = bool.Parse(msg.Attributes["gameOver"].Value);
            gameManager.GetComponent<GameManager>().setGameOver(gameOver);
        }

        XmlNodeList gameObjects = xmlData.GetElementsByTagName("gameObject");

        getGameObjects(gameObjects);


    }

    public void getGameObjects(XmlNodeList gameObjects) {


        //Setter verdiene til spillobjektene
        foreach (XmlNode gameObject in gameObjects) {

            decimal ln = decimal.Parse(gameObject.Attributes["ln"].Value);
            decimal lt = decimal.Parse(gameObject.Attributes["lt"].Value);
            int id = int.Parse(gameObject.Attributes["id"].Value);
            GameObject tempGO;

            gameManager.GetComponent<GameManager2>().gameObjects.TryGetValue(id, out tempGO);

            tempGO.GetComponent<GOScript2>().setValues(ln, lt, id);
        }
    }
}