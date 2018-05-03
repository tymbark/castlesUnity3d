using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.UI;
using D = GameDimensions;

public class GameBoardGenerator {

    private List<GameObject> gameObjects = new List<GameObject>();

    public List<GameObject> DrawGameBoard(GameEngine engine) {
        DrawPlayerHand(engine.GameState.CurrentPlayer.Cards);
        DrawEnviroment(engine.GameState);
        DrawPlayerProjectCards(engine.GameState.CurrentPlayer.ProjectArea);
        DrawAnimals(engine.AnimalsDeck);
        DrawGoods(engine.GoodsDeck);
        DrawDices();
        DrawAvailableProjectCards(engine.AvailableProjectCards);

        return gameObjects;
    }

    private void DrawPlayerProjectCards(List<Card> cards) {

        if (cards.Count > 0) {
            gameObjects.Add(CardsGenerator.DrawExecutableCard(cards[0], D.PositionProjectCard1));
        }
        if (cards.Count > 1) {
            gameObjects.Add(CardsGenerator.DrawExecutableCard(cards[1], D.PositionProjectCard2));
        }
        if (cards.Count > 2) {
            gameObjects.Add(CardsGenerator.DrawExecutableCard(cards[1], D.PositionProjectCard3));
        }

    }

    private void DrawPlayerHand(List<Card> cards) {

        if (cards.Count > 0) {
            gameObjects.Add(CardsGenerator.DrawHandCard(cards[0], D.PositionHandCard1));
        }

        if (cards.Count > 1) {
            gameObjects.Add(CardsGenerator.DrawHandCard(cards[1], D.PositionHandCard2));
        }

    }

    private void DrawAnimals(Deck animalsDeck) {
        if (animalsDeck.Cards.Count > 0) {
            gameObjects.Add(CardsGenerator.DrawStaticCard(animalsDeck.Cards[0], D.PositionAnimalToTakeCard1));
        }
        if (animalsDeck.Cards.Count > 1) {
            gameObjects.Add(CardsGenerator.DrawStaticCard(animalsDeck.Cards[1], D.PositionAnimalToTakeCard2));
        }
    }

    private void DrawGoods(Deck goodsDeck) {
        if (goodsDeck.Cards.Count > 0) {
            gameObjects.Add(CardsGenerator.DrawStaticCard(goodsDeck.Cards[0], D.PositionGoodsToTakeCard1));
        }
        if (goodsDeck.Cards.Count > 1) {
            gameObjects.Add(CardsGenerator.DrawStaticCard(goodsDeck.Cards[1], D.PositionGoodsToTakeCard2));
        }
    }

