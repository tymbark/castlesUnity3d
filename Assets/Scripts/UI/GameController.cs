using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public bool ClicksEnabled = true;
    public GameEngine GameEngine { get; private set; }
    private List<GameObject> DynamicCardsGO = new List<GameObject>();
    private ActionHandler ActionHandler;
    private GameBoardGenerator GameBoardGenerator = new GameBoardGenerator();
    private PopupsController PopupsController;

    private void Awake() {
        GameEngine = new GameEngine();
        ActionHandler = GameEngine.ActionHandler;
    }

    void Start() {
        PopupsController = GetComponent<PopupsController>();
        RefreshTable();
    }

    void Update() {

    }

    public void RefreshTable() {
        foreach (GameObject gmo in DynamicCardsGO) {
            Destroy(gmo);
        }
        DynamicCardsGO = GameBoardGenerator.DrawGameBoard(GameEngine);
    }

    public void HandleClickAction(Card targetCard) {
        List<Action> actions = GameEngine.ActionHandler.GetAvailableActions();

        switch (targetCard.Class) {
            case CardClass.EndTurn:
                if (actions.HasEndTurnAction()) {
                    ActionHandler.ProcessAction(actions.GetEndTurnAction());
                    PopupsController.ShowMessageNextTurn();
                } else {
                    PopupsController.ShowMessageCannotFinishTurn();
                }
                break;
            case CardClass.Exit:
                break;
            case CardClass.AllProjects:
                SceneManager.LoadScene("Projects");
                break;
            case CardClass.AllEstates:
                SceneManager.LoadScene("Estates");
                break;
            case CardClass.AllAnimals:
                break;
            case CardClass.AllStorages:
                break;
        }
    }

    public void HandleCardDroppedAction(GameObject playerCardObject, GameObject targetCardObject) {
        targetCardObject.ResetColor();
        Card targetCard = targetCardObject.GetStaticController().Card;
        Card actionCard = playerCardObject.GetCardController().Card;

        List<Action> actions = GameEngine.ActionHandler.GetAvailableActions();
        print(actions.Describe());

        if (actions.IsMoveAvailable(targetCard, actionCard)) {
            Action actionForExecute = actions.GetAvailableMove(targetCard, actionCard);

            ActionHandler.ProcessAction(actionForExecute);
            RefreshTable();
        }

    }

    public void HandleCardHoverAction(GameObject playerCardObject, GameObject targetCardObject) {
        Card targetCard = targetCardObject.GetStaticController().Card;
        Card actionCard = playerCardObject.GetCardController().Card;

        List<Action> actions = GameEngine.ActionHandler.GetAvailableActions();

        if (actions.IsMoveAvailable(targetCard, actionCard)) {
            targetCardObject.SetBlueColor();
        } else {
            targetCardObject.SetRedColor();
        }

    }

    public void HandleCardLeaveAction(GameObject playerCardObject, GameObject targetCardObject) {
        targetCardObject.ResetColor();
        StaticCardsController controller = targetCardObject.GetStaticController();
    }
}
