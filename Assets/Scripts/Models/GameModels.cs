using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Models;

namespace Models {

    public class GameState {

        public List<Player> Players;
        public int howManyPlayers;
        public Round CurrentRound;
        public Player CurrentPlayer;

        public Deck MainDeck;
        public Deck AnimalsDeck;
        public Deck GoodsDeck;
        public List<ProjectCard> AvailableProjectCards;

        public GameState(List<Player> players,
                         Deck mainDeck,
                         Deck animalsDeck,
                         Deck goodsDeck,
                         List<ProjectCard> availableProjectCards) {
            this.Players = players;
            this.howManyPlayers = Players.Count;
            //this.CurrentPlayer = currentPlayer;
            //this.CurrentRound = currentRound;
            this.MainDeck = mainDeck;
            this.AnimalsDeck = animalsDeck;
            this.GoodsDeck = goodsDeck;
            this.AvailableProjectCards = availableProjectCards;

        }

    }

    public enum Round {
        A, B, C, D, E
    }


}
