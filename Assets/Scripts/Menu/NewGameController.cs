using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using NetworkModels;

public class NewGameController : MonoBehaviour {

    //todo remove it
    private GameInfo newGame;

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

        string newGameId = "GAME_" + ((int)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1))
                                   .TotalSeconds).ToString("X2");

        string creatorNickname = DataPersistance.GetPlayerNickName();

        List<string> playersIds = new List<string>();
        playersIds.Add(creatorNickname);
        //todo creator name
        newGame = new GameInfo(newGameId, true, creatorNickname, howMany, 1, playersIds);

        DataPersistance.SaveCurrentGameId(newGameId);
        StartCoroutine(NetworkController.PostGameInfo(newGame, NewGameCreatedResponse));
    }

    private void NewGameCreatedResponse(bool success) {
        if (success) {
            SceneLoader.LoadWaitingRoomScene();
        } else {
            print("handle UI error new game failed");
        }
    }

}
