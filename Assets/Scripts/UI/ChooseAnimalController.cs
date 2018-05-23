﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GD = GameDimensions;

public class ChooseAnimalController : MonoBehaviour {

    private bool clickable = true;
    private GameState GameState;
    private Vector2 posAvailableCards;
    private Vector2 posYourCards;
    private GameObject dragCardText;
    public int HowManyCards;
    public System.Action DoneCallback;
    private List<GameObject> GarbageCollector = new List<GameObject>();

    private void Awake() {
        GameState = new GameEngine().GameState;
    }

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
        if (howManycards == 1) {
            text = "take " + howManycards + " card";
        } else {
            text = "take " + howManycards + " cards";

        }
        GameObject.Find("how_many_text")
                  .GetComponent<TMPro.TextMeshProUGUI>()
                  .text = text;

        DrawPlayerAnimals(GameState.CurrentPlayer, posYourCards);
        DrawAvailableAnimals();
    }

    private void DrawAvailableAnimals() {
        float margin = posAvailableCards.x;

        if (GameState.AnimalsDeck.Cards.Count > 0) {
            DrawCard(margin, 0);
            margin += GD.CardWidth + GD.MarginBig;
        }

        if (GameState.AnimalsDeck.Cards.Count > 1) {
            DrawCard(margin, 1);
        }

    }

    private void DrawCard(float margin, int index) {
        var animal = GameState.AnimalsDeck.Cards[index];

        string resId = "";
        switch (animal.Class) {
            case CardClass.Pig:
                resId = "pig";
                break;
            case CardClass.Cow:
                resId = "cow";
                break;
            case CardClass.Sheep:
                resId = "lamb";
                break;
            case CardClass.Chicken:
                resId = "hen";
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
                Destroy(gameObject, 0.5f);
                clickable = false;
            } else {
                UpdateView(HowManyCards);
            }
        };
    }

    private void GiveThisCardToPlayer(int index) {
        var card = GameState.AnimalsDeck.Cards[index];
        GameState.AnimalsDeck.Cards.RemoveAt(index);
        GameState.CurrentPlayer.Animals.Add(card);
        GameState.SaveGameState();
    }

    private void DrawPlayerAnimals(Player player, Vector2 startingPosition) {
        float margin = startingPosition.x;

        int numberOfCows = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Cow).Count;
        int numberOfChickens = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Chicken).Count;
        int numberOfPigs = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Pig).Count;
        int numberOfSheep = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Sheep).Count;

        if (numberOfCows > 0) {
            Vector2 position = new Vector2(margin, startingPosition.y);
            DrawAnimalCard(position, "cow", numberOfCows);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOfChickens > 0) {
            Vector2 position = new Vector2(margin, startingPosition.y);
            DrawAnimalCard(position, "hen", numberOfChickens);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOfPigs > 0) {
            Vector2 position = new Vector2(margin, startingPosition.y);
            DrawAnimalCard(position, "pig", numberOfPigs);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOfSheep > 0) {
            Vector2 position = new Vector2(margin, startingPosition.y);
            DrawAnimalCard(position, "lamb", numberOfSheep);
            margin += GD.CardWidth + GD.MarginSmall;
        }

    }

    private GameObject DrawAnimalCard(Vector2 position, string resId, int howMany) {
        GameObject card = CardsGenerator.CreateCardGameObject(resId, position, parent: gameObject);
        card.AddSmallText(howMany + "");
        GarbageCollector.Add(card);
        return card;
    }

}