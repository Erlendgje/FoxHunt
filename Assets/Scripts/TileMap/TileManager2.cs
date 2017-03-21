using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager2 : MonoBehaviour {

    [SerializeField]
    private Settings _settings;

    [SerializeField]
    private Texture2D texture;
    private List<GameObject> tiles;
    private decimal tileLength = 0.0008823m;
    private decimal tileWidth = 0.0017646m;

    public GameObject gameManager;

    private GameManager2 gameManagerScript;
    


    void Start() {
        gameManagerScript = gameManager.GetComponent<GameManager2>();
        tiles = new List<GameObject>();
    }

    public IEnumerator loadTiles() {


        decimal southernmosttPoint = new decimal();
        decimal northernmostPoint = new decimal();
        decimal westernmostPoint = new decimal();
        decimal easternmostPoint = new decimal();

        int numberOfTilesHeight = 4;
        int numberOfTilesWidth = 2;

        for(int i = 0; i < 4; i++) {

            if(i == 0) {
                southernmosttPoint = gameManagerScript.boundary[0, i];
                northernmostPoint = gameManagerScript.boundary[0, i];
                westernmostPoint = gameManagerScript.boundary[1, i];
                easternmostPoint = gameManagerScript.boundary[1, i];
            }

            if(southernmosttPoint > gameManagerScript.boundary[0, i]) {
                southernmosttPoint = gameManagerScript.boundary[0, i];
            }

            if(northernmostPoint < gameManagerScript.boundary[0, i]) {
                northernmostPoint = gameManagerScript.boundary[0, i];
            }

            if(westernmostPoint > gameManagerScript.boundary[1, i]) {
                westernmostPoint = gameManagerScript.boundary[1, i];
            }

            if(easternmostPoint < gameManagerScript.boundary[1, i]) {
                easternmostPoint = gameManagerScript.boundary[1, i];
            }
        }

        numberOfTilesHeight += (int) decimal.Round((northernmostPoint - southernmosttPoint) / tileLength, 0);

        numberOfTilesWidth += (int)decimal.Round((easternmostPoint - westernmostPoint) / tileLength, 0);


        if (numberOfTilesHeight < 5) {
            numberOfTilesHeight = 5;
        }
        if(numberOfTilesWidth < 3) {
            numberOfTilesWidth = 3;
        }

        int counter = 0;

        Vector3 originalPostion = this.transform.position;

        for(int k = 0; k < numberOfTilesHeight; k++) {
            for(int l = 0; l < numberOfTilesWidth; l++) {
                decimal lon = easternmostPoint + tileWidth - tileWidth * l;
                decimal lat = southernmosttPoint - tileLength + tileLength * k;

                //URL to map
                string url = string.Format("https://api.mapbox.com/v4/mapbox.{5}/{0},{1},{2}/{3}x{3}@2x.png?access_token={4}", lon, lat, _settings.zoom, _settings.size, _settings.key, _settings.style);

                WWW www = new WWW(url);
                //Loading url
                yield return www;

                //Getting image
                texture = www.texture;

                //Creating plane and applying map texture
                tiles.Add(GameObject.CreatePrimitive(PrimitiveType.Plane));
                tiles[counter].transform.localScale = Vector3.one * _settings.scale;
                tiles[counter].GetComponent<Renderer>().material = _settings.material;

                this.transform.position = new Vector3(originalPostion.x - (l * 10), originalPostion.y, originalPostion.z + (k * 10));

                tiles[counter].transform.position = this.transform.position;
                tiles[counter].transform.rotation = this.transform.rotation;
                tiles[counter].GetComponent<Renderer>().material.mainTexture = texture;


                counter++;
            }
        }

        yield return new WaitForSeconds(1f);
    }

    // Update is called once per frame
    void Update() {

    }


    //Map settings
    [System.Serializable]
    public class Settings {

        [SerializeField]
        public Material material;
        [SerializeField]
        public int zoom = 18;
        [SerializeField]
        public int size = 640;
        [SerializeField]
        public float scale = 1f;
        [SerializeField]
        public string key;
        [SerializeField]
        public string style = "emerald";
    }
}