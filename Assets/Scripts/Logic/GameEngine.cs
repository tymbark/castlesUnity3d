using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Models;

public class GameEngine {

    public GameState GameState { get; private set; }
    public ActionHandler ActionHandler { get; private set; }

    public GameEngine() {

        if (GameState == null) {
            if (DataPersistance.GameStateExists()) {
                GameState = DataPersistance.LoadGameState();
            } else {
                //only for debug - starting game scene
                List<string> nicknames = new List<string>();
                nicknames.Add("ewa");
                nicknames.Add("katarzyna");
                GameState = GameStateGenerator.GenerateGameState("DEBUG_ID", 2, nicknames);
                StartGame();
                GameState.SaveGameState();
                AddDebugOptions(GameState);
            }
        }

        ActionHandler = new ActionHandler(this);
    }

    public void Refresh() {
        GameState = DataPersistance.LoadGameState();
    }

    private void AddDebugOptions(GameState gameState) {

        var c1 = gameState.MainDeck.DrawCard();
        var c2 = gameState.MainDeck.DrawCard();
        var c3 = gameState.MainDeck.DrawCard();
        var c4 = gameState.MainDeck.DrawCard();
        var c5 = gameState.MainDeck.DrawCard();
        gameState.Players[0].ProjectArea.Add(c1);
        //gameState.Players[0].ProjectArea.Add(c2);
        //gameState.Players[0].ProjectArea.Add(c3);
        //gameState.Players[0].ProjectArea.Add(c4);
        //gameState.Players[0].ProjectArea.Add(c5);
        //gameState.Players[0].CompleteProject(c1, gameState);
        //gameState.Players[0].CompleteProject(c2, gameState);
        //gameState.Players[0].CompleteProject(c3, gameState);
        //gameState.Players[0].CompleteProject(c4, gameState);
        //gameState.Players[0].CompleteProject(c5, gameState);

        gameState.Players[0].BonusActionCards.Add(new Card(CardClass.BonusCarperter, CardDice.All));
        gameState.Players[0].BonusActionCards.Add(new Card(CardClass.BonusWarehouse, CardDice.All));
        gameState.Players[0].BonusActionCards.Add(new Card(CardClass.BonusChurch, CardDice.All));

        //players[0].BonusActionCards.Add(new Card(CardClass.BonusCityHall, CardDice.All));
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
        UnityEngine.Debug.Log("Current player: " + GameState.CurrentPlayer.NickName);
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

}