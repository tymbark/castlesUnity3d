using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class BonusSupplier {

    public static void ApplyCompletingBonus(this Player player, Card card, GameState gameState) {
        Deck goodsDeck = gameState.GoodsDeck;
        Deck animalsDeck = gameState.AnimalsDeck;

        switch (card.Class) {
            case CardClass.ActionCastle:
                player.Cards.Add(new Card(CardClass.BonusCastle, CardDice.All));
                break;
            case CardClass.ActionMine:
                player.SilverCount += 2;
                break;
            case CardClass.ActionCloister:
                break;
            case CardClass.ActionKnowledge:
                player.WorkersCount += 2;
                break;
            case CardClass.ActionShip:
                player.Goods.Add(goodsDeck.DrawCard());
                break;
            case CardClass.ActionPasture:
                player.Animals.Add(animalsDeck.DrawCard());
                break;
            case CardClass.ActionCarpenter:
                player.Cards.Add(new Card(CardClass.BonusCarperter, CardDice.All));
                break;
            case CardClass.ActionChurch:
                player.Cards.Add(new Card(CardClass.BonusChurch, CardDice.All));
                break;
            case CardClass.ActionMarket:
                player.Cards.Add(new Card(CardClass.BonusMarket, CardDice.All));
                break;
            case CardClass.ActionWatchtower:
                player.Score++;
                break;
            case CardClass.ActionBank:
                player.SilverCount += 3;
                break;
            case CardClass.ActionBoardinghouse:
                player.Cards.Add(new Card(CardClass.BonusBoardinghouse, CardDice.All));
                break;
            case CardClass.ActionWarehouse:
                player.Cards.Add(new Card(CardClass.BonusWarehouse, CardDice.All));
                break;
            case CardClass.ActionCityHall:
                player.Cards.Add(new Card(CardClass.BonusCityHall, CardDice.All));
                break;
        }
    }

}
