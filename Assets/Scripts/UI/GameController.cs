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
    public bool EndTurnParticleSystemVisible = false;
    private bool YourTurnPopupShowed = true;
    private GameEngine GameEngine;
    private List<GameObject> GarbageCollector = new List<GameObject>();
    private GameBoardGenerator GameBoardGenerator = new GameBoardGenerator();
    private PopupsController PopupsController;
    private string CurrentGameId;

    private void Awake() {
        GameEngine = new GameEngine();
    }

    private void Update() {
        var ps = GameObject.Find("Canvas").GetComponentInChildren<ParticleSystem>();
        if (EndTurnParticleSystemVisible) {
            ps.Play();
        } else {
            ps.Stop();
        }
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

        UpdateUI();
        CheckTheTurn();

        if (GSP.GameState.GetAvailableActions().FindAll((Action obj) => obj.Type == ActionType.EndTurn).Count == 1) {
            EndTurnParticleSystemVisible = true;
        } else {
            EndTurnParticleSystemVisible = false;
        }
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
                UpdateUI();
                print("game state has changed... how refreshing");
            } else {
                print("game state hasn't changed");
            }

            CheckTheTurn();

        } else {
            CheckTheTurn();
        }
    }

    private void UpdateUI() {
        print("redraw UI view");
        GameObject.Find("Canvas").transform.position = new Vector3(0, 0, 0);
        GameObject.Find("Canvas").transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

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

        GameObject.Find("Canvas").transform.position = new Vector3(0, 0, 20);
        GameObject.Find("Canvas").transform.rotation = Quaternion.Euler(new Vector3(5, 0, 0));

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
                    UpdateUI();
                });
                break;
            case ClickAction.EndTurn:
                if (actions.HasEndTurnAction()) {
                    actions.GetEndTurnAction().ProcessAction();

                    UpdateUI();
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
                UpdateUI();

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
                UpdateUI();
                CheckTheTurn();
            });
        }

        if (GSP.GameState.GetAvailableActions().FindAll((Action obj) => obj.Type == ActionType.EndTurn).Count == 1) {
            EndTurnParticleSystemVisible = true;
        } else {
            EndTurnParticleSystemVisible = false;
        }

    }

    public void HandleCardHoverAction(GameObject playerCardObject, GameObject targetCardObject) {
        Card targetCard = targetCardObject.GetDragDropController().Card;
        Card actionCard = playerCardObject.GetCardController().Card;

        List<Action> actions = GSP.GameState.GetAvailableActions();

        if (actions.IsMoveAvailable(targetCard, actionCard)) {
            targetCardObject.SetBlueColor();
        } else {
            if (targetCardObject.GetDragDropController().DragDropAction != DragDropAction.AddSilverProject) {
                targetCardObject.SetRedColor();
            }
        }

    }

    public void HandleCardLeaveAction(GameObject playerCardObject, GameObject targetCardObject) {
        targetCardObject.ResetColor();
        DropCardController controller = targetCardObject.GetDragDropController();
    }

}
