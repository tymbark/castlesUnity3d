using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Models;
using NetworkModels;

public class NetworkController {

    private static readonly string STAGE = "beta6";
    private static readonly string BASE_URL = "https://cn0izbewf2.execute-api.eu-west-2.amazonaws.com/" + STAGE;

    private static void print(object message) {
        0.print_(message.ToString());
    }

    void DoSomething(ResponseOrError<List<GameInfo>> responseOrError) {
        print(responseOrError.IsError);
        print(responseOrError.Response[0].CreatorName);
    }

    public static IEnumerator GetAllGameInfos(System.Action<ResponseOrError<List<GameInfo>>> action) {
        string url = BASE_URL + "/gamestates";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError) {
            string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
            print("HTTP ERROR " + request.responseCode + "\n" + url + "\n" + request.error + "\n" + jsonResult);
            action(new ResponseOrError<List<GameInfo>>("request failed"));
        } else {
            if (request.isDone) {
                string jsonResult =
                    System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);

                jsonResult = jsonResult.Replace(" ", "");
                jsonResult = jsonResult.Replace("\n", "");

                print("HTTP SUCCESS\n" + jsonResult);

                var games = jsonResult.ParseToListOfGameInfos();
                action(new ResponseOrError<List<GameInfo>>(games));

            }
        }
    }

    public static IEnumerator GetGameInfo(string gameId, System.Action<ResponseOrError<GameInfo>> action) {
        string url = BASE_URL + "/gamestates/" + gameId;
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError) {
            string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
            print("HTTP ERROR " + request.responseCode + "\n" + url + "\n" + request.error + "\n" + jsonResult);
            action(new ResponseOrError<GameInfo>("request failed"));
        } else {
            if (request.isDone) {
                string jsonResult =
                    System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);

                jsonResult = jsonResult.Replace(" ", "");
                jsonResult = jsonResult.Replace("\n", "");

                print("HTTP SUCCESS\n" + jsonResult);

                var games = jsonResult.ParseToGameInfo();
                action(new ResponseOrError<GameInfo>(games));

            }
        }
    }

    public static IEnumerator PostGameInfo(GameInfo game, System.Action<bool> action) {
        string url = BASE_URL + "/gamestates";

        UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);

        string bodyJson = game.ToJson();
        print(bodyJson);

        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJson);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError) {
            string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
            print("HTTP ERROR " + request.responseCode + "\n" + url + "\n" + request.error + "\n" + jsonResult);
            action(false);
        } else {
            if (request.isDone) {
                string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                print("HTTP SUCCESS\n" + jsonResult);
                action(true);
            }
        }

    }

    public static IEnumerator GetGameState(string gameId, System.Action<ResponseOrError<GameState>> action) {
        string url = BASE_URL + "/games/" + gameId;
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError) {
            string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
            print("HTTP ERROR " + request.responseCode + "\n" + url + "\n" + request.error + "\n" + jsonResult);
            action(new ResponseOrError<GameState>("request failed"));
        } else {
            if (request.isDone) {
                string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                print("HTTP SUCCESS\n" + jsonResult);
            }
        }

    }

    public static IEnumerator PostGameState(GameState gameState, System.Action<bool> action) {
        string url = BASE_URL + "/games";
        string gameStateString = gameState.Stringify();
        var jsonBody = JsonUtility.ToJson(new GameStateWithId(gameState.Id, gameStateString));

        print(jsonBody);
        UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);

        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError) {
            string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
            print("HTTP ERROR " + request.responseCode + "\n" + url + "\n" + request.error + "\n" + jsonResult);
            action(false);
        } else {
            if (request.isDone) {
                string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                print("HTTP SUCCESS\n" + jsonResult);
                action(true);
            }
        }

    }

}
