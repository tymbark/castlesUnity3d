﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using GD = GameDimensions;
using ED = EstatesDimensions;

public class EstatesController : MonoBehaviour {

    public GameEngine GameEngine { get; private set; }

    private void Awake() {
        GameEngine = new GameEngine();
    }

    void Start() {
        for (int i = 0; i < GameEngine.GameState.Players.Count; i++) {
            Player p = GameEngine.GameState.Players[i];
            DrawPlayerEstate(p, i);
        }
    }

    private void DrawPlayerEstate(Player player, int index) {
        switch (index) {
            case 0:
                DrawPlayerName(ED.Player1Name, player.Name);
                DrawPoints(ED.Player1Points, player.Score + "");
                DrawPlayerCards(player, ED.Player1Name.y);
                break;
            case 1:
                DrawPlayerName(ED.Player2Name, player.Name);
                DrawPoints(ED.Player2Points, player.Score + "");
                DrawPlayerCards(player, ED.Player2Name.y);
                break;
            case 2:
                DrawPlayerName(ED.Player3Name, player.Name);
                DrawPoints(ED.Player3Points, player.Score + "");
                DrawPlayerCards(player, ED.Player3Name.y);
                break;
            case 3:
                DrawPlayerName(ED.Player4Name, player.Name);
                DrawPoints(ED.Player4Points, player.Score + "");
                DrawPlayerCards(player, ED.Player4Name.y);
                break;
        }
    }

    private static void DrawPlayerName(Vector2 position, string text) {
        Object obj = Resources.Load("Prefabs/TextPlayerName");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("EstateCanvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        TMPro.TextMeshProUGUI textObj = prefab.GetComponent<TMPro.TextMeshProUGUI>();
        textObj.text = "" + text;
    }

    private static void DrawPoints(Vector2 position, string points) {
        Object obj = Resources.Load("Prefabs/TextPoints");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("EstateCanvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        TMPro.TextMeshProUGUI textObj = prefab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textObj.text = "" + points;
    }

    private static void DrawPlayerCards(Player player, float axisY) {
        float margin = ED.CardsSpaceStart;

        for (int i = 0; i < player.CompletedProjects.Count; i++) {
            Card c = player.CompletedProjects[i];

            if (i > 0 && i % 3 == 0) {
                margin += GD.CardWidth * 0.4f + GD.MarginSmall * 2;
            }

            margin += GD.CardWidth * 0.6f;
            Vector2 position = new Vector2(margin, axisY);
            DrawCard(position, c);
        }


    }

    private static void DrawCard(Vector2 position, Card card) {
        Object obj = Resources.Load("Prefabs/Card");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("EstateCanvas");
        prefab.transform.SetParent(canvas.transform);

        Image image = prefab.GetComponent<Image>();
        image.overrideSprite = CardsGenerator.GetSpriteForCard(card);

        prefab.transform.position = new Vector3(position.x, position.y, 0);
    }

}