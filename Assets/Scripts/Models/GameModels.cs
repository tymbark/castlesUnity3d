using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Models;

namespace Models {

    public class GameState {

        public readonly List<Player> Players;
        public readonly int HowManyPlayers;
        public Round CurrentRound;
        public int CurrentPlayerIndex;

        public readonly Deck MainDeck;
        public readonly Deck AnimalsDeck;
        public readonly Deck GoodsDeck;
        public readonly List<ProjectCard> AvailableProjectCards;
        public readonly List<BonusCard> AvailableBonusCards; 

        public GameState(List<Player> players,
                         Deck mainDeck,
                         Deck animalsDeck,
                         Deck goodsDeck,
                         List<ProjectCard> availableProjectCards,
                         List<BonusCard> availableBonusCards,
                         Round currentRound,
                         int currentPlayerIndex,
                         int howManyPlayers) {
            this.Players = players;
            this.HowManyPlayers = Players.Count;
            this.MainDeck = mainDeck;
            this.AnimalsDeck = animalsDeck;
            this.GoodsDeck = goodsDeck;
            this.AvailableProjectCards = availableProjectCards;
            this.AvailableBonusCards = availableBonusCards;
            this.HowManyPlayers = howManyPlayers;
            this.CurrentRound = currentRound;
            this.CurrentPlayerIndex = currentPlayerIndex;
        }

        public Player CurrentPlayer { get { return Players[CurrentPlayerIndex]; } }

    }

    public enum Round {
        A, B, C, D, E
    }

}