    private void DrawEnviroment(GameState gameState) {
        gameObjects.Add(CardsGenerator.DrawClickableHorizontalCard(Card.DummyAllEstates, D.PositionAllEstatesCard));
        gameObjects.Add(CardsGenerator.DrawClickableHorizontalCard(Card.DummyAllProjects, D.PositionAllProjectsCard));
        gameObjects.Add(CardsGenerator.DrawClickableHorizontalCard(Card.DummyAllStorages, D.PositionAllStoragesCard));
        gameObjects.Add(CardsGenerator.DrawClickableCard(Card.DummyAllAnimals, D.PositionAllAnimalsCard));
        gameObjects.Add(CardsGenerator.DrawClickableCard(Card.DummySellSilverAndWorkers, D.PositionSellSilverAndWorkersCard));
        gameObjects.Add(CardsGenerator.DrawClickableCard(Card.DummyShipGoods, D.PositionShipGoodsCard));
        gameObjects.Add(CardsGenerator.DrawClickableAndExecutableCard(Card.DummyWorker, D.PositionSilverCard));
        gameObjects.Add(CardsGenerator.DrawClickableAndExecutableCard(Card.DummySilver, D.PositionWorkerCard));

        gameObjects.Add(CardsGenerator.DrawClickablePointsButton(Card.DummyPoints, D.PositionPointsButton, gameState.CurrentPlayer.Score));
        gameObjects.Add(CardsGenerator.DrawClickableButtonCard(Card.DummyEndTurn, D.PositionEndTurnButton));
        gameObjects.Add(CardsGenerator.DrawClickableButtonCard(Card.DummyExit, D.PositionExitButton));

        gameObjects.Add(CardsGenerator.DrawBigBackgroundCard(Card.DummyAllProjects, D.PositionCardProjectsBigCard));

        switch (gameState.CurrentRound) {
            case Round.A:
                gameObjects.Add(CardsGenerator.DrawClickableCard(new Card(CardClass.BonusA, CardDice.O), D.PositionCurrentBonusCard));
                break;
            case Round.B:
                gameObjects.Add(CardsGenerator.DrawClickableCard(new Card(CardClass.BonusB, CardDice.O), D.PositionCurrentBonusCard));
                break;
            case Round.C:
                gameObjects.Add(CardsGenerator.DrawClickableCard(new Card(CardClass.BonusC, CardDice.O), D.PositionCurrentBonusCard));
                break;
            case Round.D:
                gameObjects.Add(CardsGenerator.DrawClickableCard(new Card(CardClass.BonusD, CardDice.O), D.PositionCurrentBonusCard));
                break;
            case Round.E:
                gameObjects.Add(CardsGenerator.DrawClickableCard(new Card(CardClass.BonusE, CardDice.O), D.PositionCurrentBonusCard));
                break;
        }

    }

    private void DrawDices() {
        gameObjects.Add(CardsGenerator.DrawStaticCard(Card.DummyDiceI, D.PositionProjectDiceI));
        gameObjects.Add(CardsGenerator.DrawStaticCard(Card.DummyDiceII, D.PositionProjectDiceII));
        gameObjects.Add(CardsGenerator.DrawStaticCard(Card.DummyDiceIII, D.PositionProjectDiceIII));
        gameObjects.Add(CardsGenerator.DrawStaticCard(Card.DummyDiceIV, D.PositionProjectDiceIV));
        gameObjects.Add(CardsGenerator.DrawStaticCard(Card.DummyDiceV, D.PositionProjectDiceV));
        gameObjects.Add(CardsGenerator.DrawStaticCard(Card.DummyDiceVI, D.PositionProjectDiceVI));
    }

    private void DrawAvailableProjectCards(List<ProjectCard> cards) {

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
                    gameObjects.Add(CardsGenerator.DrawExecutableCard(pc.Card, position));
                    countI++;
                    break;
                case CardDice.II:
                    position = D.PositionForActionCard(D.PositionProjectDiceII, countII, howManyII);
                    gameObjects.Add(CardsGenerator.DrawExecutableCard(pc.Card, position));
                    countII++;
                    break;
                case CardDice.III:
                    position = D.PositionForActionCard(D.PositionProjectDiceIII, countIII, howManyIII);
                    gameObjects.Add(CardsGenerator.DrawExecutableCard(pc.Card, position));
                    countIII++;
                    break;
                case CardDice.IV:
                    position = D.PositionForActionCard(D.PositionProjectDiceIV, countIV, howManyIV);
                    gameObjects.Add(CardsGenerator.DrawExecutableCard(pc.Card, position));
                    countIV++;
                    break;
                case CardDice.V:
                    position = D.PositionForActionCard(D.PositionProjectDiceV, countV, howManyV);
                    gameObjects.Add(CardsGenerator.DrawExecutableCard(pc.Card, position));
                    countV++;
                    break;
                case CardDice.VI:
                    position = D.PositionForActionCard(D.PositionProjectDiceVI, countVI, howManyVI);
                    gameObjects.Add(CardsGenerator.DrawExecutableCard(pc.Card, position));
                    countVI++;
                    break;
                default:
                    throw new System.InvalidProgramException("Project card with invalid Dice: " + pc.TakeProjectDice);
            }
        }

    }
}
