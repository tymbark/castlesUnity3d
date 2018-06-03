using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Models {

    public enum CardClass {
        None,

        // Animals
        Chicken, Cow, Sheep, Pig,
        Goods,

        // Enviroment
        Dice, Worker, Silver, EndTurn, Exit,
        AllProjects,
        SellSilverAndWorkers, ShipGoods, Barrel,
        AddSilverProject,

        BonusCastle,

        // Standard Actions 12x - (2x6)
        ActionCastle, ActionMine, ActionCloister,

        // Standard Actions 18x - (3x6)
        ActionKnowledge, ActionShip, ActionPasture,

        // Other Actions 3x (I-III)
        ActionCarpenter,
        ActionChurch,
        ActionMarket,
        ActionWatchtower,

        // Other Actions 3x (IV - VI)
        ActionBank,
        ActionBoardinghouse,
        ActionWarehouse,
        ActionCityHall
    }

    public enum CardDice {
        O, // for cards that dont use dices (Worker, Silver)
        I, II, III, IV, V, VI,
        I_II, III_IV, V_VI, // for goods
        All // for bonus cards
    }

    public enum BonusCard {
        FirstPlayer, FirstPlayerReverse, 
        Building, Castle, Mine, Cloister, Knowledge, Ship, Pasture, 
        AllSeven4, AllSeven3, AllSeven2, AllSeven1
    }

    public class Card {

        // used for distinguish two cards with the same class and dice
        public readonly int Number;

        // used for triple cards in finished buildings. Default 0.
        public readonly int TripleId;

        public Card(CardClass cardClass, CardDice cardDice = CardDice.O, int number = 0, int tripleId = 0) {
            Dice = cardDice;
            Class = cardClass;
            Number = number;
            TripleId = tripleId;
        }

        public readonly CardClass Class;
        public readonly CardDice Dice;

        public string Describe() {
            return "Card[ " + Class + " - " + Dice + " ]";
        }

    }

    public static class StaticCards {
        
        public static Card Dummy = new Card(CardClass.None, CardDice.O);

        public static Card DummyDiceI = new Card(CardClass.Dice, CardDice.I);
        public static Card DummyDiceII = new Card(CardClass.Dice, CardDice.II);
        public static Card DummyDiceIII = new Card(CardClass.Dice, CardDice.III);
        public static Card DummyDiceIV = new Card(CardClass.Dice, CardDice.IV);
        public static Card DummyDiceV = new Card(CardClass.Dice, CardDice.V);
        public static Card DummyDiceVI = new Card(CardClass.Dice, CardDice.VI);

        public static Card DummyAllProjects = new Card(CardClass.AllProjects, CardDice.O);
        public static Card DummyEndTurn = new Card(CardClass.EndTurn, CardDice.O);

    }

    public class ProjectCard {

        public readonly Card Card;
        public readonly CardDice TakeProjectDice;

        public ProjectCard(Card card, CardDice cardDice) {
            Card = card;
            TakeProjectDice = cardDice;
        }
    }

    public class Deck {

        public Deck(List<Card> cards, bool shuffle = true) {
            Cards = cards;
            if (shuffle) {
                Cards.Shuffle();
            }
        }

        public readonly List<Card> Cards;

        public Card DrawCard() {
            if (Cards.Count == 0) {
                throw new System.InvalidProgramException("Deck is empty!");
            }
            Card card = Cards[0];
            Cards.Remove(card);
            return card;
        }

    }

}
