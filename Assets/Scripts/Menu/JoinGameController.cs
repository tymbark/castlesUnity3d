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
    private string PlayerNickName;

    private void Start() {
        GetAvailableGames();
        PlayerNickName = DataPersistance.GetPlayerNickName();
    }

    private void GetAvailableGames() {
        StartCoroutine(NetworkController.GetAllGameInfos(AllGameInfosResponse));
    }

    private void AllGameInfosResponse(ResponseOrError<List<GameInfo>> responseOrError) {

        if (responseOrError.IsSuccess) {
            CleanGarbageCollector();

            List<GameInfo> info = responseOrError
                .Response
                .FindAll((obj) =>
                         obj.CreatorNickName != PlayerNickName
                         && obj.Available
                         && obj.PlayersNow < obj.PlayersMax);

            if (info.IsEmpty()) {
                string text = "no games available";
                var obj = CardsGenerator.DrawObjectWithTextFromPrefab(new Vector2(0, 0), "DefaultTextWhite", text);
                GarbageCollector.Add(obj);
            } else {
                for (int i = 0; i < info.Count; i++) {
                    GameInfo game = info[i];
                    float y = StartingAxisY - i * SpaceBetweenTexts;
                    string text = game.CreatorNickName + "'s game, players " + game.PlayersNow + "/" + game.PlayersMax;
                    var obj = CardsGenerator.DrawObjectWithTextFromPrefab(new Vector2(0, y), "DefaultTextWhite", text);
                    var script = obj.AddComponent<ClickActionScript>();
                    script.ClickMethod = OnGameItemClick;
                    script.ClickParameter = game;
                    GarbageCollector.Add(obj);
                }
            }
        }

        Invoke("GetAvailableGames", 3);
    }

    private void CleanGarbageCollector() {
        foreach (GameObject gameObj in GarbageCollector) {
            Destroy(gameObj);
        }
        GarbageCollector.Clear();
    }

    private void OnGameItemClick(object game) {
        if (game.GetType() == typeof(GameInfo)) {
            GameInfo gameInfo = (game as GameInfo);
            gameInfo.PlayersNicknames.Add(PlayerNickName);
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