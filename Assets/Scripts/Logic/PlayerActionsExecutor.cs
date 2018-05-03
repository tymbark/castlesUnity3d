using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class PlayerActinosExecutor {

    public static void ExecuteTakeProjectAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.UseWorkers(action.WorkersNeeded);
        p.ProjectArea.Add(action.TargetCard);
    }

    public static void ExecuteBuildProjectAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.UseWorkers(action.WorkersNeeded);
        p.CompleteProject(action);
    }

    public static void ExecuteShipGoodsAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.UseWorkers(action.WorkersNeeded);
        p.ShipGoods(action.TargetCard);
    }

    public static void ExecuteBuySilverAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.WorkersCount = 2;
    }

    public static void ExecuteBuyWorkersAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.SilverCount += 3;
    }

    public static void ExecuteSellSilverAndWorkersAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.SilverCount -= action.SilverSell;
        p.WorkersCount -= action.WorkersSell;
        p.Score += (action.WorkersSell + action.SilverSell) / 3;
    }

    public static void ExecuteUseSilverAction(this Player p, Action action, Deck actionsDeck) {
        p.SilverCount -= 3;
        Utils.Repeat(3, () => { p.SilverActionCards.Add(actionsDeck.DrawCard()); });
    }

    public static void ExecuteTakeSilverProjectAction(this Player p, Action action) {
        p.SilverActionUsed();
        p.ProjectArea.Add(action.TargetCard);
    }

    public static void ExecuteEndTurnAction(this Player p) {
        p.SilverActionDoneThisTurn = false;
    }

    private static void WithdrawUsedCard(this Player p, Card card) {
        if (p.SilverActionCards.Contains(card)) {
            p.SilverActionUsed();
        } else if (p.Cards.Contains(card)) {
            p.Cards.Remove(card);
        } else {
            throw new System.InvalidOperationException(
                "Cannot use a card that player don't have! Tried to use "
                + Utils.Describe(card) + " but player only has " + p.Cards.Describe());
        }
    }

    private static void SilverActionUsed(this Player p) {
        p.SilverActionCards.Clear();
        p.SilverActionDoneThisTurn = true;
    }

    private static void UseWorkers(this Player p, int workersNeeded) {
        if (p.WorkersCount < workersNeeded) {
            throw new System.InvalidProgramException("Cannot use more workers than are available!");
        } else {
            p.WorkersCount -= workersNeeded;
        }
    }

    public static void CompleteProject(this Player p, Action action) {
        Debug.Log("dupa" + action.TripleCardsID);
        if (p.ProjectArea.Contains(action.TargetCard)) {
            p.ProjectArea.Remove(action.TargetCard);
            p.Estate.AddProjectToEstate(action.TargetCard, action.TripleCardsID);
        } else {
            throw new System.InvalidProgramException("Cannot build a project that is not in projects area!");
        }
    }

    public static void ShipGoods(this Player p, Card card) {
        var goodsToShip = p.Goods.FindAll((Card obj) => obj.Dice == card.Dice);

        foreach (Card good in goodsToShip) {
            p.Goods.Remove(card);
            p.SilverCount++;
            p.Score++;
        }

    }
}
