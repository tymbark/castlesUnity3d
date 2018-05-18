using System;
using System.Collections;
using System.Collections.Generic;
using NetworkModels;
using Models;
using UnityEngine;

public class JoinGameController : MonoBehaviour {

    private static readonly float StartingAxisY = 150f;
    private static readonly float SpaceBetweenTexts = 150f;
    private List<GameObject> GarbageCollector = new List<GameObject>();

    private void Start() {
        GetAvailableGames();
    }

    private void GetAvailableGames() {
        StartCoroutine(NetworkController.GetAllGameInfos(AllGamesResponse));
    }

    private void AllGamesResponse(ResponseOrError<List<GameInfo>> responseOrError) {

        foreach (GameObject gameObj in GarbageCollector) {
            Destroy(gameObj);
        }

        if (responseOrError.IsSuccess) {
            for (int i = 0; i < responseOrError.Response.Count; i++) {
                GameInfo game = responseOrError.Response[i];
                float y = StartingAxisY - i * SpaceBetweenTexts;
                string text = game.CreatorName + "'s game, players " + game.PlayersNow + "/" + game.PlayersMax;
                var obj = CardsGenerator.DrawObjectWithTextFromPrefab(new Vector2(0, y), "DefaultTextWhite", text);
                var script = obj.AddComponent<ClickActionScript>();
                script.ClickMethod = OnGameItemClick;
                script.ClickParameter = game;
                GarbageCollector.Add(obj);
            }
        }

        Invoke("GetAvailableGames", 3);
    }

    private void OnGameItemClick(object game) {
        if (game.GetType() == typeof(GameInfo)) {
            GameInfo gameInfo = (game as GameInfo);
            gameInfo.PlayersIds.Add(DataPersistance.GetPlayerId());
            gameInfo.PlayersNow = gameInfo.PlayersNow + 1;
            DataPersistance.SaveCurrentGameId(gameInfo.Id);
            StartCoroutine(NetworkController.PostGameInfo(gameInfo, PostGameInfoResponse));
        }
    }

    private void PostGameInfoResponse(bool success) {
        if (success) {
            SceneLoader.LoadWaitingRoomScene();
        } else {
            print("failed to join game (post game info)");
        }
    }

}