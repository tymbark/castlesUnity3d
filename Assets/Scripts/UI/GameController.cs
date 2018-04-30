using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameEngine GameEngine { get; private set; }

    private void Awake() {
        GameEngine = new GameEngine();
    }

    void Start() {
        GameBoardGenerator.DrawGameBoard(GameEngine);
    }

    // Update is called once per frame
    void Update() {

    }

    public void HandleCardDroppedAction(GameObject playerCardObject, GameObject targetCardObject) {
        targetCardObject.ResetColor();
        StaticCardsController controller = targetCardObject.GetStaticController();
        Card targetCard = controller.Card;
        List<Action> actions = GameEngine.ActionHandler.GetAvailableActions();
    }

    public void HandleCardHoverAction(GameObject playerCardObject, GameObject targetCardObject) {
        StaticCardsController controller = targetCardObject.GetStaticController();
        Card targetCard = controller.Card;
        List<Action> actions = GameEngine.ActionHandler.GetAvailableActions();

        print(actions.Describe());

        switch (targetCard.Class) {
            
            case CardClass.Worker:
                if (actions.HasBuyWorkersAction()) {
                    targetCardObject.SetBlueColor();
                } else {
                    targetCardObject.SetRedColor();
                }

                break;

            case CardClass.Silver:
                if (actions.HasBuySilverAction()) {
                    targetCardObject.SetBlueColor();
                } else {
                    targetCardObject.SetRedColor();
                }
                break;

            case CardClass.AllStorages:
                if (actions.HasShipGoodsAction()) {
                    targetCardObject.SetBlueColor();
                } else {
                    targetCardObject.SetRedColor();
                }
                break;
        }

    }

    public void HandleCardLeaveAction(GameObject playerCardObject, GameObject targetCardObject) {
        targetCardObject.ResetColor();
        StaticCardsController controller = targetCardObject.GetStaticController();
    }
}
