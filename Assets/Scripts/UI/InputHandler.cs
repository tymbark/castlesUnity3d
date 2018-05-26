using UnityEngine;
using System.Collections.Generic;
using Models;
using GSP = GameStateProvider;

public class InputHandler : MonoBehaviour {

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 0) {
                availableActions[0].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 1) {
                availableActions[1].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 2) {
                availableActions[2].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 3) {
                availableActions[3].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 4) {
                availableActions[4].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 5) {
                availableActions[5].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 6) {
                availableActions[6].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 7) {
                availableActions[7].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 8) {
                availableActions[8].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 9) {
                availableActions[9].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 10) {
                availableActions[10].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 11) {
                availableActions[11].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 12) {
                availableActions[12].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 13) {
                availableActions[13].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 14) {
                availableActions[14].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 15) {
                availableActions[15].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 16) {
                availableActions[16].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 17) {
                availableActions[17].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 18) {
                availableActions[18].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            List<Action> availableActions = GSP.GameState.GetAvailableActions();
            if (availableActions.Count > 19) {
                availableActions[19].ProcessAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            print(Utils.GameStatusString());
        }
    }
}
