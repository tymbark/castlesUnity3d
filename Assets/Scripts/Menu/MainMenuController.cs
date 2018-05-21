using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

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

        CardsGenerator.DrawSilverCard(4, false);
        CardsGenerator.DrawStorageCard(4);
        CardsGenerator.DrawEstateCard(1);
        CardsGenerator.DrawProjectsCard(2);
        CardsGenerator.DrawPointsElement(2);

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
