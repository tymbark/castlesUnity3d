﻿using System.Collections;
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
                PopupsController.ShowChooseGoodsPopup(1, doneCallback);
                break;

            case CardClass.ActionPasture:
                PopupsController.ShowChooseAnimalPopup(1, doneCallback);
                break;

            case CardClass.ActionCarpenter:
                var carpenterAvilableBonuses = GSP.GameState.AvailableProjectCards
                    .FindAll((obj) => obj.Card.IsBuildingType() || obj.Card.Class == CardClass.ActionKnowledge)
                    .ConvertAll((ProjectCard input) => input.Card);

                if (carpenterAvilableBonuses.IsNotEmpty()) {
                    PopupsController.ShowTakeBuildingRewardCard(doneCallback, CardClass.ActionCarpenter);
                } else {
                    doneCallback();
                }
                break;

            case CardClass.ActionChurch:
                var churchAvilableBonuses = GSP.GameState.AvailableProjectCards
                    .ConvertAll((ProjectCard input) => input.Card)
                    .FindAll((obj) => obj.Class == CardClass.ActionCloister
                                   || obj.Class == CardClass.ActionCastle
                                   || obj.Class == CardClass.ActionMine);

                if (churchAvilableBonuses.IsNotEmpty()) {
                    PopupsController.ShowTakeBuildingRewardCard(doneCallback, CardClass.ActionChurch);
                } else {
                    doneCallback();
                }
                break;

            case CardClass.ActionMarket:
                var marketAvilableBonuses = GSP.GameState.AvailableProjectCards
                    .ConvertAll((ProjectCard input) => input.Card)
                    .FindAll((obj) => obj.Class == CardClass.ActionPasture
                                   || obj.Class == CardClass.ActionShip);

                if (marketAvilableBonuses.IsNotEmpty()) {
                    PopupsController.ShowTakeBuildingRewardCard(doneCallback, CardClass.ActionMarket);
                } else {
                    doneCallback();
                }
                break;

            case CardClass.ActionWatchtower:
                player.Score = player.Score + 1;
                break;
            case CardClass.ActionBank:
                player.SilverCount = player.SilverCount + 3;
                break;
            case CardClass.ActionBoardinghouse:
                var animalsOrGoodsExists = GSP.GameState.AnimalsDeck.Cards.IsNotEmpty()
                    || GSP.GameState.GoodsDeck.Cards.IsNotEmpty();

                if (animalsOrGoodsExists) {
                    PopupsController.ShowTakeGoodsOrAnimal(doneCallback);
                } else {
                    doneCallback();
                }

                break;
            case CardClass.ActionWarehouse:
                PopupsController.ShowChooseGoodsPopup(1, doneCallback);
                break;
            case CardClass.ActionCityHall:
                var cityHallAvilableBonuses = GSP.GameState.AvailableProjectCards
                    .ConvertAll((ProjectCard input) => input.Card);

                if (cityHallAvilableBonuses.IsNotEmpty()) {
                    PopupsController.ShowTakeBuildingRewardCard(doneCallback, CardClass.ActionCityHall);
                } else {
                    doneCallback();
                }
                break;
        }

        switch (card.Class) {
            case CardClass.ActionCastle:
            case CardClass.ActionMine:
            case CardClass.ActionCloister:
            case CardClass.ActionWatchtower:
            case CardClass.ActionBank:
            case CardClass.ActionKnowledge:
                doneCallback();
                break;
            case CardClass.ActionWarehouse:
            case CardClass.ActionBoardinghouse:
            case CardClass.ActionCarpenter:
            case CardClass.ActionChurch:
            case CardClass.ActionMarket:
            case CardClass.ActionCityHall:
            case CardClass.ActionShip:
            case CardClass.ActionPasture:
                break;
        }
    }

    public static void ApplyCompletingTripleBonusPoints(this Player player, int tripleId) {
        List<Card> triple = player.GetAllTriples().FindAll((obj) => obj[0].TripleId == tripleId)[0];

        if (triple.Count != 3) throw new System.InvalidProgramException("Triple must have 3 items!");


        Card tripleCardRepresentative = null;

        if (triple[0].Class == triple[1].Class && triple[1].Class == triple[2].Class && triple[1].Class == CardClass.ActionCloister) {
            tripleCardRepresentative = triple[0]; // its a cloister triple!
        } else {
            tripleCardRepresentative = triple.FindAll((Card obj) => obj.Class != CardClass.ActionCloister)[0];
        }


        player.IncreaseScore(tripleCardRepresentative);
        player.ApplyFirstTripleOfAKindBonusIfPossible(tripleCardRepresentative);

    }

    private static void IncreaseScore(this Player player, Card tripleCardRepresentative) {
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
                case CardClass.ActionCloister:
                    player.Score = player.Score + 6;
                    break;
            }
        }
    }

    private static void ApplyFirstTripleOfAKindBonusIfPossible(this Player player, Card tripleCardRepresentative) {
        var gameState = GSP.GameState;

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

    public static void ApplyAllKindsSevenBonusIfPossible(this Player player) {
        var gameState = GSP.GameState;
        bool hasCloister = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionCloister).IsNotEmpty();
        bool hasCastle = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionCastle).IsNotEmpty();
        bool hasMine = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionMine).IsNotEmpty();
        bool hasShip = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionShip).IsNotEmpty();
        bool hasKnowledge = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionKnowledge).IsNotEmpty();
        bool hasPasture = player.CompletedProjects.FindAll((Card obj) => obj.Class == CardClass.ActionPasture).IsNotEmpty();
        bool hasBuilding = player.CompletedProjects.FindAll((Card obj) => obj.IsBuildingType()).IsNotEmpty();

        bool hasAllSeven = hasCloister && hasCastle && hasMine && hasShip && hasKnowledge && hasPasture && hasBuilding;

        if (hasAllSeven && gameState.AvailableBonusCards.IsNotEmpty()) {

            List<BonusCard> availableBonusCards = gameState.AvailableBonusCards;

            if (availableBonusCards.FindAll((BonusCard obj) => obj == BonusCard.AllSeven4).Count == 1) {
                var bonusCard = availableBonusCards.FindAll((BonusCard obj) => obj == BonusCard.AllSeven4)[0];
                gameState.AvailableBonusCards.Remove(bonusCard);
                player.ReceivedBonuses.Add(bonusCard);
                player.Score += 4;

            } else if (availableBonusCards.FindAll((BonusCard obj) => obj == BonusCard.AllSeven3).Count == 1) {
                var bonusCard = availableBonusCards.FindAll((BonusCard obj) => obj == BonusCard.AllSeven3)[0];
                gameState.AvailableBonusCards.Remove(bonusCard);
                player.ReceivedBonuses.Add(bonusCard);
                player.Score += 3;

            } else if (availableBonusCards.FindAll((BonusCard obj) => obj == BonusCard.AllSeven2).Count == 1) {
                var bonusCard = availableBonusCards.FindAll((BonusCard obj) => obj == BonusCard.AllSeven2)[0];
                gameState.AvailableBonusCards.Remove(bonusCard);
                player.ReceivedBonuses.Add(bonusCard);
                player.Score += 2;

            } else if (availableBonusCards.FindAll((BonusCard obj) => obj == BonusCard.AllSeven1).Count == 1) {
                var bonusCard = availableBonusCards.FindAll((BonusCard obj) => obj == BonusCard.AllSeven1)[0];
                gameState.AvailableBonusCards.Remove(bonusCard);
                player.ReceivedBonuses.Add(bonusCard);
                player.Score += 1;
            }

        }
    }

    public static void ApplyBonusPointsForAnimals() {
        var players = GSP.GameState.Players;

        foreach (Player player in players) {
            int numberOfCows = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Cow).Count;
            int numberOfChickens = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Chicken).Count;
            int numberOfPigs = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Pig).Count;
            int numberOfSheep = player.Animals.FindAll((Card obj) => obj.Class == CardClass.Sheep).Count;

            while (true) {
                int uniqueAnimals = 0;

                if (numberOfCows > 0) {
                    uniqueAnimals++;
                    numberOfCows--;
                }

                if (numberOfChickens > 0) {
                    uniqueAnimals++;
                    numberOfChickens--;
                }

                if (numberOfPigs > 0) {
                    uniqueAnimals++;
                    numberOfPigs--;
                }

                if (numberOfSheep > 0) {
                    uniqueAnimals++;
                    numberOfSheep--;
                }

                switch (uniqueAnimals) {

                    case 4:
                        UnityEngine.Debug.Log("add 4 points");
                        player.Score = player.Score + 4;
                        break;
                    case 3:
                        UnityEngine.Debug.Log("add 2 points");
                        player.Score = player.Score + 2;
                        break;
                    case 2:
                        UnityEngine.Debug.Log("add 1 points");
                        player.Score = player.Score + 1;
                        break;
                    default:
                        return;

                }

            }

        }

    }

}