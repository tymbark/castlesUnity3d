using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GSP = GameStateProvider;

public static class ActionsExecutor {

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

            var recentlyIncreasedTriples = gameState.CurrentPlayer
                                          .GetAllTriples()
                                          .FindAll((obj) => obj[0].TripleId == newTripleId);

            if (recentlyIncreasedTriples.Count != 1) {
                throw new System.Exception("There are more than one triple list with id "
                                           + newTripleId + ". Found " + recentlyIncreasedTriples.Count);
            }

            var applyBonus = recentlyIncreasedTriples[0].Count == 3;

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

    public static void MoveFirstPlayerBonusCard(this Player p) {
        if (p.HasBonusFirstPlayerCard()) {
            return;
        } else {
            Player previousOwner = GSP.GameState.Players.Find((Player obj) => obj.HasBonusFirstPlayerCard());
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
