﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using GD = GameDimensions;
using ED = EstatesDimensions;
using GSP = GameStateProvider;

public class BonusesTakenController : MonoBehaviour {

    void Start() {
        for (int i = 0; i < GSP.GameState.Players.Count; i++) {
            Player p = GSP.GameState.Players[i];
            DrawPlayerBonuses(p, i);
        }
    }

    private void DrawPlayerBonuses(Player player, int index) {
        switch (index) {
            case 0:
                DrawPlayerName(ED.Player1Name, player.NickName);
                DrawPoints(ED.Player1Points, player.Score + "");
                DrawPlayerBonuses(player, ED.Player1Name.y);
                break;
            case 1:
                DrawPlayerName(ED.Player2Name, player.NickName);
                DrawPoints(ED.Player2Points, player.Score + "");
                DrawPlayerBonuses(player, ED.Player2Name.y);
                break;
            case 2:
                DrawPlayerName(ED.Player3Name, player.NickName);
                DrawPoints(ED.Player3Points, player.Score + "");
                DrawPlayerBonuses(player, ED.Player3Name.y);
                break;
            case 3:
                DrawPlayerName(ED.Player4Name, player.NickName);
                DrawPoints(ED.Player4Points, player.Score + "");
                DrawPlayerBonuses(player, ED.Player4Name.y);
                break;
        }
    }

    private static void DrawPlayerName(Vector2 position, string text) {
        Object obj = Resources.Load("Prefabs/TextPlayerName");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        TMPro.TextMeshProUGUI textObj = prefab.GetComponent<TMPro.TextMeshProUGUI>();
        textObj.text = "" + text;
    }

    private static void DrawPoints(Vector2 position, string points) {
        GameObject pointsCard = CardsGenerator.CreateCardGameObject("small_card_empty", position, false, true);
        pointsCard.AddSmallText(points, false, true);
    }

    private static void DrawPlayerBonuses(Player player, float axisY) {
        float margin = ED.CardsSpaceStart;

        for (int i = 0; i < player.ReceivedBonuses.Count; i++) {
            BonusCard c = player.ReceivedBonuses[i];

            Vector2 position = new Vector2(margin, axisY);
            DrawCard(position, c);
            margin += GD.CardWidth + GD.MarginSmall;
        }
    }

    private static void DrawCard(Vector2 position, BonusCard card) {
        Object obj = Resources.Load("Prefabs/Card");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        Image image = prefab.GetComponent<Image>();
        image.overrideSprite = CardsGenerator.GetSpriteForBonus(card);

        prefab.transform.position = new Vector3(position.x, position.y, 0);
    }
}
