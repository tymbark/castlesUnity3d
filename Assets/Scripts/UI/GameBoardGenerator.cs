using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.UI;
using D = GameDimensions;

public class GameBoardGenerator {

    private List<GameObject> gameObjects = new List<GameObject>();

    public List<GameObject> DrawGameBoard(GameEngine engine) {
        DrawPlayerHand(engine.GameState.CurrentPlayer);
        DrawEnviroment(engine.GameState);
        DrawPlayerProjectCards(engine.GameState.CurrentPlayer.ProjectArea);
        DrawAnimals(engine.GameState.AnimalsDeck);
        DrawGoods(engine.GameState.GoodsDeck);
        DrawDices();
        DrawAvailableProjectCards(engine.GameState.AvailableProjectCards);

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
            gameObjects.Add(CardsGenerator.DrawExecutableCard(cards[2], D.PositionProjectCard3));
        }

    }

    private void DrawPlayerHand(Player player) {

        float axisY = D.PositionCardProjectsBigCard.y + D.CardHeight * 1.2f;
        float startAxisX = D.PositionCardProjectsBigCard.x - D.CardHeight;

        var cards = player.Cards;
        var bonusCards = player.BonusActionCards;

        Vector2 positionHandCard1 = new Vector2(startAxisX + D.CardHeight / 2, axisY);
        Vector2 positionHandCard2 = positionHandCard1 + new Vector2(D.CardHeight, 0);
        Vector2 positionBonusCard1;
        Vector2 positionBonusCard2;
        Vector2 positionBonusCard3;
        Vector2 positionBonusCard4;

        switch (bonusCards.Count) {
            case 1:
                positionHandCard1 = new Vector2(startAxisX + D.CardWidth / 2, axisY);
                positionHandCard2 = positionHandCard1 + new Vector2(D.CardWidth + D.MarginSmall, 0);

                positionBonusCard1 = positionHandCard2 + new Vector2(D.CardWidth + D.MarginSmall, 0);

                gameObjects.Add(CardsGenerator.DrawHandCard(bonusCards[0], positionBonusCard1, -5));
                break;
            case 2:
                positionHandCard1 = new Vector2(startAxisX + D.CardWidth / 2.4f, axisY);
                positionHandCard2 = positionHandCard1 + new Vector2(D.CardWidth * 0.6f, 0);

                positionBonusCard1 = positionHandCard2 + new Vector2(D.CardWidth + D.MarginSmall, 0);
                positionBonusCard2 = positionBonusCard1 + new Vector2(D.CardWidth * 0.6f, 0);

                gameObjects.Add(CardsGenerator.DrawHandCard(bonusCards[0], positionBonusCard1, -6));
                gameObjects.Add(CardsGenerator.DrawHandCard(bonusCards[1], positionBonusCard2, -5));
                break;
            case 3:
                positionHandCard1 = new Vector2(startAxisX, axisY);
                positionHandCard2 = positionHandCard1 + new Vector2(D.CardWidth * 0.6f, 0);

                positionBonusCard1 = positionHandCard2 + new Vector2(D.CardWidth + D.MarginSmall, 0);
                positionBonusCard2 = positionBonusCard1 + new Vector2(D.CardWidth * 0.6f, 0);
                positionBonusCard3 = positionBonusCard2 + new Vector2(D.CardWidth * 0.6f, 0);

                gameObjects.Add(CardsGenerator.DrawHandCard(bonusCards[0], positionBonusCard1, -7));
                gameObjects.Add(CardsGenerator.DrawHandCard(bonusCards[1], positionBonusCard2, -6));
                gameObjects.Add(CardsGenerator.DrawHandCard(bonusCards[2], positionBonusCard3, -5));
                break;
            case 4:
                positionHandCard1 = new Vector2(startAxisX, axisY);
                positionHandCard2 = positionHandCard1 + new Vector2(D.CardWidth * 0.6f, 0);

                positionBonusCard1 = positionHandCard2 + new Vector2(D.CardWidth + D.MarginSmall, 0);
                positionBonusCard2 = positionBonusCard1 + new Vector2(D.CardWidth * 0.6f, 0);
                positionBonusCard3 = positionBonusCard2 + new Vector2(D.CardWidth * 0.6f, 0);
                positionBonusCard4 = positionBonusCard3 + new Vector2(D.CardWidth * 0.6f, 0);

                gameObjects.Add(CardsGenerator.DrawHandCard(bonusCards[0], positionBonusCard1, -8));
                gameObjects.Add(CardsGenerator.DrawHandCard(bonusCards[1], positionBonusCard2, -7));
                gameObjects.Add(CardsGenerator.DrawHandCard(bonusCards[2], positionBonusCard3, -6));
                gameObjects.Add(CardsGenerator.DrawHandCard(bonusCards[3], positionBonusCard4, -5));
                break;
        }

        if (cards.Count > 0) {
            gameObjects.Add(CardsGenerator.DrawHandCard(cards[0], positionHandCard1, -4));
        }

        if (cards.Count > 1) {
            gameObjects.Add(CardsGenerator.DrawHandCard(cards[1], positionHandCard2, -3));
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
        Player currentPlayer = gameState.CurrentPlayer;

        gameObjects.Add(CardsGenerator.DrawEstateCard(currentPlayer.CompletedProjects.Count));
        gameObjects.Add(CardsGenerator.DrawProjectsCard(currentPlayer.ProjectArea.Count));
        gameObjects.Add(CardsGenerator.DrawStorageCard(currentPlayer.Goods.Count));
        gameObjects.Add(CardsGenerator.DrawAnimalsCard(currentPlayer.Animals.Count));

        gameObjects.Add(CardsGenerator.DrawClickableAndExecutableCard(StaticCards.DummySellSilverAndWorkers, D.PositionSellSilverAndWorkersCard));
        gameObjects.Add(CardsGenerator.DrawClickableAndExecutableCard(StaticCards.DummyShipGoods, D.PositionShipGoodsCard));
        gameObjects.Add(CardsGenerator.DrawSilverCard(currentPlayer.SilverCount));
        gameObjects.Add(CardsGenerator.DrawWorkersCard(currentPlayer.WorkersCount));

        gameObjects.Add(CardsGenerator.DrawPointsButton(currentPlayer.Score));
        gameObjects.Add(CardsGenerator.DrawClickableButtonCard(StaticCards.DummyEndTurn, D.PositionEndTurnButton));
        gameObjects.Add(CardsGenerator.DrawClickableButtonCard(StaticCards.DummyExit, D.PositionExitButton));

        gameObjects.Add(CardsGenerator.DrawBigBackgroundCard(StaticCards.DummyAllProjects, D.PositionCardProjectsBigCard));

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
        gameObjects.Add(CardsGenerator.DrawStaticCard(StaticCards.DummyDiceI, D.PositionProjectDiceI));
        gameObjects.Add(CardsGenerator.DrawStaticCard(StaticCards.DummyDiceII, D.PositionProjectDiceII));
        gameObjects.Add(CardsGenerator.DrawStaticCard(StaticCards.DummyDiceIII, D.PositionProjectDiceIII));
        gameObjects.Add(CardsGenerator.DrawStaticCard(StaticCards.DummyDiceIV, D.PositionProjectDiceIV));
        gameObjects.Add(CardsGenerator.DrawStaticCard(StaticCards.DummyDiceV, D.PositionProjectDiceV));
        gameObjects.Add(CardsGenerator.DrawStaticCard(StaticCards.DummyDiceVI, D.PositionProjectDiceVI));
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
