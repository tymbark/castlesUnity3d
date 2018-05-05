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
            cards.Add(new Card(CardClass.ActionCastle, CardDice.I, i));
            cards.Add(new Card(CardClass.ActionCastle, CardDice.II, i));
            cards.Add(new Card(CardClass.ActionCastle, CardDice.III, i));
            cards.Add(new Card(CardClass.ActionCastle, CardDice.IV, i));
            cards.Add(new Card(CardClass.ActionCastle, CardDice.V, i));
            cards.Add(new Card(CardClass.ActionCastle, CardDice.VI, i));

            cards.Add(new Card(CardClass.ActionMine, CardDice.I, i));
            cards.Add(new Card(CardClass.ActionMine, CardDice.II, i));
            cards.Add(new Card(CardClass.ActionMine, CardDice.III, i));
            cards.Add(new Card(CardClass.ActionMine, CardDice.IV, i));
            cards.Add(new Card(CardClass.ActionMine, CardDice.V, i));
            cards.Add(new Card(CardClass.ActionMine, CardDice.VI, i));

            cards.Add(new Card(CardClass.ActionCloister, CardDice.I, i));
            cards.Add(new Card(CardClass.ActionCloister, CardDice.II, i));
            cards.Add(new Card(CardClass.ActionCloister, CardDice.III, i));
            cards.Add(new Card(CardClass.ActionCloister, CardDice.IV, i));
            cards.Add(new Card(CardClass.ActionCloister, CardDice.V, i));
            cards.Add(new Card(CardClass.ActionCloister, CardDice.VI, i));
        }


        for (int i = 0; i < 3; i++) {
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.I, i));
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.II, i));
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.III, i));
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.IV, i));
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.V, i));
            cards.Add(new Card(CardClass.ActionKnowledge, CardDice.VI, i));

            cards.Add(new Card(CardClass.ActionPasture, CardDice.I, i));
            cards.Add(new Card(CardClass.ActionPasture, CardDice.II, i));
            cards.Add(new Card(CardClass.ActionPasture, CardDice.III, i));
            cards.Add(new Card(CardClass.ActionPasture, CardDice.IV, i));
            cards.Add(new Card(CardClass.ActionPasture, CardDice.V, i));
            cards.Add(new Card(CardClass.ActionPasture, CardDice.VI, i));

            cards.Add(new Card(CardClass.ActionShip, CardDice.I, i));
            cards.Add(new Card(CardClass.ActionShip, CardDice.II, i));
            cards.Add(new Card(CardClass.ActionShip, CardDice.III, i));
            cards.Add(new Card(CardClass.ActionShip, CardDice.IV, i));
            cards.Add(new Card(CardClass.ActionShip, CardDice.V, i));
            cards.Add(new Card(CardClass.ActionShip, CardDice.VI, i));
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
