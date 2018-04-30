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
        public readonly List<Card> SilverActionCards;

        public Estate Estate;

        public int Score;
        public int WorkersCount;
        public int SilverCount;
        public bool SilverActionDoneThisTurn = false;

        public Player(string name, Card startingAnimal, Card startingGood, int startingWorkers) {
            Cards = new List<Card>();
            FutureCards = new List<Card>();
            Animals = new List<Card>();
            Goods = new List<Card>();
            ProjectArea = new List<Card>();
            SilverActionCards = new List<Card>();

            Estate = new Estate();
            Name = name;
            WorkersCount = startingWorkers;
            SilverCount = 1; // default starting value

            Goods.Add(startingGood);
            Animals.Add(startingAnimal);
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

    public class TripleCards {

        public readonly int ID;

        public Card First { get; private set; }
        public Card Second { get; private set; }
        public Card Third { get; private set; }

        public TripleCards() {
            ID = GetHashCode();
            Debug.Log("ID " + ID);
        }

        public void Add(Card card) {
            if (First == null) {
                First = card;
            } else if (Second == null) {
                Second = card;
            } else if (Third == null) {
                Third = card;
            } else {
                throw new System.InvalidOperationException("Cannot add more than 3 cards to a Three!");
            }
        }

        public CardClass Class() {

            if (All().Count == 0) {
                throw new System.InvalidProgramException("Cannot check the type of empty TripleClass!");
            }

            if (All().FindAll((Card obj) => obj.Class == CardClass.ActionCloister).Count == 0) {
                return First.Class; // there are no cloisters
            }

            if (All().FindAll((Card obj) => obj.Class == CardClass.ActionCloister).Count == All().Count) {
                return First.Class; // there are only cloisters
            }

            Card notCloisterCard = All().FindAll((Card obj) => obj.Class != CardClass.ActionCloister)[0];

            return notCloisterCard.Class;
        }

        public bool HasFirst() {
            return First != null;
        }

        public bool HasSecond() {
            return HasFirst() && Second != null;
        }

        public bool HasThird() {
            return HasFirst() && HasSecond() && Third != null;
        }

        public List<Card> All() {
            var cards = new List<Card>();

            if (HasFirst()) {
                cards.Add(First);
            }

            if (HasSecond()) {
                cards.Add(Second);
            }

            if (HasThird()) {
                cards.Add(Third);
            }

            return cards;
        }

    }

    public class Estate {
        public List<TripleCards> Buildings { get; private set; }

        public Estate() {
            Buildings = new List<TripleCards>();
        }

        public List<Card> All() {
            List<Card> output = new List<Card>();

            foreach (TripleCards tc in Buildings) {
                output.AddRange(tc.All());
            }

            return output;
        }

        public List<TripleCards> NotFinishedTripes() {
            List<TripleCards> output = new List<TripleCards>();

            foreach (TripleCards tc in Buildings) {
                if (tc.All().Count < 3) {
                    output.Add(tc);
                }
            }

            return output;
        }

        public void AddProjectToEstate(Card card, int tripleCardsId = 0) {

            if (tripleCardsId != 0) {
                TripleCards tc = Buildings.Find((TripleCards obj) => obj.ID == tripleCardsId);
                if (tc == null) {
                    throw new System.InvalidProgramException("Cannot find given triple card ID!" + tripleCardsId);
                }

                tc.Add(card);
                return;
            } else {
                var newThreeCards = new TripleCards();
                newThreeCards.Add(card);
                Buildings.Add(newThreeCards);
            }
        }
    }
}