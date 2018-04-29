using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class DeckGenerator {

    public static Deck GenerateAnimalsDeck() {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < 5; i++) {
            cards.Add(new Card(CardType.Animal, CardClass.Chicken));
            cards.Add(new Card(CardType.Animal, CardClass.Cow));
            cards.Add(new Card(CardType.Animal, CardClass.Sheep));
            cards.Add(new Card(CardType.Animal, CardClass.Pig));
        }

        return new Models.Deck(cards);
    }

    public static Deck GenerateGoodsDeck() {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < 6; i++) {
            cards.Add(new Card(CardType.Goods, CardClass.None, CardDice.I_II));
            cards.Add(new Card(CardType.Goods, CardClass.None, CardDice.III_IV));
            cards.Add(new Card(CardType.Goods, CardClass.None, CardDice.V_VI));
        }

        return new Models.Deck(cards);
    }

    public static Deck GenerateActionsDeck() {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < 2; i++) {
            cards.Add(new Card(CardType.Actions, CardClass.ActionCastle, CardDice.I));
            cards.Add(new Card(CardType.Actions, CardClass.ActionCastle, CardDice.II));
            cards.Add(new Card(CardType.Actions, CardClass.ActionCastle, CardDice.III));
            cards.Add(new Card(CardType.Actions, CardClass.ActionCastle, CardDice.IV));
            cards.Add(new Card(CardType.Actions, CardClass.ActionCastle, CardDice.V));
            cards.Add(new Card(CardType.Actions, CardClass.ActionCastle, CardDice.VI));

            cards.Add(new Card(CardType.Actions, CardClass.ActionMine, CardDice.I));
            cards.Add(new Card(CardType.Actions, CardClass.ActionMine, CardDice.II));
            cards.Add(new Card(CardType.Actions, CardClass.ActionMine, CardDice.III));
            cards.Add(new Card(CardType.Actions, CardClass.ActionMine, CardDice.IV));
            cards.Add(new Card(CardType.Actions, CardClass.ActionMine, CardDice.V));
            cards.Add(new Card(CardType.Actions, CardClass.ActionMine, CardDice.VI));

            cards.Add(new Card(CardType.Actions, CardClass.ActionCloister, CardDice.I));
            cards.Add(new Card(CardType.Actions, CardClass.ActionCloister, CardDice.II));
            cards.Add(new Card(CardType.Actions, CardClass.ActionCloister, CardDice.III));
            cards.Add(new Card(CardType.Actions, CardClass.ActionCloister, CardDice.IV));
            cards.Add(new Card(CardType.Actions, CardClass.ActionCloister, CardDice.V));
            cards.Add(new Card(CardType.Actions, CardClass.ActionCloister, CardDice.VI));
        }


        for (int i = 0; i < 3; i++) {
            cards.Add(new Card(CardType.Actions, CardClass.ActionKnowledge, CardDice.I));
            cards.Add(new Card(CardType.Actions, CardClass.ActionKnowledge, CardDice.II));
            cards.Add(new Card(CardType.Actions, CardClass.ActionKnowledge, CardDice.III));
            cards.Add(new Card(CardType.Actions, CardClass.ActionKnowledge, CardDice.IV));
            cards.Add(new Card(CardType.Actions, CardClass.ActionKnowledge, CardDice.V));
            cards.Add(new Card(CardType.Actions, CardClass.ActionKnowledge, CardDice.VI));

            cards.Add(new Card(CardType.Actions, CardClass.ActionPasture, CardDice.I));
            cards.Add(new Card(CardType.Actions, CardClass.ActionPasture, CardDice.II));
            cards.Add(new Card(CardType.Actions, CardClass.ActionPasture, CardDice.III));
            cards.Add(new Card(CardType.Actions, CardClass.ActionPasture, CardDice.IV));
            cards.Add(new Card(CardType.Actions, CardClass.ActionPasture, CardDice.V));
            cards.Add(new Card(CardType.Actions, CardClass.ActionPasture, CardDice.VI));

            cards.Add(new Card(CardType.Actions, CardClass.ActionShip, CardDice.I));
            cards.Add(new Card(CardType.Actions, CardClass.ActionShip, CardDice.II));
            cards.Add(new Card(CardType.Actions, CardClass.ActionShip, CardDice.III));
            cards.Add(new Card(CardType.Actions, CardClass.ActionShip, CardDice.IV));
            cards.Add(new Card(CardType.Actions, CardClass.ActionShip, CardDice.V));
            cards.Add(new Card(CardType.Actions, CardClass.ActionShip, CardDice.VI));
        }


        cards.Add(new Card(CardType.Actions, CardClass.ActionCarpenter, CardDice.I));
        cards.Add(new Card(CardType.Actions, CardClass.ActionCarpenter, CardDice.II));
        cards.Add(new Card(CardType.Actions, CardClass.ActionCarpenter, CardDice.III));

        cards.Add(new Card(CardType.Actions, CardClass.ActionChurch, CardDice.I));
        cards.Add(new Card(CardType.Actions, CardClass.ActionChurch, CardDice.II));
        cards.Add(new Card(CardType.Actions, CardClass.ActionChurch, CardDice.III));

        cards.Add(new Card(CardType.Actions, CardClass.ActionMarket, CardDice.I));
        cards.Add(new Card(CardType.Actions, CardClass.ActionMarket, CardDice.II));
        cards.Add(new Card(CardType.Actions, CardClass.ActionMarket, CardDice.III));

        cards.Add(new Card(CardType.Actions, CardClass.ActionWatchtower, CardDice.I));
        cards.Add(new Card(CardType.Actions, CardClass.ActionWatchtower, CardDice.II));
        cards.Add(new Card(CardType.Actions, CardClass.ActionWatchtower, CardDice.III));

        cards.Add(new Card(CardType.Actions, CardClass.ActionCityHall, CardDice.IV));
        cards.Add(new Card(CardType.Actions, CardClass.ActionCityHall, CardDice.V));
        cards.Add(new Card(CardType.Actions, CardClass.ActionCityHall, CardDice.VI));

        cards.Add(new Card(CardType.Actions, CardClass.ActionWarehouse, CardDice.IV));
        cards.Add(new Card(CardType.Actions, CardClass.ActionWarehouse, CardDice.V));
        cards.Add(new Card(CardType.Actions, CardClass.ActionWarehouse, CardDice.VI));

        cards.Add(new Card(CardType.Actions, CardClass.ActionBoardinghouse, CardDice.IV));
        cards.Add(new Card(CardType.Actions, CardClass.ActionBoardinghouse, CardDice.V));
        cards.Add(new Card(CardType.Actions, CardClass.ActionBoardinghouse, CardDice.VI));

        cards.Add(new Card(CardType.Actions, CardClass.ActionBank, CardDice.IV));
        cards.Add(new Card(CardType.Actions, CardClass.ActionBank, CardDice.V));
        cards.Add(new Card(CardType.Actions, CardClass.ActionBank, CardDice.VI));

        return new Models.Deck(cards);
    }

}
