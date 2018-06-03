using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GD = GameDimensions;
using GSP = GameStateProvider;

public class ChooseBothController : MonoBehaviour {

    private bool clickable = true;
    private GameObject AvailableCardsObj;
    private GameObject YourCardsObj;
    public System.Action DoneCallback;
    private List<GameObject> GarbageCollector = new List<GameObject>();

    public void UpdateView() {
        foreach (GameObject obj in GarbageCollector) {
            Destroy(obj);
        }
        GarbageCollector.Clear();

        AvailableCardsObj = GameObject.Find("available_cards_position");
        YourCardsObj = GameObject.Find("your_cards_position");

        GameObject.Find("how_many_text")
                          .GetComponent<TMPro.TextMeshProUGUI>()
                          .text = "take one card";

        DrawPlayerCards();
        DrawAvailableCards();

    }

    private void DrawAvailableCards() {
        float margin = 0;

        if (GSP.GameState.GoodsDeck.Cards.Count > 0) {
            DrawAvailableCard(new Vector2(margin, 0), GSP.GameState.GoodsDeck.Cards[0])
                .AddComponent<ClickActionScript>()
                .ClickMethod = (x) => {
                    TakeThisCard(GSP.GameState.GoodsDeck.Cards[0], 0);
                };
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (GSP.GameState.GoodsDeck.Cards.Count > 1) {
            DrawAvailableCard(new Vector2(margin, 0), GSP.GameState.GoodsDeck.Cards[1])
                .AddComponent<ClickActionScript>()
                .ClickMethod = (x) => {
                    TakeThisCard(GSP.GameState.GoodsDeck.Cards[1], 1);
                };
            margin += GD.CardWidth + GD.MarginSmall;
        }

        margin += GD.CardWidth;

        if (GSP.GameState.AnimalsDeck.Cards.Count > 0) {
            DrawAvailableCard(new Vector2(margin, 0), GSP.GameState.AnimalsDeck.Cards[0])
                .AddComponent<ClickActionScript>()
                .ClickMethod = (x) => {
                    TakeThisCard(GSP.GameState.AnimalsDeck.Cards[0], 0);
                };
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (GSP.GameState.AnimalsDeck.Cards.Count > 1) {
            DrawAvailableCard(new Vector2(margin, 0), GSP.GameState.AnimalsDeck.Cards[1])
                .AddComponent<ClickActionScript>()
                .ClickMethod = (x) => {
                    TakeThisCard(GSP.GameState.AnimalsDeck.Cards[1], 1);
                };
            margin += GD.CardWidth + GD.MarginSmall;
        }

    }

    private void TakeThisCard(Card card, int index) {
        if (!clickable) return;
        clickable = false;

        if (card.IsAnimalType()) {
            GSP.GameState.CurrentPlayer.Animals.Add(card);
            GSP.GameState.AnimalsDeck.Cards.RemoveAt(index);
        } else {
            GSP.GameState.CurrentPlayer.Goods.Add(card);
            GSP.GameState.GoodsDeck.Cards.RemoveAt(index);
        }
        UpdateView();

        GameObject.Find("how_many_text")
                          .GetComponent<TMPro.TextMeshProUGUI>()
                          .text = "";

        Invoke("Destroy", 1f);
    }

    private void Destroy() {
        DoneCallback();
        Destroy(gameObject);
    }

    private void DrawPlayerCards() {
        var player = GSP.GameState.CurrentPlayer;
        float margin = 0;

        int numberOfCows = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Cow).Count;
        int numberOfChickens = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Chicken).Count;
        int numberOfPigs = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Pig).Count;
        int numberOfSheep = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Sheep).Count;

        if (numberOfCows > 0) {
            Vector2 position = new Vector2(margin, 0);
            DrawPlayerCard(position, "cow", numberOfCows);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOfChickens > 0) {
            Vector2 position = new Vector2(margin, 0);
            DrawPlayerCard(position, "hen", numberOfChickens);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOfPigs > 0) {
            Vector2 position = new Vector2(margin, 0);
            DrawPlayerCard(position, "pig", numberOfPigs);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOfSheep > 0) {
            Vector2 position = new Vector2(margin, 0);
            DrawPlayerCard(position, "lamb", numberOfSheep);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        margin += GD.CardWidth;

        int numberOf_I_II = player.Goods.FindAll((Card obj) => obj.Dice == CardDice.I_II).Count;
        int numberOf_III_IV = player.Goods.FindAll((Card obj) => obj.Dice == CardDice.III_IV).Count;
        int numberOf_V_VI = player.Goods.FindAll((Card obj) => obj.Dice == CardDice.V_VI).Count;

        if (numberOf_I_II > 0) {
            Vector2 position = new Vector2(margin, 0);
            DrawPlayerCard(position, "goods1-2", numberOf_I_II);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOf_III_IV > 0) {
            Vector2 position = new Vector2(margin, 0);
            DrawPlayerCard(position, "goods3-4", numberOf_III_IV);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOf_V_VI > 0) {
            Vector2 position = new Vector2(margin, 0);
            DrawPlayerCard(position, "goods5-6", numberOf_V_VI);
            margin += GD.CardWidth + GD.MarginSmall;
        }

    }

    private GameObject DrawPlayerCard(Vector2 position, string resId, int howMany) {
        GameObject card = CardsGenerator.CreateCardGameObject(resId, position, parent: YourCardsObj);
        card.AddSmallText(howMany + "");
        GarbageCollector.Add(card);
        return card;
    }

    private GameObject DrawAvailableCard(Vector2 position, Card card) {
        var resId = card.GetResIdForCard();
        GameObject obj = CardsGenerator.CreateCardGameObject(resId, position, parent: AvailableCardsObj);
        GarbageCollector.Add(obj);
        return obj;
    }

}
