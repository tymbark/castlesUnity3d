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

    public void HandleCardDroppedAction(GameObject playerCard, GameObject targetCard) {
        print("HandleCardDroppedAction");
        StaticCardsController controller = targetCard.GetComponent<StaticCardsController>();
        if (controller == null) {
            return; // 2x PlayerHandCard 
        }
        if (controller.executable) {
            ResetColor(targetCard);
        }
    }

    public void HandleCardHoverAction(GameObject playerCard, GameObject targetCard) {
        print("HandleCardHoverAction");
        StaticCardsController controller = targetCard.GetComponent<StaticCardsController>();
        if (controller == null) {
            return; // 2x PlayerHandCard 
        }
        if (controller.executable) {
            SetBlueColor(targetCard);
        }
    }

    public void HandleCardLeaveAction(GameObject playerCard, GameObject targetCard) {
        print("HandleCardLeaveAction");
        StaticCardsController controller = targetCard.GetComponent<StaticCardsController>();
        if (controller == null){
            return; // 2x PlayerHandCard 
        }
        if (controller.executable) {
            ResetColor(targetCard);
        }
    }

    private void ResetColor(GameObject obj) {
        Image image = obj.GetComponent<Image>();
        image.color = new Color(1, 1, 1, 1);
    }

    private void SetBlueColor(GameObject obj) {
        Image image = obj.GetComponent<Image>();
        image.color = new Color(0.5f, 0.5f, 1, 1);
    }

}
