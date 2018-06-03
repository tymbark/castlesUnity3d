using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Models;
using GSP = GameStateProvider;

public class GameEngine {

    public GameEngine() {

        if (!DataPersistance.GameStateExists()) {
            //only for debug - starting game scene
            List<string> nicknames = new List<string>();
            nicknames.Add("ewa");
            nicknames.Add("katarzyna");
            var GameState = GameStateGenerator.GenerateGameState("DEBUG_ID", 2, nicknames, "ewa");
            //AddDebugOptions();
            StartGame();
            DataPersistance.SavePlayerNickName("ewa");
            GameState.SaveGameState();
        }

    }

    private static void AddDebugOptions() {

        GSP.GameState.Players[0].WorkersCount = 40;
        GSP.GameState.Players[0].SilverCount = 40;
        GSP.GameState.Players[0].ProjectArea.Add(new Card(CardClass.ActionMarket, CardDice.II));
        GSP.GameState.Players[0].CompletedProjects.Add(new Card(CardClass.ActionBank, CardDice.I, 0, 1));
        GSP.GameState.Players[0].CompletedProjects.Add(new Card(CardClass.ActionBank, CardDice.II, 0, 1));

    }

    public static void StartGame() {
        AddDebugOptions();
        GSP.GameState.CurrentPlayer.ReceivedBonuses.Add(BonusCard.FirstPlayer);
        GSP.GameState.CurrentTurn = 1;
        DrawFutureCards();
        DrawHandCards();
    }

    public static void ExecuteEndTurnAction() {
        List<string> nicks = GSP.GameState.Players.ConvertAll(p => p.NickName);
        int indexOfCurrentNick = nicks.IndexOf(GSP.GameState.CurrentPlayerNickName);

        if (indexOfCurrentNick + 1 < GSP.GameState.Players.Count) {
            GSP.GameState.CurrentPlayerNickName = nicks[indexOfCurrentNick + 1];
        } else {
            // only last player does this:
            GSP.GameState.CurrentPlayerNickName = nicks[0];
            NextTurn();
        }

        UnityEngine.Debug.Log("Current player: " + GSP.GameState.CurrentPlayer.NickName);
        UnityEngine.Debug.Log("Current round: " + GSP.GameState.CurrentRound);
        UnityEngine.Debug.Log("Current turn: " + GSP.GameState.CurrentTurn);
    }

    private static void NextTurn() {
        int currentTurn = GSP.GameState.CurrentTurn;

        if (currentTurn >= 1 && currentTurn <= 5) {
            GSP.GameState.CurrentTurn = currentTurn + 1;
            DrawHandCards();
        } else if (currentTurn == 6) {
            NextRound();
            GSP.GameState.CurrentTurn = 1;
        } else {
            throw new System.InvalidProgramException("Illegal turn :" + GSP.GameState.CurrentTurn);
        }
    }

    private static void NextRound() {
        switch (GSP.GameState.CurrentRound) {
            case Round.A:
                GSP.GameState.CurrentRound = Round.B;
                DrawProjectCards();
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.B:
                GSP.GameState.CurrentRound = Round.C;
                DrawProjectCards();
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.C:
                GSP.GameState.CurrentRound = Round.D;
                DrawProjectCards();
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.D:
                GSP.GameState.CurrentRound = Round.E;
                DrawProjectCards();
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.E:
                FinishGame();
                break;
        }
    }

    private static void FinishGame() {
        GSP.GameState.IsFinished = true;
    }

    private static void DrawFutureCards() {
        foreach (Player p in GSP.GameState.Players) {
            if (p.FutureCards.Count == 0) {
                Utils.Repeat(6, () => { p.FutureCards.Add(GSP.GameState.MainDeck.DrawCard()); });
            } else {
                throw new System.InvalidProgramException("Future cards can be redrawn only if empty");
            }
        }
    }

    private static void DrawHandCards() {
        foreach (Player p in GSP.GameState.Players) {
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
                        Card newCard = p.FutureCards[p.FutureCards.Count - 1];
                        p.FutureCards.Remove(newCard);
                        p.Cards.Add(newCard);
                    }
                    break;
            }
        }
    }

    private static void DrawProjectCards() {

        int howManyProjectCardsPerTurn = 0;

        switch (GSP.GameState.HowManyPlayers) {
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

            Card topCardFromDeck = GSP.GameState.MainDeck.DrawCard();
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


            GSP.GameState.AvailableProjectCards.Add(new ProjectCard(topCardFromDeck, cardDice));
        }
    }

}