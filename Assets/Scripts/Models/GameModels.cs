using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Models;

namespace Models {

    public class GameState {

        public readonly string Id;
        public readonly List<Player> Players;
        public readonly int HowManyPlayers;
        public Round CurrentRound;
        public int CurrentPlayerIndex;
        public int CurrentTurn;
        public bool IsFinished;

        public readonly Deck MainDeck;
        public readonly Deck AnimalsDeck;
        public readonly Deck GoodsDeck;
        public readonly List<ProjectCard> AvailableProjectCards;
        public readonly List<BonusCard> AvailableBonusCards;

        public GameState(string id,
                         List<Player> players,
                         Deck mainDeck,
                         Deck animalsDeck,
                         Deck goodsDeck,
                         List<ProjectCard> availableProjectCards,
                         List<BonusCard> availableBonusCards,
                         Round currentRound,
                         int currentPlayerIndex,
                         int currentTurn,
                         int howManyPlayers,
                         bool isFinished) {
            this.Id = id;
            this.Players = players;
            this.HowManyPlayers = Players.Count;
            this.MainDeck = mainDeck;
            this.AnimalsDeck = animalsDeck;
            this.GoodsDeck = goodsDeck;
            this.AvailableProjectCards = availableProjectCards;
            this.AvailableBonusCards = availableBonusCards;
            this.HowManyPlayers = howManyPlayers;
            this.CurrentRound = currentRound;
            this.CurrentTurn = currentTurn;
            this.CurrentPlayerIndex = currentPlayerIndex;
            this.IsFinished = isFinished;
        }

        public Player CurrentPlayer { get { return Players[CurrentPlayerIndex]; } }

    }

    public enum Round {
        A, B, C, D, E
    }

}
