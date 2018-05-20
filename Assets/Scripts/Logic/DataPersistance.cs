using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class DataPersistance {

    private static string GAME_STATE_KEY = "game_state";
    private static string CURRENT_GAME_ID_KEY = "game_key_id";
    private static string PLAYER_NICKNAME_KEY = "player_nickname";

    public static void SaveGameState(this GameState gameState) {
        string gameStateString = gameState.Stringify();
        PlayerPrefs.SetString(GAME_STATE_KEY, gameStateString);
    }

    public static bool GameStateExists() {
        return PlayerPrefs.HasKey(GAME_STATE_KEY);
    }

    public static GameState LoadGameState() {
        string gameStateString = PlayerPrefs.GetString(GAME_STATE_KEY);
        return gameStateString.ParseToGameState();
    }

    public static string GetPlayerNickName() {
        if (!PlayerPrefs.HasKey(PLAYER_NICKNAME_KEY)) {
            string newPlayerId = "PLAYER_" + ((int)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1))
                                           .TotalSeconds).ToString("X2");
            PlayerPrefs.SetString(PLAYER_NICKNAME_KEY, newPlayerId);
        }
        return PlayerPrefs.GetString(PLAYER_NICKNAME_KEY);
    }

    public static void SaveCurrentGameId(string currentGameId) {
        PlayerPrefs.SetString(CURRENT_GAME_ID_KEY, currentGameId);
    }

    public static string GetCurrentGameId() {
        if (!PlayerPrefs.HasKey(CURRENT_GAME_ID_KEY)) {
            throw new System.InvalidProgramException("Current game key does not exist!");
        }
        return PlayerPrefs.GetString(CURRENT_GAME_ID_KEY);
    }

}


//System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
//st.Start();
//st.Stop();
//Debug.Log(string.Format("MyMethod took {0} ms to complete", st.ElapsedMilliseconds));
