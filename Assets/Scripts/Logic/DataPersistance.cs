using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class DataPersistance {

    private static string GAME_STATE_KEY = "game_state_cob";

    public static void Save(this GameState gameState) {
        string gameStateString = gameState.Stringify();
        PlayerPrefs.SetString(GAME_STATE_KEY, gameStateString);
    }

    public static bool GameStateExists() {
        return PlayerPrefs.HasKey(GAME_STATE_KEY);
    }

    public static GameState LoadGameState() {
        Debug.Log("loading game state...");
        string gameStateString = PlayerPrefs.GetString(GAME_STATE_KEY);
        return gameStateString.ParseToGameState();
    }

}
