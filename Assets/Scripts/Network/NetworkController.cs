﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Models;
using NetworkModels;

public class NetworkController {

    private static readonly string BASE_URL = "https://cn0izbewf2.execute-api.eu-west-2.amazonaws.com/beta3";

    private static void print(object message) {
        0.print_(message.ToString());
    }

    void DoSomething(ResponseOrError<List<Game>> responseOrError) {
        print(responseOrError.IsError);
        print(responseOrError.Response[0].CreatorName);
    }

    public static IEnumerator GetAllGames(System.Action<ResponseOrError<List<Game>>> action) {
        string url = BASE_URL + "/games";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError) {
            print("HTTP ERROR!\n" + request.error);
            action(new ResponseOrError<List<Game>>("request failed"));
        } else {
            if (request.isDone) {
                string jsonResult =
                    System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);

                jsonResult = jsonResult.Replace(" ", "");
                jsonResult = jsonResult.Replace("\n", "");

                print("HTTP SUCCESS!\n" + jsonResult);

                var games = jsonResult.ParseToListOfGames();
                action(new ResponseOrError<List<Game>>(games));

            }
        }
    }

    public static IEnumerator GetGameState() {
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

    public static IEnumerator PostGameState(GameState gameState) {
        print("PostGameState 1");
        string url = "https://cn0izbewf2.execute-api.eu-west-2.amazonaws.com/alpha2/games";
        string gameStateString = gameState.Stringify();
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
