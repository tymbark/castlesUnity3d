using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class NetworkController : MonoBehaviour {

    private GameController GameController;

    void Start() {
        GameController = GetComponent<GameController>();

        //StartCoroutine(GetGameState());
        //print("StartCoroutine before");
        //StartCoroutine(PostGameState());
        //print("StartCoroutine after");
    }

    IEnumerator GetGameState() {
        string getCountriesUrl = "https://cn0izbewf2.execute-api.eu-west-2.amazonaws.com/alpha2/games/1";
        UnityWebRequest request = UnityWebRequest.Get(getCountriesUrl);
        //www.chunkedTransfer = false;
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError) {
            print(request.error);
        } else {
            if (request.isDone) {
                string jsonResult =
                    System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                print(jsonResult);

            }
        }

    }

    public class Temp {
        public string id = "909";
        public string player = "hello world!";
    }

    IEnumerator PostGameState() {
        print("PostGameState 1");
        string url = "https://cn0izbewf2.execute-api.eu-west-2.amazonaws.com/alpha2/games";
        string gameStateString = GameController.GameEngine.GameState.Stringify();
        string postData = JsonUtility.ToJson(new Temp());

        UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);

        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(postData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        print("PostGameState 2");
        yield return request.SendWebRequest();
        print("PostGameState 3");
        if (request.isNetworkError || request.isHttpError) {
            print(request.error);
            print(request.responseCode);
            string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
            print(jsonResult);

        } else {
            if (request.isDone) {
                string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                print(jsonResult);

            }
        }

    }

}
