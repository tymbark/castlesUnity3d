using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class ActionsExecutor {

    //public static void ExecuteTakeProjectAction(this Player p, Action action) {
    //    p.WithdrawUsedCard(action.ActionCard);
    //    p.UseWorkers(action.WorkersNeeded);
    //    p.ProjectArea.Add(action.TargetCard);
    //}

    //public static bool ExecuteBuildProjectAction(this Player p, Action action, GameState gameState) {
    //    p.WithdrawUsedCard(action.ActionCard);
    //    p.UseWorkers(action.WorkersNeeded);
    //    p.ApplyCompletingSingleBuildingBonus(action.TargetCard, gameState);

    //    bool didFinishTriple = gameState.CompleteProject(action.TargetCard);

    //    return didFinishTriple;
    //}

    //public static void ExecuteShipGoodsAction(this Player p, Action action, GameState gameState) {
    //    p.WithdrawUsedCard(action.ActionCard);
    //    p.UseWorkers(action.WorkersNeeded);
    //    p.ShipGoods(action.TargetCard);
    //    p.MoveFirstPlayerBonusCard(gameState);
    //}

    //public static void ExecuteBuySilverAction(this Player p, Action action) {
    //    p.WithdrawUsedCard(action.ActionCard);
    //    p.SilverCount += 1;
    //}

    //public static void ExecuteBuyWorkersAction(this Player p, Action action) {
    //    p.WithdrawUsedCard(action.ActionCard);
    //    p.WorkersCount = 2;
    //}

    //public static void ExecuteSellSilverAndWorkersAction(this Player p, Action action) {
    //    p.WithdrawUsedCard(action.ActionCard);
    //    p.SilverCount -= action.SilverSell;
    //    p.WorkersCount -= action.WorkersSell;
    //    p.Score += (action.WorkersSell + action.SilverSell) / 3;
    //}

    //public static void ExecuteUseSilverAction(this Player p, Action action, Deck actionsDeck) {
    //    p.SilverCount = p.SilverCount - 3;
    //    Utils.Repeat(3, () => { p.BonusActionCards.Add(actionsDeck.DrawCard()); });
    //}

    //public static void ExecuteTakeSilverProjectAction(this Player p, Action action) {
    //    p.BonusActionUsed(action.ActionCard);
    //    p.ProjectArea.Add(action.TargetCard);
    //}

    //public static void ExecuteEndTurnAction(this Player p) {
    //    p.SilverActionDoneThisTurn = false;
    //}

    public static void WithdrawUsedCard(this Player p, Card card) {
        if (p.BonusActionCards.Contains(card)) {
            p.BonusActionUsed(card);
        } else if (p.Cards.Contains(card)) {
            p.Cards.Remove(card);
        } else {
            throw new System.InvalidOperationException(
                "Cannot use a card that player don't have! Tried to use "
                + card.Describe() + " but player only has " + p.Cards.Describe());
        }
    }

    public static void BonusActionUsed(this Player p, Card card) {
        if (!p.BonusActionCards.Contains(card)) {
            throw new System.InvalidOperationException(
                "Cannot use a bonus card that player don't have! Tried to use "
                + card.Describe() + " but player only has " + p.BonusActionCards.Describe());
        }

        if (card.IsActionType()) {
            p.BonusActionCards.RemoveAll((Card obj) => obj.IsActionType());
            p.SilverActionDoneThisTurn = true;
        } else {
            p.BonusActionCards.Remove(card);
        }
    }

    public static void UseWorkers(this Player p, int workersNeeded) {
        if (p.WorkersCount < workersNeeded) {
            throw new System.InvalidProgramException("Cannot use more workers than are available!");
        } else {
            p.WorkersCount = p.WorkersCount - workersNeeded;
        }
    }

    public static bool CompleteProject(this GameState gameState, Card newCard, int newTripleId) {
        if (gameState.CurrentPlayer.ProjectArea.Has(newCard)) {

            var projectToRemove = gameState.CurrentPlayer
                                           .ProjectArea
                                           .Find((Card obj) => obj.IsEqualTo(newCard));

            gameState.CurrentPlayer
                     .ProjectArea
                     .Remove(projectToRemove);

            gameState.CurrentPlayer
                     .CompletedProjects
                     .Add(newCard.WithTripleId(newTripleId));

            var recentlyTriple = gameState.CurrentPlayer
                                          .GetAllTriples()
                                          .FindAll((obj) => obj[0].TripleId == newTripleId);

            if (recentlyTriple.Count != 1) {
                throw new System.Exception("There are more than one triple list with id "
                                           + newTripleId + ". Found " + recentlyTriple.Count);
            }

            var applyBonus = recentlyTriple[0].Count == 3;

            return applyBonus;
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

    public static void MoveFirstPlayerBonusCard(this Player p, GameState gameState) {
        if (p.HasBonusFirstPlayerCard()) {
            return;
        } else {
            Player previousOwner = gameState.Players.Find((Player obj) => obj.HasBonusFirstPlayerCard());
            if (previousOwner.ReceivedBonuses.Contains(BonusCard.FirstPlayer)) {
                previousOwner.ReceivedBonuses.Remove(BonusCard.FirstPlayer);
            }
            if (previousOwner.ReceivedBonuses.Contains(BonusCard.FirstPlayerReverse)) {
                previousOwner.ReceivedBonuses.Remove(BonusCard.FirstPlayerReverse);
            }
            p.ReceivedBonuses.Add(BonusCard.FirstPlayerReverse);
        }
    }

    public static bool HasBonusFirstPlayerCard(this Player p) {
        return p.ReceivedBonuses.Contains(BonusCard.FirstPlayer) ||
                p.ReceivedBonuses.Contains(BonusCard.FirstPlayerReverse);
    }


}
