using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;


public class Hud : MonoBehaviour {

    public Text AuthorPanel;
    public Text JsonPanel;

    [System.Serializable]
    public class MyBook 
    {
        public string create_date; 
        public string id;
        public string title;
        public string genre;
        public string description;
        public string author;
        public string publisher;
        public string pages;
        public string image_url;
        public string image_buy;
    }

    public string url_server = "http://130.40.29.65:3000/api/books";

    //System.Threading.Timer _timer;

    // Use this for initialization
    void Start() {
        //int secondsInterval = 1;
        //_timer = new System.Threading.Timer(Tick, null, 0, secondsInterval * 1000);
        JsonPanel.text = "In start()";
        AuthorPanel.text = "In start()";

        StartCoroutine(GetText());

    }
    //private void Tick (object state)
    //{
    //    GetText();
    //}

    IEnumerator GetText()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url_server))
        { 
            yield return www.SendWebRequest();
 
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                JsonPanel.text = "isNetworkError";
            }
            else if (www.isHttpError)
            {
                Debug.Log(www.error);
                JsonPanel.text = "isHttpError";
            }
            else
            {
                MyBook myJsonObject = new MyBook();
                myJsonObject = JsonUtility.FromJson<MyBook>(www.downloadHandler.text);

                JsonPanel.text = www.downloadHandler.text;
                AuthorPanel.text = myJsonObject.author;

                JsonPanel.text = "In GetText() in else";

                // Show results as text
                Debug.Log(www.downloadHandler.text);
                Debug.Log(myJsonObject.create_date);
                Debug.Log(AuthorPanel.text);
            
            }
    }
}

    // Update is called once per frame
    void Update() {

        }
    } 
