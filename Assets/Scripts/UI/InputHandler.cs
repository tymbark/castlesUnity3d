using UnityEngine;
using System.Collections.Generic;
using Models;

public class InputHandler : MonoBehaviour {

    public GameEngine GameEngine;

    private void Start() {
        GameObject gameObj = GameObject.Find("GameObjectController");
        GameController gameController = gameObj.GetComponent<GameController>();

        GameEngine engine = gameController.GameEngine;
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 0) {
                GameEngine.ActionHandler.ProcessAction(availableActions[0]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 1) {
                GameEngine.ActionHandler.ProcessAction(availableActions[1]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 2) {
                GameEngine.ActionHandler.ProcessAction(availableActions[2]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 3) {
                GameEngine.ActionHandler.ProcessAction(availableActions[3]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 4) {
                GameEngine.ActionHandler.ProcessAction(availableActions[4]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 5) {
                GameEngine.ActionHandler.ProcessAction(availableActions[5]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 6) {
                GameEngine.ActionHandler.ProcessAction(availableActions[6]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 7) {
                GameEngine.ActionHandler.ProcessAction(availableActions[7]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 8) {
                GameEngine.ActionHandler.ProcessAction(availableActions[8]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 9) {
                GameEngine.ActionHandler.ProcessAction(availableActions[9]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 10) {
                GameEngine.ActionHandler.ProcessAction(availableActions[10]);
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 11) {
                GameEngine.ActionHandler.ProcessAction(availableActions[11]);
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 12) {
                GameEngine.ActionHandler.ProcessAction(availableActions[12]);
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 13) {
                GameEngine.ActionHandler.ProcessAction(availableActions[13]);
            }
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 14) {
                GameEngine.ActionHandler.ProcessAction(availableActions[14]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 15) {
                GameEngine.ActionHandler.ProcessAction(availableActions[15]);
            }
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 16) {
                GameEngine.ActionHandler.ProcessAction(availableActions[16]);
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 17) {
                GameEngine.ActionHandler.ProcessAction(availableActions[17]);
            }
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 18) {
                GameEngine.ActionHandler.ProcessAction(availableActions[18]);
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            List<Action> availableActions = GameEngine.ActionHandler.GetAvailableActions();
            if (availableActions.Count > 19) {
                GameEngine.ActionHandler.ProcessAction(availableActions[19]);
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            print(Utils.GameStatusString(GameEngine));
        }
    }
}
