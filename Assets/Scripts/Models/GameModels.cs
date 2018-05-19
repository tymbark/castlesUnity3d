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
        public string CurrentPlayerNickName;
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
                         string currentPlayerNickName,
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
            this.CurrentPlayerNickName = currentPlayerNickName;
            this.IsFinished = isFinished;
        }

        public Player CurrentPlayer {
            get {
                return Players.Find((obj) => obj.NickName == CurrentPlayerNickName);
            }
        }

    }

    public enum Round {
        A, B, C, D, E
    }

    public static class GameStateHelperMethods {

        public static bool ItsMyTurn(this GameState gameState) {
            return DataPersistance.GetPlayerNickName() == gameState.CurrentPlayer.NickName;
        }

        public static bool HasNotStarted(this GameState gameState) {
            return gameState.Players.TrueForAll((Player p) =>
                                                p.Cards.IsEmpty() &&
                                                p.FutureCards.IsEmpty() &&
                                                p.Score == 0);
        }
    }
}
