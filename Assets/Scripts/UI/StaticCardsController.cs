using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputActions;
using UnityEngine.EventSystems;

public class StaticCardsController : MonoBehaviour, IPointerClickHandler {

    public Models.Card Card;
    public ClickAction ClickAction;
    public bool Executable = false;
    public bool Clickable = false;
    private GameController GameController;

    void Start() {
        GameObject gameObj = GameObject.Find("GameObjectController");
        GameController = gameObj.GetComponent<GameController>();
    }

    void Update() {

    }

    public void OnPointerClick(PointerEventData eventData) {
        if (GameController.ClicksEnabled) {
            print("clicked");

            if (Clickable) {

                if (Card == null) {
                    // use new mechanism -> click action

                    GameController.HandleClickAction(ClickAction.ShowBonuses);

                } else {
                    // use old mechanism -> card
                    switch (Card.Class) {
                        case Models.CardClass.EndTurn:
                            GameController.HandleClickAction(ClickAction.EndTurn);
                            break;
                        case Models.CardClass.Exit:
                            GameController.HandleClickAction(ClickAction.ExitGame);
                            break;
                        case Models.CardClass.AllProjects:
                            GameController.HandleClickAction(ClickAction.ShowProjects);
                            break;
                        case Models.CardClass.AllEstates:
                            GameController.HandleClickAction(ClickAction.ShowEstates);
                            break;
                        case Models.CardClass.AllAnimals:
                            GameController.HandleClickAction(ClickAction.ShowAnimals);
                            break;
                        case Models.CardClass.AllStorages:
                            GameController.HandleClickAction(ClickAction.ShowStorage);
                            break;
                        case Models.CardClass.SellSilverAndWorkers:
                            GameController.HandleClickAction(ClickAction.ShowSilver);
                            break;
                        case Models.CardClass.Silver:
                            GameController.HandleClickAction(ClickAction.ShowSilver);
                            break;
                        case Models.CardClass.Worker:
                            GameController.HandleClickAction(ClickAction.ShowWorkers);
                            break;
                    }
                }
            }
        }
    }


}
