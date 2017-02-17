using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    [SerializeField]
        private Settings _settings;

    [SerializeField]
    private Texture2D texture;
    private GameObject tile;

    private float lat, lon;

    IEnumerator Start() {
        while (!Input.location.isEnabledByUser) {
            yield return new WaitForSeconds(1f);
        }

        Input.location.Start(10f, 5f);

        int maxWait = 20;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSeconds(1f);
            maxWait--;
        }

        if(maxWait < 1) {
            yield break;
        }

        if(Input.location.status == LocationServiceStatus.Failed) {
            yield break;
        }
        else {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
        }

        StartCoroutine(loadTiles(_settings.zoom));

        while (Input.location.isEnabledByUser) {
            yield return new WaitForSeconds(1f);
        }

        yield return StartCoroutine(Start ());
    }

    IEnumerator loadTiles(int zoom) {
        lat = Input.location.lastData.latitude;
        lon = Input.location.lastData.longitude;

        string url = string.Format("https://api.mapbox.com/v4/mapbox.{5}/{0},{1},{2}/{3}x{3}@2x.png?access_token={4}", lon, lat, zoom, _settings.size, _settings.key, _settings.style);

        WWW www = new WWW(url);
        yield return www;

        texture = www.texture;

        if(tile == null) {
            tile = GameObject.CreatePrimitive(PrimitiveType.Plane);
            tile.transform.localScale = Vector3.one * _settings.scale;
            tile.GetComponent<Renderer>().material = _settings.material;
            tile.transform.parent = transform;
        }

        tile.GetComponent<Renderer>().material.mainTexture = texture;

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(loadTiles(_settings.zoom));
    }

    // Update is called once per frame
    void Update(){
        
    }

    [System.Serializable]
    public class Settings{

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