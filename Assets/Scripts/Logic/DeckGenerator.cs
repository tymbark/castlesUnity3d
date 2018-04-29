using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class DeckGenerator {

    public static Deck GenerateAnimalsDeck() {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < 5; i++) {
            cards.Add(new Card(CardClass.Chicken));
            cards.Add(new Card(CardClass.Cow));
            cards.Add(new Card(CardClass.Sheep));
            cards.Add(new Card(CardClass.Pig));
        }

        return new Models.Deck(cards);
    }

    public static Deck GenerateGoodsDeck() {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < 6; i++) {
            cards.Add(new Card(CardClass.Goods, CardDice.I_II));
            cards.Add(new Card(CardClass.Goods, CardDice.III_IV));
            cards.Add(new Card(CardClass.Goods, CardDice.V_VI));
        }

        return new Models.Deck(cards);
    }

    public static Deck GenerateActionsDeck() {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < 2; i++) {
            cards.Add(new Card(CardClass.ActionCastle, CardDice.I));
            cards.Add(new Card(CardClass.ActionCastle, CardDice.II));
            cards.Add(new Card(CardClass.ActionCastle, CardDice.III));
            cards.Add(new Card(CardClass.ActionCastle, CardDice.IV));
            cards.Add(new Card(CardClass.ActionCastle, CardDice.V));
            cards.Add(new Card(CardClass.ActionCastle, CardDice.VI));

            cards.Add(new Card(CardClass.ActionMine, CardDice.I));
            cards.Add(new Card(CardClass.ActionMine, CardDice.II));
            cards.Add(new Card(CardClass.ActionMine, CardDice.III));
            cards.Add(new Card(CardClass.ActionMine, CardDice.IV));
            cards.Add(new Card(CardClass.ActionMine, CardDice.V));
            cards.Add(new Card(CardClass.ActionMine, CardDice.VI));

            cards.Add(new Card(CardClass.ActionCloister, CardDice.I));
            cards.Add(new Card(CardClass.ActionCloister, CardDice.II));
            cards.Add(new Card(CardClass.ActionCloister, CardDice.III));
            cards.Add(new Card(CardClass.ActionCloister, CardDice.IV));
            cards.Add(new Card(CardClass.ActionCloister, CardDice.V));
            cards.Add(new Card(CardClass.ActionCloister, CardDice.VI));
        }


        for (int i = 0; i < 3; i++) {
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.I));
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.II));
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.III));
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.IV));
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.V));
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.VI));

            cards.Add(new Card(CardClass.ActionPasture, CardDice.I));
            cards.Add(new Card(CardClass.ActionPasture, CardDice.II));
            cards.Add(new Card(CardClass.ActionPasture, CardDice.III));
            cards.Add(new Card(CardClass.ActionPasture, CardDice.IV));
            cards.Add(new Card(CardClass.ActionPasture, CardDice.V));
            cards.Add(new Card(CardClass.ActionPasture, CardDice.VI));

            cards.Add(new Card(CardClass.ActionShip, CardDice.I));
            cards.Add(new Card(CardClass.ActionShip, CardDice.II));
            cards.Add(new Card(CardClass.ActionShip, CardDice.III));
            cards.Add(new Card(CardClass.ActionShip, CardDice.IV));
            cards.Add(new Card(CardClass.ActionShip, CardDice.V));
            cards.Add(new Card(CardClass.ActionShip, CardDice.VI));
        }


        cards.Add(new Card(CardClass.ActionCarpenter, CardDice.I));
        cards.Add(new Card(CardClass.ActionCarpenter, CardDice.II));
        cards.Add(new Card(CardClass.ActionCarpenter, CardDice.III));

        cards.Add(new Card(CardClass.ActionChurch, CardDice.I));
        cards.Add(new Card(CardClass.ActionChurch, CardDice.II));
        cards.Add(new Card(CardClass.ActionChurch, CardDice.III));

        cards.Add(new Card(CardClass.ActionMarket, CardDice.I));
        cards.Add(new Card(CardClass.ActionMarket, CardDice.II));
        cards.Add(new Card(CardClass.ActionMarket, CardDice.III));

        cards.Add(new Card(CardClass.ActionWatchtower, CardDice.I));
        cards.Add(new Card(CardClass.ActionWatchtower, CardDice.II));
        cards.Add(new Card(CardClass.ActionWatchtower, CardDice.III));

        cards.Add(new Card(CardClass.ActionCityHall, CardDice.IV));
        cards.Add(new Card(CardClass.ActionCityHall, CardDice.V));
        cards.Add(new Card(CardClass.ActionCityHall, CardDice.VI));

        cards.Add(new Card(CardClass.ActionWarehouse, CardDice.IV));
        cards.Add(new Card(CardClass.ActionWarehouse, CardDice.V));
        cards.Add(new Card(CardClass.ActionWarehouse, CardDice.VI));

        cards.Add(new Card(CardClass.ActionBoardinghouse, CardDice.IV));
        cards.Add(new Card(CardClass.ActionBoardinghouse, CardDice.V));
        cards.Add(new Card(CardClass.ActionBoardinghouse, CardDice.VI));

        cards.Add(new Card(CardClass.ActionBank, CardDice.IV));
        cards.Add(new Card(CardClass.ActionBank, CardDice.V));
        cards.Add(new Card(CardClass.ActionBank, CardDice.VI));

        return new Models.Deck(cards);
    }

}
