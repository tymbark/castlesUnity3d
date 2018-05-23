using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GD = GameDimensions;

public class ChooseAnimalController : MonoBehaviour {

    private GameState GameState;
    private Vector2 posAvailableCards;
    private Vector2 posYourCards;
    private GameObject dragCardText;
    public int HowManyCards;
    public List<Card> AvailableCards;
    public System.Action DoneCallback;

    private void Awake() {
        GameState = new GameEngine().GameState;
    }

    public void Setup(List<Card> availableCards) {
        AvailableCards = availableCards;

        posYourCards = GameObject.Find("your_cards_position").transform.position;
        posAvailableCards = GameObject.Find("available_cards_position").transform.position;
        dragCardText = GameObject.Find("drag_card_text");

        DrawPlayerAnimals(GameState.CurrentPlayer, posYourCards);
        DrawAvailableAnimals();
    }

    private void DrawAvailableAnimals() {
        float margin = posAvailableCards.x;

        foreach (Card animal in AvailableCards) {
            string resId = "";
            switch (animal.Class) {
                case CardClass.Pig:
                    resId = "pig";
                    break;
                case CardClass.Cow:
                    resId = "cow";
                    break;
                case CardClass.Sheep:
                    resId = "sheep";
                    break;
                case CardClass.Chicken:
                    resId = "hen";
                    break;
            }
            CardsGenerator.CreateCardGameObject(resId, new Vector2(margin, posAvailableCards.y), parent: gameObject);

            margin += GD.CardWidth + GD.MarginBig;
        }
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
        return card;
    }

}
