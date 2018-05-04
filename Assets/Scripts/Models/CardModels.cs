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
        Dice, Worker, Silver, EndTurn, Exit, Points,
        AllProjects, AllEstates, AllAnimals, AllStorages,
        SellSilverAndWorkers, ShipGoods,

        // Bonuses
        BonusA, BonusB, BonusC, BonusD, BonusE,
        BonusCastle, BonusMine, BonusCloister, BonusKnowledge, BonusShip, BonusPasture,
        BonusAllSeven1, BonusAllSeven2, BonusAllSeven3, BonusAllSeven4,

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
        I_II, III_IV, V_VI // for goods
    }

    public class Card {
        public Card(CardClass cardClass, CardDice cardDice = CardDice.O) {
            Dice = cardDice;
            Class = cardClass;
        }

        public readonly CardClass Class;
        public readonly CardDice Dice;

        public string Describe() {
            return "Card[ " + Class + " - " + Dice + " ]";
        }

        public bool IsEqualTo(Card card) {
            return card.Class == Class && card.Dice == Dice;
        }

        public static Card Dummy = new Card(CardClass.None, CardDice.O);

        public static Card DummyDiceI = new Card(CardClass.Dice, CardDice.I);
        public static Card DummyDiceII = new Card(CardClass.Dice, CardDice.II);
        public static Card DummyDiceIII = new Card(CardClass.Dice, CardDice.III);
        public static Card DummyDiceIV = new Card(CardClass.Dice, CardDice.IV);
        public static Card DummyDiceV = new Card(CardClass.Dice, CardDice.V);
        public static Card DummyDiceVI = new Card(CardClass.Dice, CardDice.VI);

        public static Card DummyAllProjects = new Card(CardClass.AllProjects, CardDice.O);
        public static Card DummyAllStorages = new Card(CardClass.AllStorages, CardDice.O);
        public static Card DummyAllEstates = new Card(CardClass.AllEstates, CardDice.O);
        public static Card DummyAllAnimals = new Card(CardClass.AllAnimals, CardDice.O);
        public static Card DummySellSilverAndWorkers = new Card(CardClass.SellSilverAndWorkers, CardDice.O);
        public static Card DummyShipGoods = new Card(CardClass.ShipGoods, CardDice.O);
        public static Card DummySilver = new Card(CardClass.Silver, CardDice.O);
        public static Card DummyWorker = new Card(CardClass.Worker, CardDice.O);
        public static Card DummyExit = new Card(CardClass.Exit, CardDice.O);
        public static Card DummyEndTurn = new Card(CardClass.EndTurn, CardDice.O);
        public static Card DummyPoints = new Card(CardClass.Points, CardDice.O);

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
            Card card = Cards[Cards.Count - 1];
            Cards.Remove(card);
            return card;
        }

    }

}
