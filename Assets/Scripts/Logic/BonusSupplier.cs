using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GSP = GameStateProvider;

public static class BonusSupplier {

    public static void ApplyCompletingSingleBuildingBonus(this Player player,
                                                          Card card,
                                                          System.Action doneCallback) {
        GameState gameState = GSP.GameState;
        Deck goodsDeck = gameState.GoodsDeck;
        Deck animalsDeck = gameState.AnimalsDeck;

        switch (card.Class) {
            case CardClass.ActionCastle:
                player.BonusActionCards.Add(new Card(CardClass.BonusCastle, CardDice.All));
                break;
            case CardClass.ActionMine:
                player.SilverCount = player.SilverCount + 2;
                break;
            case CardClass.ActionCloister:
                break;
            case CardClass.ActionKnowledge:
                player.WorkersCount = player.WorkersCount + 2;
                break;
            case CardClass.ActionShip:
                PopupsController.ShowChooseGoodsPopup(1, () => {
                    GameController.UpdateView();
                    doneCallback();
                });
                break;
            case CardClass.ActionPasture:
                PopupsController.ShowChooseAnimalPopup(1, () => {
                    GameController.UpdateView();
                    doneCallback();
                });
                break;
            case CardClass.ActionCarpenter:
                player.BonusActionCards.Add(new Card(CardClass.BonusCarperter, CardDice.All));
                break;
            case CardClass.ActionChurch:
                player.BonusActionCards.Add(new Card(CardClass.BonusChurch, CardDice.All));
                break;
            case CardClass.ActionMarket:
                player.BonusActionCards.Add(new Card(CardClass.BonusMarket, CardDice.All));
                break;
            case CardClass.ActionWatchtower:
                player.Score = player.Score + 1;
                break;
            case CardClass.ActionBank:
                player.SilverCount = player.SilverCount + 3;
                break;
            case CardClass.ActionBoardinghouse:
                player.BonusActionCards.Add(new Card(CardClass.BonusBoardinghouse, CardDice.All));
                break;
            case CardClass.ActionWarehouse:
                player.BonusActionCards.Add(new Card(CardClass.BonusWarehouse, CardDice.All));
                break;
            case CardClass.ActionCityHall:
                player.BonusActionCards.Add(new Card(CardClass.BonusCityHall, CardDice.All));
                break;
        }

        switch (card.Class) {
            case CardClass.ActionCastle:
            case CardClass.ActionMine:
            case CardClass.ActionCloister:
            case CardClass.ActionCarpenter:
            case CardClass.ActionChurch:
            case CardClass.ActionMarket:
            case CardClass.ActionWatchtower:
            case CardClass.ActionBank:
            case CardClass.ActionBoardinghouse:
            case CardClass.ActionWarehouse:
            case CardClass.ActionCityHall:
            case CardClass.ActionKnowledge:
                doneCallback();
                break;
            case CardClass.ActionShip:
            case CardClass.ActionPasture:
                break;
        }
    }

    public static void ApplyCompletingTripleBonus(this Player player, List<Card> triple, GameState gameState) {
        if (triple.Count != 3) throw new System.InvalidProgramException("Triple must have 3 items!");


        if (triple[0].Class == triple[1].Class && triple[1].Class == triple[2].Class && triple[1].Class == CardClass.ActionCloister) {
            player.Score = player.Score + 6;
        }

        Card tripleCardRepresentative = triple.FindAll((Card obj) => obj.Class != CardClass.ActionCloister)[0];

        IncreaseScore(player, tripleCardRepresentative);
        ApplyFirstTripleOfAKindBonus(player, tripleCardRepresentative, gameState);

    }

    private static void IncreaseScore(Player player, Card tripleCardRepresentative) {
        if (tripleCardRepresentative.IsBuildingType()) {
            player.Score = player.Score + 3;
        } else {
            switch (tripleCardRepresentative.Class) {
                case CardClass.ActionCastle:
                    player.Score = player.Score + 2;
                    break;
                case CardClass.ActionMine:
                case CardClass.ActionKnowledge:
                case CardClass.ActionShip:
                case CardClass.ActionPasture:
                    player.Score = player.Score + 4;
                    break;
            }
        }
    }

