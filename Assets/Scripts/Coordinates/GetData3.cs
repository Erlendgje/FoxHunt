using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class GetData3 : MonoBehaviour {

    public GameObject spawn, fox, hunter, obstacle, gameManager;
	public float lt, ln;

	// Use this for initialization
	IEnumerator Start() {

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
			lt = Input.location.lastData.latitude;
			ln = Input.location.lastData.longitude;
		}

		//Check if gps is on
		while (Input.location.isEnabledByUser) {
			yield return new WaitForSeconds(1f);
		}

		//Restart function if gps is turned off
		yield return StartCoroutine(Start());
	}

	public void getConfig() {

        string url = "http://asia.hiof.no/foxhunt-servlet/getConfig";

        XmlDocument xmlData = new XmlDocument();
        xmlData.Load(url);



        //Getting map area
        XmlNodeList pointList = xmlData.GetElementsByTagName("point");
        decimal[,] boundary = new decimal[2, 4];
        int i = 0;
        foreach (XmlNode point in pointList) {
            boundary[0, i] = decimal.Parse(point.Attributes["lat"].Value);
            boundary[1, i] = decimal.Parse(point.Attributes["lon"].Value);
            i++;
        }


        //Getting game settings
        XmlNodeList settings = xmlData.GetElementsByTagName("display");

        foreach (XmlNode setting in settings) {

            double catchrange = double.Parse(setting.Attributes["catchrange"].Value);
            bool gps = bool.Parse(setting.Attributes["gps"].Value);
            bool opponents = bool.Parse(setting.Attributes["opponents"].Value);
            bool points = bool.Parse(setting.Attributes["points"].Value);

            gameManager.GetComponent<GameManager3>().SetSettings(boundary, catchrange, gps, opponents, points);
        }


        //Creating gameobjects
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
            gameManager.GetComponent<GameManager3>().gameObjects.Add(id, go);
        }

        getGameObjects(gameObjects);
    }

	//Starting update to/from server each 0.2 sec
	public void startUpdate() {
		InvokeRepeating("getState", 0f, 0.2f);
	}



	//Getting info from server about the games flow.
    public void getState() {

		lt = Input.location.lastData.latitude;
		ln = Input.location.lastData.longitude;
		string url = "http://asia.hiof.no/foxhunt-servlet/getState?userid=" + gameManager.GetComponent<GameManager3>().userID + "&lat=" + lt + "&lon=" + ln;

        XmlDocument xmlData = new XmlDocument();
        xmlData.Load(url);


        //Checks if the game is done
        XmlNodeList messages = xmlData.GetElementsByTagName("msg");

        foreach (XmlNode msg in messages) {
            bool gameOver = bool.Parse(msg.Attributes["gameOver"].Value);
            gameManager.GetComponent<GameManager3>().SetGameOver(gameOver);
        }

        XmlNodeList gameObjects = xmlData.GetElementsByTagName("gameObject");

		//Updateing each gameobject
        getGameObjects(gameObjects);
    }



	//Updating gameobjects
    public void getGameObjects(XmlNodeList gameObjects) {


        //Getting value from XMLNode and set the value in GOScript
        foreach (XmlNode gameObject in gameObjects) {

            decimal ln = decimal.Parse(gameObject.Attributes["ln"].Value);
            decimal lt = decimal.Parse(gameObject.Attributes["lt"].Value);
            int id = int.Parse(gameObject.Attributes["id"].Value);
			string name = "";
			int score = 0;
			try {
				score = int.Parse(gameObject.Attributes["caught"].Value);
			}
			catch {

			}

			try {
				name = gameObject.Attributes["name"].Value;
			}
			catch {

			}
			
            GameObject tempGO;

            gameManager.GetComponent<GameManager3>().gameObjects.TryGetValue(id, out tempGO);

            tempGO.GetComponent<GOScript3>().SetValues(lt, ln, id, score, name);
        }
    }
}