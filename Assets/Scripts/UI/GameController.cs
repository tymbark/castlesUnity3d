using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using NetworkModels;
using InputActions;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public bool ClicksEnabled = true;
    public GameEngine GameEngine { get; private set; }
    private List<GameObject> GarbageCollector = new List<GameObject>();
    private GameBoardGenerator GameBoardGenerator = new GameBoardGenerator();
    private PopupsController PopupsController;
    private string CurrentGameId;

    private void Awake() {
        GameEngine = new GameEngine();
    }

    void Start() {
        if (!DataPersistance.GameStateExists()) {
            throw new System.InvalidProgramException("Game state don't exist!");
        }
        CurrentGameId = DataPersistance.GetCurrentGameId();
        PopupsController = GetComponent<PopupsController>();

        if (GameEngine.GameState.ItsMyTurn()) {
            if (GameEngine.GameState.HasNotStarted()) {
                GameEngine.StartGame();
            }
        }

        RedrawUI();
        CheckTheTurn();
    }

    private void GetGameStateRequest() {
        StartCoroutine(NetworkController.GetGameState(CurrentGameId, GameStateResponse));
    }

    private void PostGameStateRequest() {
        StartCoroutine(NetworkController.PostGameState(GameEngine.GameState, (bool success) => {
            if (success) {
                CheckTheTurn();
            }
        }));
    }

    private void CheckTheTurn() {
        if (!GameEngine.GameState.ItsMyTurn()) {
            Invoke("GetGameStateRequest", 4);
        } else {
            // PLAY
        }
    }

    private void GameStateResponse(ResponseOrError<GameState> responseOrError) {
        if (responseOrError.IsSuccess) {
            var newGameState = responseOrError.Response;

            if (!GameEngine.GameState.IsEqualTo(newGameState)) {
                UpdateGameState(newGameState);
                RedrawUI();
                print("game state has changed... how refreshing");
            } else {
                print("game state hasn't changed");
            }

            CheckTheTurn();

        } else {
            CheckTheTurn();
        }
    }

    private void RedrawUI() {
        print("redraw UI");
        foreach (GameObject gmo in GarbageCollector) {
            Destroy(gmo);
        }
        GarbageCollector.Clear();
        GarbageCollector = GameBoardGenerator.DrawGameBoard(GameEngine.GameState);

        if (GameEngine.GameState.IsFinished) {
            SceneLoader.LoadGameFinishedScene();
        } else {
            ShowOrHideScreenBlockerWaitForTurn();
        }

    }

    private void UpdateGameState(GameState gameState) {
        gameState.SaveGameState();
        GameEngine.UpdateGameState();
    }

    private void ShowOrHideScreenBlockerWaitForTurn() {
        if (!GameEngine.GameState.ItsMyTurn()) {
            print("its not my turn -> show UI blocker");
            GarbageCollector.Add(PopupsController.ShowMessageWaitForYourTurn(GameEngine.GameState.CurrentPlayerNickName));
            ClicksEnabled = false;
        } else {
            print("its my turn...");
            ClicksEnabled = true;
        }
    }

    public void HandleClickAction(ClickAction action) {
        print("handle click action " + action);
        List<Action> actions = GameEngine.GameState.GetAvailableActions();

        switch (action) {
            case ClickAction.UseSilver:
                PopupsController.ShowAreYouSurePopup(() => {
                    GameEngine.ProcessAction(actions.GetUseSilverAction(), SceneLoader.LoadChooseBonusScene);
                    UpdateGameState(GameEngine.GameState);
                    RedrawUI();
                });
                break;
            case ClickAction.EndTurn:
                if (actions.HasEndTurnAction()) {
                    GameEngine.ProcessAction(actions.GetEndTurnAction());

                    UpdateGameState(GameEngine.GameState);
                    RedrawUI();
                    PostGameStateRequest();

                } else {
                    PopupsController.ShowMessageCannotFinishTurn();
                }
                break;
            case ClickAction.ExitGame:
                //SceneLoader.LoadChooseBonusScene();
                PopupsController.ShowChooseAnimalPopup(2);
                print("todo exit game ...");
                break;
            case ClickAction.ShowProjects:
            case ClickAction.ShowWorkers:
            case ClickAction.ShowSilver:
                SceneLoader.LoadProjectsScene();
                break;
            case ClickAction.ShowEstates:
                SceneLoader.LoadEstatesScene();
                break;
            case ClickAction.ShowAnimals:
                SceneLoader.LoadAnimalsScene();
                break;
            case ClickAction.ShowBonuses:
                SceneLoader.LoadBonusesTakenScene();
                break;
            case ClickAction.ShowStorage:
                SceneLoader.LoadGoodsScene();
                break;
        }
    }

    public void HandleCardDroppedAction(GameObject playerCardObject, GameObject targetCardObject) {
        targetCardObject.ResetColor();

        Card targetCard = targetCardObject.GetDragDropController().Card;
        Card actionCard = playerCardObject.GetCardController().Card;

        List<Action> actions = GameEngine.GameState.GetAvailableActions();

        if (actions.IsMoveAvailable(targetCard, actionCard)) {
            Action actionForExecute = actions.GetAvailableMove(targetCard, actionCard);

            GameEngine.ProcessAction(actionForExecute);
            UpdateGameState(GameEngine.GameState);
            RedrawUI();
            CheckTheTurn();
        }
    }

    public void HandleCardHoverAction(GameObject playerCardObject, GameObject targetCardObject) {
        Card targetCard = targetCardObject.GetDragDropController().Card;
        Card actionCard = playerCardObject.GetCardController().Card;

        List<Action> actions = GameEngine.GameState.GetAvailableActions();

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
