using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GD = GameDimensions;
using GSP = GameStateProvider;

public class ChooseAnimalController : MonoBehaviour {

    private bool clickable = true;
    private Vector2 posAvailableCards;
    private Vector2 posYourCards;
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

        DrawPlayerAnimals(GSP.GameState.CurrentPlayer, posYourCards);
        DrawAvailableAnimals();
    }

    private void DrawAvailableAnimals() {
        float margin = posAvailableCards.x;

        if (GSP.GameState.AnimalsDeck.Cards.Count > 0) {
            DrawCard(margin, 0);
            margin += GD.CardWidth + GD.MarginBig;
        }

        if (GSP.GameState.AnimalsDeck.Cards.Count > 1) {
            DrawCard(margin, 1);
        }

    }

    private void DrawCard(float margin, int index) {
        var animal = GSP.GameState.AnimalsDeck.Cards[index];

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
            print("click");
            if (!clickable) return;
            print("click");

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
        var card = GSP.GameState.AnimalsDeck.Cards[index];
        GSP.GameState.AnimalsDeck.Cards.RemoveAt(index);
        GSP.GameState.CurrentPlayer.Animals.Add(card);
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
