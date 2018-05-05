using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class ActionsFinder {

    public static List<Action> ReadyToBuildProjectActions(this Player p) {
        var actionProjectsCanBuild = new List<Action>();

        List<Card> bonusCards = p.BonusActionCards.OnlyBuildAnyPojectBonuses();
        List<Card> availableCards = p.NormalActionAvailable() ? p.Cards.JoinWith(bonusCards) : bonusCards;

        foreach (Card card in availableCards) {
            foreach (Card project in p.ProjectArea) {
                int workersNeeded = LogicHelper.HowManyWorkersNeeded(card.Dice, project.Dice);

                if (workersNeeded <= p.WorkersCount) {
                    Action item = new Action(ActionType.BuildProject, card, project, workersNeeded, 0, 0);
                    actionProjectsCanBuild.Add(item);
                }
            }
        }

        return actionProjectsCanBuild;
    }

    public static List<Action> ReadyToTakeProjectActions(this Player p, List<ProjectCard> availableProjects) {
        var actionProjectsCanTake = new List<Action>();

        List<Card> bonusCards = p.BonusActionCards.OnlySilverAndCastleBonuses();
        List<Card> availableCards = p.NormalActionAvailable() ? p.Cards.JoinWith(bonusCards) : bonusCards;

        foreach (ProjectCard project in availableProjects) {
            foreach (Card card in availableCards) {

                int workersNeeded = LogicHelper.HowManyWorkersNeeded(card.Dice, project.TakeProjectDice);

                if (workersNeeded <= p.WorkersCount) {

                    var action = new Action(ActionType.TakeProject, card, project.Card, workersNeeded);
                    actionProjectsCanTake.Add(action);

                }
            }
            switch (project.Card.Class) {
                case CardClass.ActionCastle:
                case CardClass.ActionMine:
                case CardClass.ActionCloister:
                    foreach (Card c in p.BonusActionCards.FindAll((Card c) => c.Class == CardClass.BonusChurch)) {
                        var action = new Action(ActionType.TakeProject, c, project.Card, 0);
                        actionProjectsCanTake.Add(action);
                    }
                    break;
                case CardClass.ActionShip:
                case CardClass.ActionPasture:
                    foreach (Card c in p.BonusActionCards.FindAll((Card c) => c.Class == CardClass.BonusMarket)) {
                        var action = new Action(ActionType.TakeProject, c, project.Card, 0);
                        actionProjectsCanTake.Add(action);
                    }
                    break;
                case CardClass.ActionKnowledge:
                case CardClass.ActionCarpenter:
                case CardClass.ActionChurch:
                case CardClass.ActionMarket:
                case CardClass.ActionWatchtower:
                case CardClass.ActionBank:
                case CardClass.ActionBoardinghouse:
                case CardClass.ActionWarehouse:
                case CardClass.ActionCityHall:
                    foreach (Card c in p.BonusActionCards.FindAll((Card c) => c.Class == CardClass.BonusCarperter)) {
                        var action = new Action(ActionType.TakeProject, c, project.Card, 0);
                        actionProjectsCanTake.Add(action);
                    }
                    break;

            }
        }

        return actionProjectsCanTake;
    }

    public static List<Action> ReadyToShipGoodsActions(this Player p) {
        var actionShipmentsReady = new List<Action>();

        List<Card> bonusCards = p.BonusActionCards.OnlyShippableBonuses();
        List<Card> availableCards = p.NormalActionAvailable() ? p.Cards.JoinWith(bonusCards) : bonusCards;

        foreach (Card card in availableCards) {
            foreach (Card goods in p.Goods) {

                int workersNeeded = LogicHelper.HowManyWorkersNeededToShip(card.Dice, goods.Dice);

                if (workersNeeded <= p.WorkersCount) {

                    var action = new Action(ActionType.ShipGoods, card, goods, workersNeeded);
                    actionShipmentsReady.Add(action);

                }
            }
        }

        return actionShipmentsReady;
    }

    public static List<Action> ReadyToTakeFreeSilverProjectActions(this Player p) {
        var silverFreeProjects = new List<Action>();

        foreach (Card card in p.BonusActionCards.OnlySilverBonuses()) {

            var action = new Action(ActionType.TakeSilverProject, Card.Dummy, card);
            silverFreeProjects.Add(action);

        }

        return silverFreeProjects;
    }

    public static List<Action> ReadyToBuyWorkersActions(this Player p) {
        var actionsBuyWorkers = new List<Action>();
        var workerCard = new Card(CardClass.Worker, CardDice.O);

        List<Card> bonusCards = p.BonusActionCards.OnlySilverAndCastleBonuses();
        List<Card> availableCards = p.NormalActionAvailable() ? p.Cards.JoinWith(bonusCards) : bonusCards;

        foreach (Card card in availableCards) {
            actionsBuyWorkers.Add(new Action(ActionType.BuyWorkers, card, workerCard));
        }

        return actionsBuyWorkers;
    }

    public static List<Action> ReadyToBuySilverActions(this Player p) {
        var actionsBuySilver = new List<Action>();
        var silverCard = new Card(CardClass.Silver, CardDice.O);

        List<Card> bonusCards = p.BonusActionCards.OnlySilverAndCastleBonuses();
        List<Card> availableCards = p.NormalActionAvailable() ? p.Cards.JoinWith(bonusCards) : bonusCards;

        foreach (Card card in availableCards) {
            actionsBuySilver.Add(new Action(ActionType.BuySilver, card, silverCard));
        }

        return actionsBuySilver;
    }

    public static List<Action> ReadyToSellSilverAndWorkersActions(this Player p) {
        var actionsSellSilverAndWorkers = new List<Action>();

        List<Card> bonusCards = p.BonusActionCards.OnlySilverAndCastleBonuses();
        List<Card> availableCards = p.NormalActionAvailable() ? p.Cards.JoinWith(bonusCards) : bonusCards;

        foreach (Card card in availableCards) {
            actionsSellSilverAndWorkers.AddRange(LogicHelper.SellStuffAllCombinations(card, p.SilverCount, p.WorkersCount));
        }

        return actionsSellSilverAndWorkers;
    }

    public static List<Action> ReadyToUseSilverActions(this Player p) {
        var actionsUseSilver = new List<Action>();

        actionsUseSilver.Add(new Action(ActionType.UseSilver, Card.Dummy, Card.Dummy));

        return actionsUseSilver;
    }

}
