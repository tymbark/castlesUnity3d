using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using NetworkModels;

public class NewGameController : MonoBehaviour {

    void Start() {

        GameObject.Find("2Players")
                  .GetComponent<ClickActionScript>()
                  .ClickMethod = obj => { SendCreateGameRequest(2); };

        GameObject.Find("3Players")
                  .GetComponent<ClickActionScript>()
                  .ClickMethod = (obj) => { SendCreateGameRequest(3); };

        GameObject.Find("4Players")
                  .GetComponent<ClickActionScript>()
                  .ClickMethod = (obj) => { SendCreateGameRequest(4); };

    }

    private void SendCreateGameRequest(int howMany) {
        //GameState newGameState = GameStateGenerator.GenerateGameState(howMany);
        Game newGame = new Game("Testid01", true, "damian", howMany, 1, new List<string>());
        StartCoroutine(NetworkController.CreateNewGame(newGame, HandleResponse));
    }

    void HandleResponse(bool success) {
        if (success) {
            SceneLoader.LoadWaitingRoomScene();
        } else {
            print("error");
        }
    }

}
