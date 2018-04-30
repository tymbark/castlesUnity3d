using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.UI;
using D = GameDimensions;

public class GameBoardGenerator : MonoBehaviour {


    public static void DrawGameBoard(GameEngine engine) {
        DrawPlayerHand(engine.GameState.CurrentPlayer.Cards);
        DrawEnviroment();
        DrawPlayerProjectCards(engine.GameState.CurrentPlayer.ProjectArea);
        DrawAnimals(engine.AnimalsDeck);
        DrawGoods(engine.GoodsDeck);
        DrawDices();
        DrawAvailableProjectCards(engine.AvailableProjectCards);
    }

    static void DrawPlayerProjectCards(List<Card> cards) {

        if (cards.Count > 0) {
            CardsGenerator.DrawExecutableCard(cards[0], D.PositionProjectCard1);
        }
        if (cards.Count > 1) {
            CardsGenerator.DrawExecutableCard(cards[1], D.PositionProjectCard2);
        }
        if (cards.Count > 2) {
            CardsGenerator.DrawExecutableCard(cards[1], D.PositionProjectCard3);
        }

    }

    static void DrawPlayerHand(List<Card> cards) {

        if (cards.Count > 0) {
            CardsGenerator.DrawHandCard(cards[0], D.PositionHandCard1);
        }

        if (cards.Count > 1) {
            CardsGenerator.DrawHandCard(cards[1], D.PositionHandCard2);
        }

    }

    static void DrawAnimals(Deck animalsDeck) {
        if (animalsDeck.Cards.Count > 0) {
            CardsGenerator.DrawStaticCard(animalsDeck.Cards[0], D.PositionAnimalToTakeCard1);
        }
        if (animalsDeck.Cards.Count > 1) {
            CardsGenerator.DrawStaticCard(animalsDeck.Cards[1], D.PositionAnimalToTakeCard2);
        }
    }

    static void DrawGoods(Deck goodsDeck) {
        if (goodsDeck.Cards.Count > 0) {
            CardsGenerator.DrawStaticCard(goodsDeck.Cards[0], D.PositionGoodsToTakeCard1);
        }
        if (goodsDeck.Cards.Count > 1) {
            CardsGenerator.DrawStaticCard(goodsDeck.Cards[1], D.PositionGoodsToTakeCard2);
        }
    }

    static void DrawEnviroment() {
        CardsGenerator.DrawClickableHorizontalCard(Card.DummyAllEstates, D.PositionAllEstatesCard);
        CardsGenerator.DrawClickableHorizontalCard(Card.DummyAllProjects, D.PositionAllProjectsCard);
        CardsGenerator.DrawClickableHorizontalCard(Card.DummyAllStorages, D.PositionAllStoragesCard);
        CardsGenerator.DrawClickableCard(Card.DummyAllAnimals, D.PositionAllAnimalsCard);
        CardsGenerator.DrawClickableCard(Card.DummyAllBonuses, D.PositionAllBonusesCard);
        CardsGenerator.DrawClickableAndExecutableCard(Card.DummyWorker, D.PositionSilverCard);
        CardsGenerator.DrawClickableAndExecutableCard(Card.DummySilver, D.PositionWorkerCard);

        CardsGenerator.DrawClickableButtonCard(Card.DummyOptions, D.PositionOptionsButton);
        CardsGenerator.DrawClickableButtonCard(Card.DummyEndTurn, D.PositionEndTurnButton);
        CardsGenerator.DrawClickableButtonCard(Card.DummyExit, D.PositionExitButton);

        CardsGenerator.DrawBigBackgroundCard(Card.DummyAllProjects, D.PositionCardProjectsBigCard);
    }

    static void DrawDices() {
        CardsGenerator.DrawStaticCard(Card.DummyDiceI, D.PositionProjectDiceI);
        CardsGenerator.DrawStaticCard(Card.DummyDiceII, D.PositionProjectDiceII);
        CardsGenerator.DrawStaticCard(Card.DummyDiceIII, D.PositionProjectDiceIII);
        CardsGenerator.DrawStaticCard(Card.DummyDiceIV, D.PositionProjectDiceIV);
        CardsGenerator.DrawStaticCard(Card.DummyDiceV, D.PositionProjectDiceV);
        CardsGenerator.DrawStaticCard(Card.DummyDiceVI, D.PositionProjectDiceVI);
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
            Vector2 position;
            switch (pc.TakeProjectDice) {
                case CardDice.I:
                    position = D.PositionForActionCard(D.PositionProjectDiceI, countI, howManyI);
                    CardsGenerator.DrawExecutableCard(pc.Card, position);
                    countI++;
                    break;
                case CardDice.II:
                    position = D.PositionForActionCard(D.PositionProjectDiceII, countII, howManyII);
                    CardsGenerator.DrawExecutableCard(pc.Card, position);
                    countII++;
                    break;
                case CardDice.III:
                    position = D.PositionForActionCard(D.PositionProjectDiceIII, countIII, howManyIII);
                    CardsGenerator.DrawExecutableCard(pc.Card, position);
                    countIII++;
                    break;
                case CardDice.IV:
                    position = D.PositionForActionCard(D.PositionProjectDiceIV, countIV, howManyIV);
                    CardsGenerator.DrawExecutableCard(pc.Card, position);
                    countIV++;
                    break;
                case CardDice.V:
                    position = D.PositionForActionCard(D.PositionProjectDiceV, countV, howManyV);
                    CardsGenerator.DrawExecutableCard(pc.Card, position);
                    countV++;
                    break;
                case CardDice.VI:
                    position = D.PositionForActionCard(D.PositionProjectDiceVI, countVI, howManyVI);
                    CardsGenerator.DrawExecutableCard(pc.Card, position);
                    countVI++;
                    break;
                default:
                    throw new System.InvalidProgramException("Project card with invalid Dice: " + pc.TakeProjectDice);
            }
        }

    }
}
