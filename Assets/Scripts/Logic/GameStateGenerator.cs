using Models;
using System;
using System.Collections.Generic;

public static class GameStateGenerator {

    public static GameState GenerateGameState(string newGameId, int howManyPlayers) {
        string newId = newGameId;
        var mainDeck = DeckGenerator.GenerateActionsDeck();
        var animalsDeck = DeckGenerator.GenerateAnimalsDeck();
        var goodsDeck = DeckGenerator.GenerateGoodsDeck();
        var projectCards = PrepareProjectCards(mainDeck, howManyPlayers);
        var players = PreparePlayers(animalsDeck, goodsDeck, howManyPlayers);
        var bonuses = PrepareAvailableBonuses(howManyPlayers);

        return new GameState(newId,
                             players,
                             mainDeck,
                             animalsDeck,
                             goodsDeck,
                             projectCards,
                             bonuses,
                             Round.A,
                             0,
                             1,
                             players.Count,
                             false);
    }

    private static List<BonusCard> PrepareAvailableBonuses(int howManyPlayers) {
        List<BonusCard> bonuses = new List<BonusCard>();

        switch (howManyPlayers) {
            case 4:
                bonuses.Add(BonusCard.AllSeven4);
                bonuses.Add(BonusCard.AllSeven3);
                bonuses.Add(BonusCard.AllSeven2);
                bonuses.Add(BonusCard.AllSeven1);
                break;
            case 3:
                bonuses.Add(BonusCard.AllSeven4);
                bonuses.Add(BonusCard.AllSeven2);
                bonuses.Add(BonusCard.AllSeven1);
                break;
            case 2:
                bonuses.Add(BonusCard.AllSeven3);
                bonuses.Add(BonusCard.AllSeven1);
                break;
        }

        bonuses.Add(BonusCard.Building);
        bonuses.Add(BonusCard.Castle);
        bonuses.Add(BonusCard.Mine);
        bonuses.Add(BonusCard.Cloister);
        bonuses.Add(BonusCard.Knowledge);
        bonuses.Add(BonusCard.Pasture);
        bonuses.Add(BonusCard.Ship);

        return bonuses;
    }

    private static List<Player> PreparePlayers(Deck animalsDeck, Deck goodsDeck, int howManyPlayers) {
        List<Player> Players = new List<Player>();
        for (int i = 0; i < howManyPlayers; i++) {
            int startingWorkers;
            switch (i) {
                case 0:
                    startingWorkers = 0;
                    break;
                case 3:
                    startingWorkers = 2;
                    break;
                default:
                    startingWorkers = 1;
                    break;
            }

            Players.Add(new Player("ewaID" + i, "ewa" + i, animalsDeck.DrawCard(), goodsDeck.DrawCard(), startingWorkers));
        }
        return Players;
    }

    private static List<ProjectCard> PrepareProjectCards(Deck mainDeck, int howManyPlayers) {
        var AvailableProjectCards = new List<ProjectCard>();

        int howManyProjectCardsPerTurn = 0;

        switch (howManyPlayers) {
            case 2:
                howManyProjectCardsPerTurn = 7;
                break;
            case 3:
                howManyProjectCardsPerTurn = 10;
                break;
            case 4:
                howManyProjectCardsPerTurn = 13;
                break;
        }

        for (int i = 0; i < howManyProjectCardsPerTurn; i++) {

            Card topCardFromDeck = mainDeck.DrawCard();
            CardDice cardDice;

            switch (i) {
                case 0:
                    cardDice = CardDice.I;
                    break;
                case 1:
                    cardDice = CardDice.II;
                    break;
                case 2:
                    cardDice = CardDice.III;
                    break;
                case 3:
                    cardDice = CardDice.IV;
                    break;
                case 4:
                    cardDice = CardDice.V;
                    break;
                case 5:
                    cardDice = CardDice.VI;
                    break;
                default:
                    cardDice = topCardFromDeck.Dice;
                    break;
            }


            AvailableProjectCards.Add(new ProjectCard(topCardFromDeck, cardDice));
        }

        return AvailableProjectCards;
    }
}
