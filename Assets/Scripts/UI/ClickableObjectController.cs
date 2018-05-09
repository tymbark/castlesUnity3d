using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputActions;
using UnityEngine.EventSystems;

public class ClickableObjectController : MonoBehaviour, IPointerClickHandler {

    public ClickAction ClickAction;

    public void OnPointerClick(PointerEventData eventData) {
        GameObject gameObj = GameObject.Find("GameObjectController");

        if (gameObj == null) {
            print("click ignored...");
        } else {
            GameController gameController = gameObj.GetComponent<GameController>();

            if (gameController.ClicksEnabled && ClickAction != ClickAction.NotSet) {
                print("clicked");
                gameController.HandleClickAction(ClickAction);
            }

        }
    }
}
