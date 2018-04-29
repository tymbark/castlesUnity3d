using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Models {

    public class GameState {

        readonly List<Player> Players;
        public readonly Deck ActionsDeck;

        public Player CurrentPlayer { get; private set; }

        public GameState(List<Player> players, Deck actionsDeck) {
            this.ActionsDeck = actionsDeck;
            if (players.Count < 2) {
                throw new System.InvalidOperationException("Players size cannot be smaller than 2!");
            }

            Players = players;
        }

        public void StartTurn() {
            CurrentPlayer = Players[0];
            DrawCards();
        }


        public void NextTurn() {

            if (Players.IndexOf(CurrentPlayer) == Players.Count - 1) {
                CurrentPlayer = Players[0];
            } else {
                CurrentPlayer = Players[Players.IndexOf(CurrentPlayer) + 1];
            }

            if (CurrentPlayer.Cards.Count == 1) {
                DrawCards();
            }
        }

        private void DrawCards() {
            foreach (Player p in Players) {
                p.DrawCards(ActionsDeck);
            }
        }

    }

    public class GameTable {
        public GameTable() {
        }
        public List<Card> cards = new List<Card>();


        public CardType Type { get; private set; }

    }

}
