using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Models;

public class GameEngine {

    private readonly int howManyPlayers = 2; //todo change this

    public GameState GameState { get; private set; }
    public ActionHandler ActionHandler { get; private set; }

    public GameEngine() {

        if (GameState == null) {
            if (DataPersistance.GameStateExists()) {
                GameState = DataPersistance.LoadGameState();
            } else {
                GameState = GenerateGameState();
                StartTurn();
                GameState.Save();
            }
        }

        ActionHandler = new ActionHandler(this);

        GameState.Players[0].CompletedProjects.Add(GameState.MainDeck.DrawCard());
        GameState.Players[0].ProjectArea.Add(GameState.MainDeck.DrawCard());
        GameState.Players[0].ProjectArea.Add(GameState.MainDeck.DrawCard());
        GameState.Players[0].ProjectArea.Add(GameState.MainDeck.DrawCard());
        GameState.Players[1].ProjectArea.Add(GameState.MainDeck.DrawCard());
        GameState.Players[1].Animals.Add(GameState.AnimalsDeck.DrawCard());
    }

    private GameState GenerateGameState() {
        var mainDeck = DeckGenerator.GenerateActionsDeck();
        var animalsDeck = DeckGenerator.GenerateAnimalsDeck();
        var goodsDeck = DeckGenerator.GenerateGoodsDeck();
        var projectCards = PrepareProjectCards(mainDeck, howManyPlayers);
        var players = PreparePlayers(animalsDeck, goodsDeck);

        return new GameState(players, mainDeck, animalsDeck, goodsDeck,
                             projectCards, Round.A, 0, players.Count);
    }

    private List<Player> PreparePlayers(Deck animalsDeck, Deck goodsDeck) {
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

            Players.Add(new Player("ewa" + i, animalsDeck.DrawCard(), goodsDeck.DrawCard(), startingWorkers));
        }
        return Players;
    }


    public void StartTurn() {
        GameState.CurrentPlayerIndex = 0;
        DrawCards();
    }


    public void NextTurn() {

        if (GameState.Players.IndexOf(GameState.CurrentPlayer) == GameState.Players.Count - 1) {
            GameState.CurrentPlayerIndex = 0;
        } else {
            GameState.CurrentPlayerIndex = GameState.CurrentPlayerIndex + 1;
        }

        if (GameState.CurrentPlayer.Cards.Count == 1) {
            DrawCards();
        }
    }

    private void DrawCards() {
        foreach (Player p in GameState.Players) {
            p.DrawCards(GameState.MainDeck);
        }
    }

    private static List<ProjectCard> PrepareProjectCards(Deck mainDeck, int playersCount) {
        var AvailableProjectCards = new List<ProjectCard>();

        int howManyProjectCardsPerTurn = 0;

        switch (playersCount) {
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