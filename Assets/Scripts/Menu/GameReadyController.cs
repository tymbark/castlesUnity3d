using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using NetworkModels;
using System;

public class GameReadyController : MonoBehaviour {

    private string currentGameId;
    private string playerId;

    private void Start() {
        currentGameId = DataPersistance.GetCurrentGameId();
        playerId = DataPersistance.GetPlayerNickName();
        GetGameState();
    }

    private void GetGameState() {
        StartCoroutine(NetworkController.GetGameState(currentGameId, GameStateResponse));
    }

    private void GameStateResponse(ResponseOrError<GameState> responseOrError) {
        if (responseOrError.IsSuccess) {
            responseOrError.Response.SaveGameState();
            Invoke("OpenMainGameBoard", 1);
        } else {
            print("cannot get game state, retry in 3 sec");
            Invoke("GetGameState", 2);
        }
    }

    private void OpenMainGameBoard() {
        SceneLoader.LoadMainGameScene();
    }


}
