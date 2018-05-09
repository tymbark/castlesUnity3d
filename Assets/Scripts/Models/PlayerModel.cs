using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models {

    public class Player {

        public readonly string Name;

        public readonly List<Card> Cards;
        public readonly List<Card> FutureCards;
        public readonly List<Card> Animals;
        public readonly List<Card> Goods;
        public readonly List<Card> ProjectArea;
        public readonly List<Card> BonusActionCards;
        public readonly List<Card> CompletedProjects;
        public readonly List<BonusCard> ReceivedBonuses;

        public int Score;
        public int WorkersCount;
        public int SilverCount;
        public int CurrentTurn;
        public bool SilverActionDoneThisTurn = false;

        public Player(string name, Card startingAnimal, Card startingGood, int startingWorkers) {
            Cards = new List<Card>();
            FutureCards = new List<Card>();
            Animals = new List<Card>();
            Goods = new List<Card>();
            ProjectArea = new List<Card>();
            BonusActionCards = new List<Card>();
            CompletedProjects = new List<Card>();
            ReceivedBonuses = new List<BonusCard>();

            Name = name;
            WorkersCount = startingWorkers;
            SilverCount = 1; // default starting value

            Goods.Add(startingGood);
            Animals.Add(startingAnimal);
        }

        public Player(List<Card> cards,
                      List<Card> futureCards,
                      List<Card> animals,
                      List<Card> goods,
                      List<Card> projectArea,
                      List<Card> silverActionCards,
                      List<Card> completedProjects,
                      List<BonusCard> receivedBonuses,
                      string name,
                      int score,
                      int workersCount,
                      int silverCount,
                      bool silverDoneThisTurn) {
            Name = name;
            Cards = cards;
            FutureCards = futureCards;
            Animals = animals;
            Goods = goods;
            ProjectArea = projectArea;
            BonusActionCards = silverActionCards;
            CompletedProjects = completedProjects;
            Score = score;
            WorkersCount = workersCount;
            SilverCount = silverCount;
            SilverActionDoneThisTurn = silverDoneThisTurn;
            ReceivedBonuses = receivedBonuses;
        }

        public void DrawCards(Deck deck) {

            if (FutureCards.Count == 0) {

                Utils.Repeat(6, () => { FutureCards.Add(deck.DrawCard()); });
                Utils.Repeat(2, () => {
                    Card c = FutureCards[FutureCards.Count - 1];
                    FutureCards.Remove(c);
                    Cards.Add(c);
                });

            } else {

                if (FutureCards.Count > 0) {

                    Card c = FutureCards[FutureCards.Count - 1];
                    FutureCards.Remove(c);
                    Cards.Add(c);

                }
            }

            switch (Cards.Count) {
                case 0:
                    Cards.Add(deck.DrawCard());
                    Cards.Add(deck.DrawCard());
                    break;
                case 1:
                    Cards.Add(deck.DrawCard());
                    break;
            }
        }

    }

}