    private static void ApplyFirstTripleOfAKindBonus(Player player, Card tripleCardRepresentative, GameState gameState) {
        if (tripleCardRepresentative.IsBuildingType()) {
            if (gameState.AvailableBonusCards.Contains(BonusCard.Building)) {
                gameState.AvailableBonusCards.Remove(BonusCard.Building);
                player.ReceivedBonuses.Add(BonusCard.Building);
                player.Score = player.Score + 1;
            }
        } else {
            switch (tripleCardRepresentative.Class) {
                case CardClass.ActionCastle:
                    if (gameState.AvailableBonusCards.Contains(BonusCard.Castle)) {
                        gameState.AvailableBonusCards.Remove(BonusCard.Castle);
                        player.ReceivedBonuses.Add(BonusCard.Castle);
                        player.Score = player.Score + 1;
                    }
                    break;

                case CardClass.ActionMine:
                    if (gameState.AvailableBonusCards.Contains(BonusCard.Mine)) {
                        gameState.AvailableBonusCards.Remove(BonusCard.Mine);
                        player.ReceivedBonuses.Add(BonusCard.Mine);
                        player.Score = player.Score + 1;
                    }
                    break;

                case CardClass.ActionKnowledge:
                    if (gameState.AvailableBonusCards.Contains(BonusCard.Knowledge)) {
                        gameState.AvailableBonusCards.Remove(BonusCard.Knowledge);
                        player.ReceivedBonuses.Add(BonusCard.Knowledge);
                        player.Score = player.Score + 1;
                    }
                    break;

                case CardClass.ActionShip:
                    if (gameState.AvailableBonusCards.Contains(BonusCard.Ship)) {
                        gameState.AvailableBonusCards.Remove(BonusCard.Ship);
                        player.ReceivedBonuses.Add(BonusCard.Ship);
                        player.Score = player.Score + 1;
                    }
                    break;

                case CardClass.ActionPasture:
                    if (gameState.AvailableBonusCards.Contains(BonusCard.Pasture)) {
                        gameState.AvailableBonusCards.Remove(BonusCard.Pasture);
                        player.ReceivedBonuses.Add(BonusCard.Pasture);
                        player.Score = player.Score + 1;
                    }
                    break;
            }
        }
    }

    public static void ApplyAllKindsSevenBonus(this Player player, GameState gameState) {
        bool hasCloister = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionCloister).IsNotEmpty();
        bool hasCastle = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionCastle).IsNotEmpty();
        bool hasMine = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionMine).IsNotEmpty();
        bool hasShip = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionShip).IsNotEmpty();
        bool hasKnowledge = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionKnowledge).IsNotEmpty();
        bool hasPasture = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionPasture).IsNotEmpty();
        bool hasBuilding = player.CompletedProjects.FindAll((Card obj) => obj.IsBuildingType()).IsNotEmpty();

        bool hasAllSeven = hasCloister && hasCastle && hasMine && hasShip && hasKnowledge && hasPasture && hasBuilding;

        if (hasAllSeven) {
            foreach (BonusCard bc in gameState.AvailableBonusCards) {
                switch (bc) {
                    case BonusCard.AllSeven4:
                        gameState.AvailableBonusCards.Remove(bc);
                        player.ReceivedBonuses.Add(bc);
                        player.Score = player.Score + 4;
                        return;
                    case BonusCard.AllSeven3:
                        gameState.AvailableBonusCards.Remove(bc);
                        player.ReceivedBonuses.Add(bc);
                        player.Score = player.Score + 3;
                        break;
                    case BonusCard.AllSeven2:
                        gameState.AvailableBonusCards.Remove(bc);
                        player.ReceivedBonuses.Add(bc);
                        player.Score = player.Score + 2;
                        break;
                    case BonusCard.AllSeven1:
                        gameState.AvailableBonusCards.Remove(bc);
                        player.ReceivedBonuses.Add(bc);
                        player.Score = player.Score + 1;
                        break;
                }
            }
        }
    }

}