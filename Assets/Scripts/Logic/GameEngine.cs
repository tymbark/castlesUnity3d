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
            AddDebugOptions(GameState);
            StartGame();
            DataPersistance.SavePlayerNickName("ewa");
            GameState.SaveGameState();
        }

    }

    private void AddDebugOptions(GameState gameState) {


        //GSP.GameState.Players[0].ProjectArea.Add(new Card(CardClass.ActionCastle, CardDice.II));
        //GSP.GameState.Players[0].ProjectArea.Add(new Card(CardClass.ActionCastle, CardDice.II));
        //GSP.GameState.Players[0].ProjectArea.Add(new Card(CardClass.ActionShip, CardDice.II));
        //GSP.GameState.Players[0].CompleteProject(new Card(CardClass.ActionCastle, CardDice.II), GameState);
        //GSP.GameState.Players[0].CompleteProject(new Card(CardClass.ActionCastle, CardDice.II), GameState);


        GSP.GameState.Players[0].BonusActionCards.Add(new Card(CardClass.BonusCastle, CardDice.All));
    }

    public void StartGame() {
        //GSP.GameState.CurrentPlayer.ReceivedBonuses.Add(BonusCard.FirstPlayer);
        GSP.GameState.Players[1].ReceivedBonuses.Add(BonusCard.FirstPlayer);
        GSP.GameState.CurrentTurn = 1;
        DrawFutureCards();
        DrawHandCards();
    }

    public void ExecuteEndTurnAction() {
        List<string> nicks = GSP.GameState.Players.ConvertAll(p => p.NickName);
        int indexOfCurrentNick = nicks.IndexOf(GSP.GameState.CurrentPlayerNickName);

        if (indexOfCurrentNick + 1 < GSP.GameState.Players.Count) {
            GSP.GameState.CurrentPlayerNickName = nicks[indexOfCurrentNick + 1];
        } else {
            GSP.GameState.CurrentPlayerNickName = nicks[0];
            NextTurn();
        }

        UnityEngine.Debug.Log("Current player: " + GSP.GameState.CurrentPlayer.NickName);
        UnityEngine.Debug.Log("Current round: " + GSP.GameState.CurrentRound);
        UnityEngine.Debug.Log("Current turn: " + GSP.GameState.CurrentTurn);
    }

    private void NextTurn() {
        if (GSP.GameState.CurrentTurn >= 1 && GSP.GameState.CurrentTurn <= 6) {
            GSP.GameState.CurrentTurn = GSP.GameState.CurrentTurn + 1;
            DrawHandCards();
        } else if (GSP.GameState.CurrentTurn == 7) {
            NextRound();
            GSP.GameState.CurrentTurn = 1;
        } else {
            throw new System.InvalidProgramException("Illegal turn :" + GSP.GameState.CurrentTurn);
        }
    }

    private void NextRound() {
        switch (GSP.GameState.CurrentRound) {
            case Round.A:
                GSP.GameState.CurrentRound = Round.B;
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.B:
                GSP.GameState.CurrentRound = Round.C;
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.C:
                GSP.GameState.CurrentRound = Round.D;
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.D:
                GSP.GameState.CurrentRound = Round.E;
                DrawFutureCards();
                DrawHandCards();
                break;
            case Round.E:
                FinishGame();
                break;
        }
    }

    private void FinishGame() {
        GSP.GameState.IsFinished = true;
    }

    private void DrawFutureCards() {
        foreach (Player p in GSP.GameState.Players) {
            if (p.FutureCards.Count == 0) {
                Utils.Repeat(6, () => { p.FutureCards.Add(GSP.GameState.MainDeck.DrawCard()); });
            } else {
                throw new System.InvalidProgramException("Future cards can be redrawn only if empty");
            }
        }
    }

    private void DrawHandCards() {
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
                        Card c = p.FutureCards[p.FutureCards.Count - 1];
                        p.FutureCards.Remove(c);
                        p.Cards.Add(c);
                    }
                    break;
            }
        }
    }

}