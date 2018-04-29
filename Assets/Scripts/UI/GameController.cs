using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.UI;
using D = GameDimensions;

public class GameController : MonoBehaviour {

    // Use this for initialization
    void Start() {
        print(D.PositionHandCard1);
        print(D.PositionHandCard2);
        GameObject go = GameObject.Find("Input Handler");
        InputHandler handler = go.GetComponent<InputHandler>();

        GameEngine engine = handler.GameEngine;

        //DrawPlayerHand(engine);
        DrawEnviroment();
        DrawAnimals(engine.AnimalsDeck);
        DrawGoods(engine.GoodsDeck);
        DrawDices();
        DrawAvailableProjectCards(engine.AvailableProjectCards);

    }
    // Update is called once per frame
    void Update() {

    }

    static void DrawAnimals(Deck animalsDeck) {
        if (animalsDeck.Cards.Count > 0) {
            UICard.DrawCard(animalsDeck.Cards[0], D.PositionAnimalToTakeCard1);
        }
        if (animalsDeck.Cards.Count > 1) {
            UICard.DrawCard(animalsDeck.Cards[1], D.PositionAnimalToTakeCard2);
        }
    }

    static void DrawGoods(Deck goodsDeck) {
        if (goodsDeck.Cards.Count > 0) {
            UICard.DrawCard(goodsDeck.Cards[0], D.PositionGoodsToTakeCard1);
        }
        if (goodsDeck.Cards.Count > 1) {
            UICard.DrawCard(goodsDeck.Cards[1], D.PositionGoodsToTakeCard2);
        }
    }

    static void DrawEnviroment() {
        UICard.DrawCard(Card.DummyAllEstates, D.PositionAllEstatesCard, true);
        UICard.DrawCard(Card.DummyAllProjects, D.PositionAllProjectsCard, true);
        UICard.DrawCard(Card.DummyAllStorages, D.PositionAllStoragesCard, true);
    }

    static void DrawDices() {
        UICard.DrawCard(Card.DummyDiceI, D.PositionProjectDiceI);
        UICard.DrawCard(Card.DummyDiceII, D.PositionProjectDiceII);
        UICard.DrawCard(Card.DummyDiceIII, D.PositionProjectDiceIII);
        UICard.DrawCard(Card.DummyDiceIV, D.PositionProjectDiceIV);
        UICard.DrawCard(Card.DummyDiceV, D.PositionProjectDiceV);
        UICard.DrawCard(Card.DummyDiceVI, D.PositionProjectDiceVI);
    }

    static void DrawAvailableProjectCards(List<ProjectCard> cards) {

        int howManyI = cards.FindAll((obj) => obj.TakeProjectDice == CardDice.I).Count;
        int howManyII = cards.FindAll((obj) => obj.TakeProjectDice == CardDice.II).Count;
        int howManyIII = cards.FindAll((obj) => obj.TakeProjectDice == CardDice.III).Count;
        int howManyIV = cards.FindAll((obj) => obj.TakeProjectDice == CardDice.IV).Count;
        int howManyV = cards.FindAll((obj) => obj.TakeProjectDice == CardDice.V).Count;
        int howManyVI = cards.FindAll((obj) => obj.TakeProjectDice == CardDice.VI).Count;

        int countI = 0;
        int countII = 0;
        int countIII = 0;
        int countIV = 0;
        int countV = 0;
        int countVI = 0;

        foreach (ProjectCard pc in cards) {
            switch (pc.TakeProjectDice) {
                case CardDice.I:
                    UICard.DrawCard(pc.Card, D.PositionForActionCard(D.PositionProjectDiceI, countI, howManyI));
                    countI++;
                    break;
                case CardDice.II:
                    UICard.DrawCard(pc.Card, D.PositionForActionCard(D.PositionProjectDiceII, countII, howManyII));
                    countII++;
                    break;
                case CardDice.III:
                    UICard.DrawCard(pc.Card, D.PositionForActionCard(D.PositionProjectDiceIII, countIII, howManyIII));
                    countIII++;
                    break;
                case CardDice.IV:
                    UICard.DrawCard(pc.Card, D.PositionForActionCard(D.PositionProjectDiceIV, countIV, howManyIV));
                    countIV++;
                    break;
                case CardDice.V:
                    UICard.DrawCard(pc.Card, D.PositionForActionCard(D.PositionProjectDiceV, countV, howManyV));
                    countV++;
                    break;
                case CardDice.VI:
                    UICard.DrawCard(pc.Card, D.PositionForActionCard(D.PositionProjectDiceVI, countVI, howManyVI));
                    countVI++;
                    break;
                default:
                    throw new System.InvalidProgramException("Project card with invalid Dice: " + pc.TakeProjectDice);
            }
        }

    }

    static void DrawPlayerHand(GameEngine engine) {

        Player p = engine.GameState.CurrentPlayer;

        if (p.Cards.Count > 0) {
            UICard.DrawCard(p.Cards[0], D.PositionHandCard1);
        }

        if (p.Cards.Count > 1) {
            UICard.DrawCard(p.Cards[1], D.PositionHandCard2);
        }

    }

}
