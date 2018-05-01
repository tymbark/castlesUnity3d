using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticCardsController : MonoBehaviour, IPointerClickHandler {

    public Models.Card Card;
    public bool executable = false;
    public bool clickable = false;
    private GameController GameController;

    void Start () {
        GameObject gameObj = GameObject.Find("GameObjectController");
        GameController = gameObj.GetComponent<GameController>();
	}
	
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData) {
        if (GameController.ClicksEnabled) {
            print("clicked");
            if (clickable) {
                GameController.HandleClickAction(Card);
            }    
        }
    }


}
