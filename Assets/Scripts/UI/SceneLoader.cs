using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    private static readonly string NEW_GAME = "NewGame";
    private static readonly string JOIN_GAME = "JoinGame";
    private static readonly string GAME_READY = "GameReady";
    private static readonly string WAITING_ROOM = "WaitingRoom";

    private static readonly string MAIN_GAME = "MainGame";
    private static readonly string ESTATES = "Estates";
    private static readonly string ANIMALS = "Animals";
    private static readonly string GOODS = "Goods";
    private static readonly string PROJECTS = "Projects";

    public static void LoadWaitingRoomScene() {
        SceneManager.LoadScene(WAITING_ROOM);
    }

    public static void LoadNewGameScene() {
        SceneManager.LoadScene(NEW_GAME);
    }

    public static void LoadJoinGameScene() {
        SceneManager.LoadScene(JOIN_GAME);
    }

    public static void LoadGameReadyScene() {
        SceneManager.LoadScene(GAME_READY);
    }

    public static void LoadMainGameScene() {
        SceneManager.LoadScene(MAIN_GAME);
    }

    public static void LoadProjectsScene() {
        SceneManager.LoadScene(PROJECTS);
    }

    public static void LoadAnimalsScene() {
        SceneManager.LoadScene(ANIMALS);
    }

    public static void LoadEstatesScene() {
        SceneManager.LoadScene(ESTATES);
    }

    public static void LoadGoodsScene() {
        SceneManager.LoadScene(GOODS);
    }

}
