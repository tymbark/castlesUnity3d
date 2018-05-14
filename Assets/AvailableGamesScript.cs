using System.Collections;
using System.Collections.Generic;
using NetworkModels;
using UnityEngine;

public class AvailableGamesScript : MonoBehaviour {

    private void Start() {
        StartCoroutine(NetworkController.GetAllGames(HandleAction));
    }

    void HandleAction(ResponseOrError<System.Collections.Generic.List<NetworkModels.Game>> obj) {

        if (obj.IsSuccess) {
            for (int i = 0; i < obj.Response.Count; i++) {
                Game game = obj.Response[i];
                CardsGenerator.DrawObjectWithTextFromPrefab(new Vector2(0, i * 100), "DefaultText", game.Id);
            }
        }
    }

}
