using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using NetworkModels;
using InputActions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public bool ClicksEnabled = true;
    public GameEngine GameEngine { get; private set; }
    private List<GameObject> GUIobjects = new List<GameObject>();
    private ActionHandler ActionHandler;
    private GameBoardGenerator GameBoardGenerator = new GameBoardGenerator();
    private PopupsController PopupsController;
    private string CurrentGameId;

    private void Awake() {
        GameEngine = new GameEngine();
        ActionHandler = GameEngine.ActionHandler;
    }

    void Start() {
        if (!DataPersistance.GameStateExists()) {
            throw new System.InvalidProgramException("Game state don't exist!");
        }
        CurrentGameId = DataPersistance.LoadGameState().Id;
        PopupsController = GetComponent<PopupsController>();
        RefreshTable();
        GetGameStateFromServer();
    }

    private void GetGameStateFromServer() {
        StartCoroutine(NetworkController.GetGameState(CurrentGameId, GameStateResponse));
    }

    private void GameStateResponse(ResponseOrError<GameState> responseOrError) {
        if (responseOrError.IsSuccess) {
            var newGameState = responseOrError.Response;
            if (!GameEngine.GameState.IsEqualTo(newGameState)) {
                newGameState.SaveGameState();
                GameEngine.Refresh();
                RefreshTable();
                print("game state has changed... how refreshing");
            } else {
                print("game state hasn't changed");
            }

            if (!(newGameState.CurrentPlayer.NickName == DataPersistance.GetPlayerNickName())) {
                Invoke("GetGameStateFromServer", 3);
            }

        } else {
            Invoke("GetGameStateFromServer", 5);
        }
    }

    public void RefreshTable() {
        foreach (GameObject gmo in GUIobjects) {
            Destroy(gmo);
        }
        GUIobjects = GameBoardGenerator.DrawGameBoard(GameEngine);
        GameEngine.GameState.SaveGameState();
    }

    public void HandleClickAction(ClickAction action) {
        print("handle click action " + action);
        List<Action> actions = GameEngine.ActionHandler.GetAvailableActions();

        switch (action) {
            case ClickAction.EndTurn:
                if (actions.HasEndTurnAction()) {
                    ActionHandler.ProcessAction(actions.GetEndTurnAction());
                    PopupsController.ShowMessageNextTurn();
                } else {
                    PopupsController.ShowMessageCannotFinishTurn();
                }
                break;
            case ClickAction.ExitGame:
                print("todo exit game ...");
                break;
            case ClickAction.ShowProjects:
            case ClickAction.ShowWorkers:
            case ClickAction.ShowSilver:
                SceneManager.LoadScene("Projects");
                break;
            case ClickAction.ShowEstates:
                SceneManager.LoadScene("Estates");
                break;
            case ClickAction.ShowAnimals:
                SceneManager.LoadScene("Animals");
                break;
            case ClickAction.ShowBonuses:
                print("todo open bonuses ...");
                break;
            case ClickAction.ShowStorage:
                SceneManager.LoadScene("Goods");
                break;
        }
    }

    public void HandleCardDroppedAction(GameObject playerCardObject, GameObject targetCardObject) {
        targetCardObject.ResetColor();

        Card targetCard = targetCardObject.GetDragDropController().Card;
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
        Card targetCard = targetCardObject.GetDragDropController().Card;
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
        DropCardController controller = targetCardObject.GetDragDropController();
    }
}
