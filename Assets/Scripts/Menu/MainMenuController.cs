using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class MainMenuController : MonoBehaviour {

    private void Awake() {
        //Screen.SetResolution(1920, 1080, false);
    }

    void Start() {

        GameObject.Find("NewGameButton")
                  .AddComponent<ClickActionScript>()
                  .ClickMethod = NewGameClicked;

        GameObject.Find("JoinGameButton")
                  .AddComponent<ClickActionScript>()
                  .ClickMethod = JoinGameClicked;

        GameObject.Find("OptionsButton")
                  .AddComponent<ClickActionScript>()
                  .ClickMethod = OptionsClicked;


    }

    private void NewGameClicked(object param) {
        SceneLoader.LoadNewGameScene();
    }

    private void JoinGameClicked(object param) {
        SceneLoader.LoadJoinGameScene();
    }

    private void OptionsClicked(object param) {
        SceneLoader.LoadOptionsScene();
    }
}
