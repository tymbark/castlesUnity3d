using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Models;

public class GameEngine {

    public GameState GameState { get; private set; }
    public ActionHandler ActionHandler { get; private set; }

    public GameEngine() {

        if (false && DataPersistance.GameStateExists()) {
            GameState = DataPersistance.LoadGameState();
        } else {
            //only for debug - starting game scene
            List<string> nicknames = new List<string>();
            nicknames.Add("ewa");
            nicknames.Add("katarzyna");
            GameState = GameStateGenerator.GenerateGameState("DEBUG_ID", 2, nicknames, "ewa");
            AddDebugOptions(GameState);
            StartGame();
            DataPersistance.SavePlayerNickName("ewa");
            GameState.SaveGameState();
        }

        ActionHandler = new ActionHandler(this);
    }

    public void UpdateGameState() {
        GameState = DataPersistance.LoadGameState();
    }

    private void AddDebugOptions(GameState gameState) {


        gameState.Players[0].ProjectArea.Add(new Card(CardClass.ActionCastle, CardDice.II));
        gameState.Players[0].ProjectArea.Add(new Card(CardClass.ActionCastle, CardDice.II));
        gameState.Players[0].ProjectArea.Add(new Card(CardClass.ActionCastle, CardDice.II));
        gameState.Players[0].CompleteProject(new Card(CardClass.ActionCastle, CardDice.II), GameState);
        gameState.Players[0].CompleteProject(new Card(CardClass.ActionCastle, CardDice.II), GameState);


        gameState.Players[0].BonusActionCards.Add(new Card(CardClass.BonusCastle, CardDice.All));
    }

    public void StartGame() {
        GameState.CurrentPlayer.ReceivedBonuses.Add(BonusCard.FirstPlayer);
        GameState.CurrentTurn = 1;
        DrawFutureCards();
        DrawHandCards();
    }

    public void ExecuteEndTurnAction() {
        List<string> nicks = GameState.Players.ConvertAll(p => p.NickName);
        int indexOfCurrentNick = nicks.IndexOf(GameState.CurrentPlayerNickName);

        if (indexOfCurrentNick + 1 < GameState.Players.Count) {
            GameState.CurrentPlayerNickName = nicks[indexOfCurrentNick + 1];
        } else {
            GameState.CurrentPlayerNickName = nicks[0];
            NextTurn();
        }

        UnityEngine.Debug.Log("Current player: " + GameState.CurrentPlayer.NickName);
        UnityEngine.Debug.Log("Current round: " + GameState.CurrentRound);
        UnityEngine.Debug.Log("Current turn: " + GameState.CurrentTurn);
    }

    private void NextTurn() {
        if (GameState.CurrentTurn >= 1 && GameState.CurrentTurn <= 6) {
            GameState.CurrentTurn = GameState.CurrentTurn + 1;
            DrawHandCards();
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