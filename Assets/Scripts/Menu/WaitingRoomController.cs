using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using NetworkModels;
using System;

public class WaitingRoomController : MonoBehaviour {

    private readonly List<GameObject> GarbageCollector = new List<GameObject>();
    private string currentGameId;
    private string playerId;

    private void Start() {
        currentGameId = DataPersistance.GetCurrentGameId();
        playerId = DataPersistance.GetPlayerNickName();
        GetGameInfo();
    }

    private void GetGameInfo() {
        StartCoroutine(NetworkController.GetGameInfo(currentGameId, GameInfoResponse));
    }

    private void GameInfoResponse(ResponseOrError<GameInfo> responseOrError) {
        if (responseOrError.IsSuccess) {
            GameInfo response = responseOrError.Response;
            UpdateView(response);
            if (response.PlayersNow == response.PlayersMax) {
                print("all players are present, ready to start");

                if (response.CreatorNickName == playerId) {
                    CreateAndSendGameState(response);
                } else {
                    SceneLoader.LoadGameReadyScene();
                }

            } else {
                Invoke("GetGameInfo", 3);
            }
        } else {
            Invoke("GetGameInfo", 3);
        }
    }

    private void UpdateView(GameInfo gameInfo) {
        CleanGarbageCollector();
        string text = gameInfo.PlayersNow + "/" + gameInfo.PlayersMax + " joined";
        var obj = CardsGenerator.DrawObjectWithTextFromPrefab(new Vector2(0, 0), "DefaultTextWhite", text);
        var script = obj.AddComponent<ClickActionScript>();
        GarbageCollector.Add(obj);
    }

    private void CleanGarbageCollector() {
        foreach (GameObject gameObj in GarbageCollector) {
            Destroy(gameObj);
        }
        GarbageCollector.Clear();
    }

    private void CreateAndSendGameState(GameInfo gameInfo) {
        GameState gameState = GameStateGenerator
            .GenerateGameState(gameInfo.Id, gameInfo.PlayersMax, gameInfo.PlayersNicknames);

        StartCoroutine(NetworkController.PostGameState(gameState, GameStateCreatedResponse));

    }

    private void GameStateCreatedResponse(bool success) {
        if (success) {
            SceneLoader.LoadGameReadyScene();
        } else {
            print("cannot create game state...");
        }
    }
}
