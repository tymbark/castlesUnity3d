using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class GameStateProvider {

    private static GameState _gameState;

    public static void SaveGameState(this GameState gameState) {
        string gameStateString = gameState.Stringify();
        DataPersistance.SaveGameState(gameState);
        _gameState = DataPersistance.LoadGameState();
    }

    public static GameState GameState {
        get {
            if (_gameState == null) {
                _gameState = DataPersistance.LoadGameState();
                return _gameState;
            } else {
                return _gameState;
            }
        }
    }

}

