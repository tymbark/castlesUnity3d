using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class ActionsFinder {

    public static List<Action> ReadyToBuildProjectActions(this Player p) {
        var actionProjectsCanBuild = new List<Action>();

        foreach (Card card in p.Cards.JoinWith(p.BonusActionCards.OnlySilverAndCastleBonuses())) {
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

        foreach (Card card in p.Cards.JoinWith(p.BonusActionCards.OnlySilverAndCastleBonuses())) {
            foreach (ProjectCard project in availableProjects) {

                int workersNeeded = LogicHelper.HowManyWorkersNeeded(card.Dice, project.TakeProjectDice);

                if (workersNeeded <= p.WorkersCount) {

                    var action = new Action(ActionType.TakeProject, card, project.Card, workersNeeded);
                    actionProjectsCanTake.Add(action);

                }
            }
        }

        return actionProjectsCanTake;
    }

    public static List<Action> ReadyToShipGoodsActions(this Player p) {
        var actionShipmentsReady = new List<Action>();

        foreach (Card card in p.Cards.JoinWith(p.BonusActionCards.OnlyShippableBonuses())) {
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

        foreach (Card card in p.Cards.JoinWith(p.BonusActionCards.OnlySilverAndCastleBonuses())) {
            actionsBuyWorkers.Add(new Action(ActionType.BuyWorkers, card, workerCard));    
        }

        return actionsBuyWorkers;
    }

    public static List<Action> ReadyToBuySilverActions(this Player p) {
        var actionsBuySilver = new List<Action>();
        var silverCard = new Card(CardClass.Silver, CardDice.O);

        foreach (Card card in p.Cards.JoinWith(p.BonusActionCards.OnlySilverAndCastleBonuses())) {
            actionsBuySilver.Add(new Action(ActionType.BuySilver, card, silverCard));
        }

        return actionsBuySilver;
    }

    public static List<Action> ReadyToSellSilverAndWorkersActions(this Player p) {
        var actionsSellSilverAndWorkers = new List<Action>();

        foreach (Card card in p.Cards.JoinWith(p.BonusActionCards.OnlySilverAndCastleBonuses())) {
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
