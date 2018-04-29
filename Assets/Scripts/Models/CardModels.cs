using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Models {

    public enum CardType {
        Animal, Actions, Worker, Silver, Goods,
        EndTurn, None // its not exactly a Card, but it's convenient
    }

    public enum CardClass {
        // Worker, Silver
        None,

        // Animals
        Chicken, Cow, Sheep, Pig,
        Goods,

        // Enviroment
        Dice,

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
        public Card(CardType cardType, CardClass cardClass = CardClass.None, CardDice cardDice = CardDice.O) {
            Type = cardType;
            Dice = cardDice;
            Class = cardClass;
        }

        public readonly CardType Type;
        public readonly CardClass Class;
        public readonly CardDice Dice;

        public override string ToString() {
            return "Card[ " + Type + " - " + Class + " - " + Dice + " ]";
        }

        public bool CompareTo(Card card) {
            return card.Type == Type
                       && card.Class == Class
                       && card.Dice == Dice;
        }

        public static Card EndTurnCard() {
            return new Card(CardType.EndTurn, CardClass.None, CardDice.O);
        }

        public static Card Dummy = new Card(CardType.None, CardClass.None, CardDice.O);

        public static Card DummyDiceI = new Card(CardType.None, CardClass.None, CardDice.I);
        public static Card DummyDiceII = new Card(CardType.None, CardClass.None, CardDice.II);
        public static Card DummyDiceIII = new Card(CardType.None, CardClass.None, CardDice.III);
        public static Card DummyDiceIV = new Card(CardType.None, CardClass.None, CardDice.IV);
        public static Card DummyDiceV = new Card(CardType.None, CardClass.None, CardDice.V);
        public static Card DummyDiceVI = new Card(CardType.None, CardClass.None, CardDice.VI);

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

        public Deck(List<Card> cards) {
            Cards = cards;
            Cards.Shuffle();
        }

        public readonly List<Card> Cards;

        public Card DrawCard() {
            Card card = Cards[Cards.Count - 1];
            Cards.Remove(card);
            return card;
        }

    }

}
