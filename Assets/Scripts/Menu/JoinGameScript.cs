using System;
using System.Collections;
using System.Collections.Generic;
using NetworkModels;
using UnityEngine;

public class JoinGameScript : MonoBehaviour {

    private static readonly float StartingAxisY = 150f;
    private static readonly float SpaceBetweenTexts = 150f;

    private void Start() {
        StartCoroutine(NetworkController.GetAllGames(HandleAction));
    }

    private void OnGameItemClick(object game) {
        if (game.GetType() == typeof(Game)) {
            StartCoroutine(NetworkController.GetGameState((game as Game).Id));
        }
    }

    void HandleAction(ResponseOrError<List<NetworkModels.Game>> responseOrError) {

        if (responseOrError.IsSuccess) {
            for (int i = 0; i < responseOrError.Response.Count; i++) {
                Game game = responseOrError.Response[i];
                float y = StartingAxisY - i * SpaceBetweenTexts;
                string text = game.CreatorName + "'s game, players " + game.PlayersNow + "/" + game.PlayersMax;
                var obj = CardsGenerator.DrawObjectWithTextFromPrefab(new Vector2(0, y), "DefaultTextWhite", text);
                var script = obj.AddComponent<ClickActionScript>();
                script.ClickMethod = OnGameItemClick;
                script.ClickParameter = game;
            }
        }
    }
}
