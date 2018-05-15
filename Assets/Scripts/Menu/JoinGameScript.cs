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

    void HandleAction(ResponseOrError<List<NetworkModels.Game>> responseOrError) {

        if (responseOrError.IsSuccess) {
            for (int i = 0; i < responseOrError.Response.Count; i++) {
                Game game = responseOrError.Response[i];
                float y = StartingAxisY - i * SpaceBetweenTexts;
                string text = game.CreatorName + "'s game, players " + game.PlayersNow + "/" + game.PlayersMax;
                var obj = CardsGenerator.DrawObjectWithTextFromPrefab(new Vector2(0, y), "DefaultTextWhite", text);
                var script = obj.AddComponent<ClickActionScript>();
                script.action = ClickAction;
                script.game = game;
            }
        }
    }

    private void ClickAction(Game game) {
        StartCoroutine(NetworkController.GetGameState(game.Id));
    }
}
