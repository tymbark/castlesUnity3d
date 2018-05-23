using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

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

        //List<Card> cards = new List<Card>();
        //cards.Add(new Card(CardClass.Cow));
        //cards.Add(new Card(CardClass.Pig));
        //PopupsController.ShowChooseAnimalPopup(cards);

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
