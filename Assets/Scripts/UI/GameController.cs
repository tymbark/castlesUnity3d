using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
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



        print("Start!");
        string json = JsonUtility.ToJson(GameEngine.GameState.CurrentPlayer);
        var state = JsonUtility.FromJson<GameState>(json);
        print(json);

        //print("before" + GameEngine.GameState.MainDeck.Cards.Count);
        //print("before" + GameEngine.GameState.AvailableProjectCards.Describe());


        //print("after " + state.MainDeck.Cards.Count);
        //print("after " + state.AvailableProjectCards.Describe());

        //var d = GameEngine.GameState.AnimalsDeck;
        //print("size before :" + d.Cards.Count);
        //print("before: " + d.Stringify());
        //string s = d.Stringify();
        //var d2 = s.ParseToDeck();
        //print("size after :" + d2.Cards.Count);
        //print("before: " + d2.Stringify());

        //var x = GameEngine.GameState.AvailableProjectCards;
        //var x = new List<ProjectCard>();
        //print("before:" + x.Describe());
        //print(x.Stringify());
        //var x2 = x.Stringify().ParseToProjectCardList();
        //print("after:" + x2.Describe());

        var gs = GameEngine.GameState;

        var cp = GameEngine.GameState.CurrentPlayer;
        print("before: " + gs.AvailableProjectCards.Describe());
        print("before: " + gs.MainDeck.Cards.Describe());
        print("before: " + cp.Cards.Describe());
        print("before: " + cp.FutureCards.Describe());
        print("before: " + cp.Name);
        print("before: " + cp.Animals.Describe());
        print("before: " + cp.CompletedProjects.Describe());

        print(gs.Stringify());
        var gs2 = gs.Stringify().ParseToGameState();

        var cp2 = gs2.CurrentPlayer;
        print("afterr: " + gs2.AvailableProjectCards.Describe());
        print("afterr: " + gs2.MainDeck.Cards.Describe());
        print("afterr: " + cp2.Cards.Describe());
        print("afterr: " + cp2.FutureCards.Describe());
        print("afterr: " + cp2.Name);
        print("afterr: " + cp2.Animals.Describe());
        print("afterr: " + cp2.CompletedProjects.Describe());

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
                break;
            case CardClass.AllEstates:
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
