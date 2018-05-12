using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class NetworkController : MonoBehaviour {

    // Use this for initialization
    void Start() {
        StartCoroutine(ExampleRequest());
    }

    IEnumerator ExampleRequest() {
        string getCountriesUrl = "https://cn0izbewf2.execute-api.eu-west-2.amazonaws.com/alpha1/games/1";
        using (UnityWebRequest request = UnityWebRequest.Get(getCountriesUrl)) {
            //www.chunkedTransfer = false;
            yield return request.Send();
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
    }

    // Update is called once per frame
    void Update() {

    }
}
