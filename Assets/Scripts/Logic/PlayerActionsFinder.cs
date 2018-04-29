using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class PlayerActionsFinder {

    public static List<Action> ReadyToBuildProjectActions(this Player p) {
        var actionProjectsCanBuild = new List<Action>();

        foreach (Card card in p.Cards.JoinWith(p.SilverActionCards)) {
            foreach (Card project in p.ProjectArea) {

                int workersNeeded = LogicHelper.HowManyWorkersNeeded(card.Dice, project.Dice);

                if (workersNeeded <= p.WorkersCount) {

                    List<TripleCards> notFinishedTriples = p.Estate.NotFinishedTripes();

                    if (notFinishedTriples.Count == 0) {
                        actionProjectsCanBuild.Add(new Action(ActionType.BuildProject, card, project, workersNeeded, 0, 0));
                    } else {

                        foreach (TripleCards tc in notFinishedTriples) {
                            if (tc.Class() == project.Class) {
                                actionProjectsCanBuild.Add(new Action(ActionType.BuildProject, card, project, workersNeeded, 0, 0, tc.ID));
                            }

                            // todo use cloister as a wildcard
                            //else if (tc.Class() == CardClass.ActionCloister) {
                                //actionProjectsCanBuild.Add(new Action(ActionType.BuildProject, card, project, workersNeeded, 0, 0, tc.ID));
                            //}
                        }
                    }
                }
            }
        }

        return actionProjectsCanBuild;
    }

    public static List<Action> ReadyToTakeProjectActions(this Player p, List<ProjectCard> availableProjects) {
        var actionProjectsCanTake = new List<Action>();

        foreach (Card card in p.Cards.JoinWith(p.SilverActionCards)) {
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

        foreach (Card card in p.Cards.JoinWith(p.SilverActionCards)) {
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

        foreach (Card card in p.SilverActionCards) {

            var action = new Action(ActionType.TakeSilverProject, Card.Dummy, card);
            silverFreeProjects.Add(action);

        }

        return silverFreeProjects;
    }

    public static List<Action> ReadyToBuyWorkersActions(this Player p) {
        var actionsBuyWorkers = new List<Action>();

        var workerCard = new Card(CardType.Worker, CardClass.None, CardDice.O);
        actionsBuyWorkers.Add(new Action(ActionType.BuyWorkers, p.Cards[0], workerCard));
        actionsBuyWorkers.Add(new Action(ActionType.BuyWorkers, p.Cards[1], workerCard));

        if (p.SilverActionCards.Count > 0) {
            // todo consider for final version add all three silver cards maybe
            actionsBuyWorkers.Add(new Action(ActionType.BuyWorkers, p.SilverActionCards[0], workerCard));
        }

        return actionsBuyWorkers;
    }

    public static List<Action> ReadyToBuySilverActions(this Player p) {
        var actionsBuySilver = new List<Action>();

        var silverCard = new Card(CardType.Silver, CardClass.None, CardDice.O);
        actionsBuySilver.Add(new Action(ActionType.BuySilver, p.Cards[0], silverCard));
        actionsBuySilver.Add(new Action(ActionType.BuySilver, p.Cards[1], silverCard));

        if (p.SilverActionCards.Count > 0) {
            // todo consider for final version add all three silver cards maybe
            actionsBuySilver.Add(new Action(ActionType.BuySilver, p.SilverActionCards[0], silverCard));
        }

        return actionsBuySilver;
    }

    public static List<Action> ReadyToSellSilverAndWorkersActions(this Player p) {
        var actionsSellSilverAndWorkers = new List<Action>();

        actionsSellSilverAndWorkers.AddRange(LogicHelper.SellStuffAllCombinations(p.Cards[0], p.SilverCount, p.WorkersCount));
        actionsSellSilverAndWorkers.AddRange(LogicHelper.SellStuffAllCombinations(p.Cards[1], p.SilverCount, p.WorkersCount));

        if (p.SilverActionCards.Count > 0) {
            // todo consider for final version add all three silver cards maybe
            actionsSellSilverAndWorkers.AddRange(LogicHelper.SellStuffAllCombinations(p.SilverActionCards[0], p.SilverCount, p.WorkersCount));
        }

        return actionsSellSilverAndWorkers;
    }

    public static List<Action> ReadyToUseSilverActions(this Player p) {
        var actionsUseSilver = new List<Action>();

        actionsUseSilver.Add(new Action(ActionType.UseSilver, Card.Dummy, Card.Dummy));

        return actionsUseSilver;
    }

}
