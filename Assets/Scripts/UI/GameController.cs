using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public bool ClicksEnabled { get; private set; }
    public GameEngine GameEngine { get; private set; }
    private List<GameObject> Trash = new List<GameObject>();
    private List<GameObject> DynamicCardsGO = new List<GameObject>();
    private ActionHandler ActionHandler;
    private GameBoardGenerator GameBoardGenerator = new GameBoardGenerator();

    private void Awake() {
        GameEngine = new GameEngine();
        ActionHandler = GameEngine.ActionHandler;
    }

    void Start() {
        RefreshTable();
        ClicksEnabled = true;
    }

    // Update is called once per frame
    void Update() {

    }

    void RefreshTable() {
        foreach (GameObject gmo in DynamicCardsGO) {
            Destroy(gmo);
        }
        DynamicCardsGO = GameBoardGenerator.DrawGameBoard(GameEngine);
    }

    private void ShowMessageCannotFinishTurn() {
        ClicksEnabled = false;
        Object prefab = Resources.Load("Prefabs/TextUseACard");
        print("prefab" + prefab);
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = Vector3.one;

        GameObject canvas = GameObject.Find("Canvas");
        messageGameObject.transform.SetParent(canvas.transform);
        Trash.Add(messageGameObject);
        Invoke("DestroyUnwantedObjects", 3f);
    }

    public void DestroyUnwantedObjects() {
        foreach (GameObject obj in Trash) {
            Destroy(obj);
        }
        Trash.Clear();
        ClicksEnabled = true;
    }

    public void HandleClickAction(Card targetCard) {
        List<Action> actions = GameEngine.ActionHandler.GetAvailableActions();

        switch (targetCard.Class) {
            case CardClass.EndTurn:
                if (actions.HasEndTurnAction()) {
                } else {
                    ShowMessageCannotFinishTurn();
                }
                break;
            case CardClass.Options:
                break;
            case CardClass.Exit:
                break;
            case CardClass.AllProjects:
                break;
            case CardClass.AllEstates:
                break;
            case CardClass.AllAnimals:
                break;
            case CardClass.AllBonuses:
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
