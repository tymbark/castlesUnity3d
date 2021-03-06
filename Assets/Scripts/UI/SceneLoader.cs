﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    private static readonly string MAIN_MENU = "MainMenu";
    private static readonly string NEW_GAME = "NewGame";
    private static readonly string JOIN_GAME = "JoinGame";
    private static readonly string GAME_READY = "GameReady";
    private static readonly string WAITING_ROOM = "WaitingRoom";
    private static readonly string OPTIONS = "Options";

    private static readonly string MAIN_GAME = "MainGame";
    private static readonly string ESTATES = "Estates";
    private static readonly string ANIMALS = "Animals";
    private static readonly string GOODS = "Goods";
    private static readonly string PROJECTS = "Projects";
    private static readonly string BONUSES_TAKEN = "BonusesTaken";
    private static readonly string BONUSES_AVAILABLE = "BonusesAvailable";
    private static readonly string GAME_FINISHED = "GameFinished";

    public static void LoadMainMenuScene() {
        print("Load Scene " + MAIN_MENU);
        SceneManager.LoadScene(MAIN_MENU);
    }

    public static void LoadWaitingRoomScene() {
        print("Load Scene " + WAITING_ROOM);
        SceneManager.LoadScene(WAITING_ROOM);
    }

    public static void LoadNewGameScene() {
        print("Load Scene " + NEW_GAME);
        SceneManager.LoadScene(NEW_GAME);
    }

    public static void LoadJoinGameScene() {
        print("Load Scene " + JOIN_GAME);
        SceneManager.LoadScene(JOIN_GAME);
    }

    public static void LoadOptionsScene() {
        print("Load Scene " + OPTIONS);
        SceneManager.LoadScene(OPTIONS);
    }

    public static void LoadGameReadyScene() {
        print("Load Scene " + GAME_READY);
        SceneManager.LoadScene(GAME_READY);
    }

    public static void LoadMainGameScene() {
        print("Load Scene " + MAIN_GAME);
        SceneManager.LoadScene(MAIN_GAME);
    }

    public static void LoadProjectsScene() {
        print("Load Scene " + PROJECTS);
        SceneManager.LoadScene(PROJECTS);
    }

    public static void LoadAnimalsScene() {
        print("Load Scene " + ANIMALS);
        SceneManager.LoadScene(ANIMALS);
    }

    public static void LoadEstatesScene() {
        print("Load Scene " + ESTATES);
        SceneManager.LoadScene(ESTATES);
    }

    public static void LoadGoodsScene() {
        print("Load Scene " + GOODS);
        SceneManager.LoadScene(GOODS);
    }

    public static void LoadBonusesTakenScene() {
        print("Load Scene " + BONUSES_TAKEN);
        SceneManager.LoadScene(BONUSES_TAKEN);
    }

    public static void LoadBonusesAvailableScene() {
        print("Load Scene " + BONUSES_AVAILABLE);
        SceneManager.LoadScene(BONUSES_AVAILABLE);
    }

    public static void LoadGameFinishedScene() {
        print("Load Scene " + GAME_FINISHED);
        SceneManager.LoadScene(GAME_FINISHED);
    }

}
