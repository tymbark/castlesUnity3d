using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputActions;
using Models;
using UnityEngine.EventSystems;

public class DropCardController : MonoBehaviour {

    private Card _Card;
    public Card Card { get { return GetCard(); } set { _Card = value; } }
    public DragDropAction DragDropAction;

    private Card GetCard() {
        switch (DragDropAction) {
            case DragDropAction.BuyWorkers:
                return new Card(CardClass.Worker);
            case DragDropAction.BuySilver:
                return new Card(CardClass.Silver);
            case DragDropAction.SellSilverAndWorkers:
                return new Card(CardClass.SellSilverAndWorkers);
            case DragDropAction.ShipGoods:
                return new Card(CardClass.ShipGoods);
            case DragDropAction.TakeProject:
                return _Card;
            case DragDropAction.BuildProject:
                return _Card;
        }

        throw new System.InvalidProgramException("There is no card for this action");
    }


}
