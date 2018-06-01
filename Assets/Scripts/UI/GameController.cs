using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using NetworkModels;
using InputActions;
using UnityEngine.UI;
using GSP = GameStateProvider;

public class GameController : MonoBehaviour {

    public bool ClicksEnabled = true;
    private bool YourTurnPopupShowed = true;
    private GameEngine GameEngine;
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

        if (GSP.GameState.ItsMyTurn()) {
            if (GSP.GameState.HasNotStarted()) {
                GameEngine.StartGame();
            }
        }

        UpdateView();
        CheckTheTurn();
    }

    private void GetGameStateRequest() {
        StartCoroutine(NetworkController.GetGameState(CurrentGameId, GameStateResponse));
    }

    private void PostGameStateRequest() {
        StartCoroutine(NetworkController.PostGameState(GSP.GameState, (bool success) => {
            if (success) {
                CheckTheTurn();
            }
        }));
    }

    private void CheckTheTurn() {
        if (!GSP.GameState.ItsMyTurn()) {
            YourTurnPopupShowed = false;
            Invoke("GetGameStateRequest", 4);
        } else {
            if (!YourTurnPopupShowed) {
                PopupsController.ShowYourTurnPopup();
            }
            YourTurnPopupShowed = true;
        }
    }

    private void GameStateResponse(ResponseOrError<GameState> responseOrError) {
        if (responseOrError.IsSuccess) {
            var newGameState = responseOrError.Response;

            if (!GSP.GameState.IsEqualTo(newGameState)) {
                newGameState.SaveGameState();
                UpdateView();
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
        print("redraw UI view");
        foreach (GameObject gmo in GarbageCollector) {
            Destroy(gmo);
        }
        GarbageCollector.Clear();
        GarbageCollector = GameBoardGenerator.DrawGameBoard(GSP.GameState);

        if (GSP.GameState.IsFinished) {
            SceneLoader.LoadGameFinishedScene();
        } else {
            ShowOrHideScreenBlockerWaitForTurn();
        }

    }

    private void ShowOrHideScreenBlockerWaitForTurn() {
        if (!GSP.GameState.ItsMyTurn()) {
            print("its not my turn -> show UI blocker");
            GarbageCollector.Add(PopupsController.ShowMessageWaitForYourTurn(GSP.GameState.CurrentPlayerNickName));
            ClicksEnabled = false;
        } else {
            print("its my turn...");
            ClicksEnabled = true;
        }
    }

    public void HandleClickAction(ClickAction action) {
        print("handle click action " + action);
        List<Action> actions = GSP.GameState.GetAvailableActions();

        switch (action) {
            case ClickAction.UseSilver:
                PopupsController.ShowAreYouSurePopup(() => {
                    actions.GetUseSilverAction().ProcessAction();
                    UpdateView();
                });
                break;
            case ClickAction.EndTurn:
                if (actions.HasEndTurnAction()) {
                    actions.GetEndTurnAction().ProcessAction();

                    UpdateView();
                    PostGameStateRequest();

                } else {
                    PopupsController.ShowMessageCannotFinishTurn();
                }
                break;
            case ClickAction.ExitGame:

                if (GSP.GameState.CurrentPlayer.Cards.Count < 2) {
                    GSP.GameState.CurrentPlayer.Cards.Add(GSP.GameState.MainDeck.DrawCard());
                } else {
                    GSP.GameState.AvailableProjectCards.Add(new ProjectCard(GSP.GameState.MainDeck.DrawCard(), CardDice.I));
                }
                UpdateView();

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

        List<Action> actions = GSP.GameState.GetAvailableActions();

        if (actions.IsMoveAvailable(targetCard, actionCard)) {
            Action actionForExecute = actions.GetAvailableMove(targetCard, actionCard);

            actionForExecute.ProcessAction(() => {
                UpdateView();
                CheckTheTurn();
            });
        }
    }

    public void HandleCardHoverAction(GameObject playerCardObject, GameObject targetCardObject) {
        Card targetCard = targetCardObject.GetDragDropController().Card;
        Card actionCard = playerCardObject.GetCardController().Card;

        List<Action> actions = GSP.GameState.GetAvailableActions();

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

    public static void UpdateView() {
        GameObject.Find("GameObjectController").GetComponent<GameController>().RedrawUI();
    }
}
