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
            if (false && DataPersistance.GameStateExists()) {
                GameState = DataPersistance.LoadGameState();
            } else {
                GameState = GenerateGameState();
                StartGame();
                GameState.Save();
                AddDebugOptions(GameState);
            }
        }

        ActionHandler = new ActionHandler(this);
    }

    private void AddDebugOptions(GameState gameState) {

        var c1 = gameState.MainDeck.DrawCard();
        var c2 = gameState.MainDeck.DrawCard();
        var c3 = gameState.MainDeck.DrawCard();
        var c4 = gameState.MainDeck.DrawCard();
        var c5 = gameState.MainDeck.DrawCard();
        gameState.Players[0].ProjectArea.Add(c1);
        gameState.Players[0].ProjectArea.Add(c2);
        gameState.Players[0].ProjectArea.Add(c3);
        gameState.Players[0].ProjectArea.Add(c4);
        gameState.Players[0].ProjectArea.Add(c5);
        gameState.Players[0].CompleteProject(c1, gameState);
        gameState.Players[0].CompleteProject(c2, gameState);
        gameState.Players[0].CompleteProject(c3, gameState);
        gameState.Players[0].CompleteProject(c4, gameState);
        gameState.Players[0].CompleteProject(c5, gameState);

        //players[0].BonusActionCards.Add(new Card(CardClass.BonusCarperter, CardDice.All));
        //players[0].BonusActionCards.Add(new Card(CardClass.BonusCityHall, CardDice.All));
    }

    private GameState GenerateGameState() {
        var mainDeck = DeckGenerator.GenerateActionsDeck();
        var animalsDeck = DeckGenerator.GenerateAnimalsDeck();
        var goodsDeck = DeckGenerator.GenerateGoodsDeck();
        var projectCards = PrepareProjectCards(mainDeck);
        var players = PreparePlayers(animalsDeck, goodsDeck);
        var bonuses = PrepareAvailableBonuses();

        return new GameState(players,
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

    private List<BonusCard> PrepareAvailableBonuses() {
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

    private void StartGame() {
        GameState.CurrentPlayerIndex = 0;
        GameState.CurrentTurn = 1;
        DrawFutureCards();
        DrawHandCards();
    }

    public void ExecuteEndTurnAction() {
        if (GameState.CurrentPlayerIndex + 1 < GameState.Players.Count) {
            GameState.CurrentPlayerIndex = GameState.CurrentPlayerIndex + 1;
        } else {
            GameState.CurrentPlayerIndex = 0;
            NextTurn();
        }
        UnityEngine.Debug.Log("Current player: " + GameState.CurrentPlayer.Name);
        UnityEngine.Debug.Log("Current round: " + GameState.CurrentRound);
        UnityEngine.Debug.Log("Current turn: " + GameState.CurrentTurn);
    }

    private void NextTurn() {
        if (GameState.CurrentTurn >= 1 && GameState.CurrentTurn <= 6) {
            GameState.CurrentTurn = GameState.CurrentTurn + 1;
        } else if (GameState.CurrentTurn == 7) {
            NextRound();
            GameState.CurrentTurn = 1;
        } else {
            throw new System.InvalidProgramException("Illegal turn :" + GameState.CurrentTurn);
        }
    }

    private void NextRound() {
        switch (GameState.CurrentRound) {
            case Round.A:
                GameState.CurrentRound = Round.B;
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.B:
                GameState.CurrentRound = Round.C;
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.C:
                GameState.CurrentRound = Round.D;
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.D:
                GameState.CurrentRound = Round.E;
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.E:
                FinishGame();
                break;
        }
    }

    private void FinishGame() {

    }

    private void DrawFutureCards() {
        foreach (Player p in GameState.Players) {
            if (p.FutureCards.Count == 0) {
                Utils.Repeat(6, () => { p.FutureCards.Add(GameState.MainDeck.DrawCard()); });
            } else {
                throw new System.InvalidProgramException("Future cards can be redrawn only if empty");
            }
        }
    }

    private void DrawHandCards() {
        foreach (Player p in GameState.Players) {
            switch (p.Cards.Count) {
                case 0:
                    if (p.FutureCards.Count < 2) {
                        throw new System.InvalidProgramException("Not enough future cards.");
                    }

                    Utils.Repeat(2, () => {
                        Card c = p.FutureCards[p.FutureCards.Count - 1];
                        p.FutureCards.Remove(c);
                        p.Cards.Add(c);
                    });
                    break;
                case 1:
                    if (p.FutureCards.Count == 0) {
                        // last turn!
                    } else {
                        Card c = p.FutureCards[p.FutureCards.Count - 1];
                        p.FutureCards.Remove(c);
                        p.Cards.Add(c);
                    }
                    break;
            }
        }
    }

    private List<ProjectCard> PrepareProjectCards(Deck mainDeck) {
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