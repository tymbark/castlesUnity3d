﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GD = GameDimensions;
using GSP = GameStateProvider;

public class ChooseGoodsController : MonoBehaviour {

    private bool clickable = true;
    private Vector2 posAvailableCards;
    private Vector2 posYourCards;
    private GameObject dragCardText;
    public int HowManyCards;
    public System.Action DoneCallback;
    private List<GameObject> GarbageCollector = new List<GameObject>();

    public void UpdateView(int howManycards) {
        foreach (GameObject obj in GarbageCollector) {
            Destroy(obj);
        }
        GarbageCollector.Clear();

        HowManyCards = howManycards;
        posYourCards = new Vector2(-200, -350);
        posAvailableCards = new Vector2(-200, 200);

        dragCardText = GameObject.Find("drag_card_text");

        string text;
        switch (howManycards) {
            case 0:
                text = "";
                break;
            case 1:
                text = "take " + howManycards + " card";
                break;
            default:
                text = "take " + howManycards + " cards";
                break;
        }

        GameObject.Find("how_many_text")
                  .GetComponent<TMPro.TextMeshProUGUI>()
                  .text = text;

        DrawPlayerGoods(GSP.GameState.CurrentPlayer, posYourCards);
        DrawAvailableGoods();
    }

    private void DrawAvailableGoods() {
        float margin = posAvailableCards.x;

        if (GSP.GameState.GoodsDeck.Cards.Count > 0) {
            DrawCard(margin, 0);
            margin += GD.CardWidth + GD.MarginBig;
        }

        if (GSP.GameState.GoodsDeck.Cards.Count > 1) {
            DrawCard(margin, 1);
        }

    }

    private void DrawCard(float margin, int index) {
        var goods = GSP.GameState.GoodsDeck.Cards[index];

        string resId = "";
        switch (goods.Dice) {
            case CardDice.I_II:
                resId = "goods1-2";
                break;
            case CardDice.III_IV:
                resId = "goods3-4";
                break;
            case CardDice.V_VI:
                resId = "goods5-6";
                break;
        }
        var card = CardsGenerator.CreateCardGameObject(resId, new Vector2(margin, posAvailableCards.y), parent: gameObject);
        GarbageCollector.Add(card);
        var clickComponent = card.AddComponent<ClickActionScript>();
        clickComponent.ClickMethod = (x) => {
            if (!clickable) return;

            GiveThisCardToPlayer(index);
            HowManyCards = HowManyCards - 1;
            if (HowManyCards == 0) {
                UpdateView(HowManyCards);
                clickable = false;
                Invoke("Done", 1f);
            } else {
                UpdateView(HowManyCards);
            }
        };
    }

    private void Done() {
        Destroy(gameObject);
        DoneCallback();
    }

    private void GiveThisCardToPlayer(int index) {
        var card = GSP.GameState.GoodsDeck.Cards[index];
        GSP.GameState.GoodsDeck.Cards.RemoveAt(index);
        GSP.GameState.CurrentPlayer.Goods.Add(card);
    }

    private void DrawPlayerGoods(Player player, Vector2 startingPosition) {
        float margin = startingPosition.x;

        int numberOf_I_II = player.Goods.FindAll((Card obj) => obj.Dice == CardDice.I_II).Count;
        int numberOf_III_IV = player.Goods.FindAll((Card obj) => obj.Dice == CardDice.III_IV).Count;
        int numberOf_V_VI = player.Goods.FindAll((Card obj) => obj.Dice == CardDice.V_VI).Count;

        if (numberOf_I_II > 0) {
            Vector2 position = new Vector2(margin, startingPosition.y);
            DrawGoodsCard(position, "goods1-2", numberOf_I_II);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOf_III_IV > 0) {
            Vector2 position = new Vector2(margin, startingPosition.y);
            DrawGoodsCard(position, "goods3-4", numberOf_III_IV);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOf_V_VI > 0) {
            Vector2 position = new Vector2(margin, startingPosition.y);
            DrawGoodsCard(position, "goods5-6", numberOf_V_VI);
            margin += GD.CardWidth + GD.MarginSmall;
        }


    }

    private GameObject DrawGoodsCard(Vector2 position, string resId, int howMany) {
        GameObject card = CardsGenerator.CreateCardGameObject(resId, position, parent: gameObject);
        card.AddSmallText(howMany + "");
        GarbageCollector.Add(card);
        return card;
    }

}
