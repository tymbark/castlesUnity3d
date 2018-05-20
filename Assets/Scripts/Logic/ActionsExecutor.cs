using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class ActionsExecutor {

    public static void ExecuteTakeProjectAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.UseWorkers(action.WorkersNeeded);
        p.ProjectArea.Add(action.TargetCard);
    }

    public static bool ExecuteBuildProjectAction(this Player p, Action action, GameState gameState) {
        p.WithdrawUsedCard(action.ActionCard);
        p.UseWorkers(action.WorkersNeeded);
        p.ApplyCompletingSingleBuildingBonus(action.TargetCard, gameState);

        bool didFinishTriple = p.CompleteProject(action.TargetCard, gameState);

        return didFinishTriple;
    }

    public static void ExecuteShipGoodsAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.UseWorkers(action.WorkersNeeded);
        p.ShipGoods(action.TargetCard);
    }

    public static void ExecuteBuySilverAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.SilverCount += 1;
    }

    public static void ExecuteBuyWorkersAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.WorkersCount = 2;
    }

    public static void ExecuteSellSilverAndWorkersAction(this Player p, Action action) {
        p.WithdrawUsedCard(action.ActionCard);
        p.SilverCount -= action.SilverSell;
        p.WorkersCount -= action.WorkersSell;
        p.Score += (action.WorkersSell + action.SilverSell) / 3;
    }

    public static void ExecuteUseSilverAction(this Player p, Action action, Deck actionsDeck) {
        p.SilverCount = p.SilverCount - 3;
        Utils.Repeat(3, () => { p.BonusActionCards.Add(actionsDeck.DrawCard()); });
    }

    public static void ExecuteTakeSilverProjectAction(this Player p, Action action) {
        p.BonusActionUsed(action.ActionCard);
        p.ProjectArea.Add(action.TargetCard);
    }

    public static void ExecuteEndTurnAction(this Player p) {
        p.SilverActionDoneThisTurn = false;
    }

    private static void WithdrawUsedCard(this Player p, Card card) {
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

    private static void BonusActionUsed(this Player p, Card card) {
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

    private static void UseWorkers(this Player p, int workersNeeded) {
        if (p.WorkersCount < workersNeeded) {
            throw new System.InvalidProgramException("Cannot use more workers than are available!");
        } else {
            p.WorkersCount = p.WorkersCount - workersNeeded;
        }
    }

    public static bool CompleteProject(this Player p, Card targetCard, GameState gameState) {
        bool didFinishedTriple = false;
        if (p.ProjectArea.Has(targetCard)) {

            var projectToRemove = p.ProjectArea.Find((Card obj) => obj.IsEqualTo(targetCard));
            p.ProjectArea.Remove(projectToRemove);

            List<List<Card>> spots = p.CompletedProjects.FindAvailableSpots(targetCard);

            Card cardWithTripleId;

            if (spots.Count == 0) {
                int newTripleId = p.CompletedProjects.Count + 1;
                cardWithTripleId = targetCard.WithTripleId(newTripleId);
            } else {
                List<Card> tripleCards = spots[0];
                cardWithTripleId = targetCard.WithTripleId(tripleCards[0].TripleId);

                tripleCards.Add(targetCard);

                if (tripleCards.Count == 3) {
                    p.ApplyCompletingTripleBonus(tripleCards, gameState);
                    didFinishedTriple = true;
                }
            }

            p.CompletedProjects.Add(cardWithTripleId);
            p.CompletedProjects.Sort((x, y) => x.TripleId.CompareTo(y.TripleId));

            return didFinishedTriple;
        } else {
            throw new System.InvalidProgramException("Cannot build a project that is not in projects area!");
        }
    }

    public static List<List<Card>> FindAvailableSpots(this List<Card> buildings, Card newCard) {
        List<List<Card>> triples = buildings.CreateTripleLists();

        List<List<Card>> result = new List<List<Card>>();

        foreach (List<Card> list in triples) {
            if (list.Count < 3) {
                if (list.MatchesCard(newCard)) {
                    result.Add(list);
                }
            }
        }

        return result;
    }

    public static bool MatchesCard(this List<Card> cards, Card test) {
        bool listMatches = true;

        foreach (Card c in cards) {

            bool theSameAction = test.Class == c.Class;
            bool bothBuildings = test.IsBuildingType() && c.IsBuildingType();
            bool cloisterType = test.Class == CardClass.ActionCloister || c.Class == CardClass.ActionCloister;

            if (!theSameAction && !bothBuildings && !cloisterType) {
                listMatches = false;
            }
        }

        return listMatches;
    }

    public static bool IsBuildingType(this Card card) {
        switch (card.Class) {
            case CardClass.ActionCarpenter:
            case CardClass.ActionChurch:
            case CardClass.ActionMarket:
            case CardClass.ActionWatchtower:
            case CardClass.ActionBank:
            case CardClass.ActionBoardinghouse:
            case CardClass.ActionWarehouse:
            case CardClass.ActionCityHall:
                return true;
        }
        return false;
    }

    public static List<List<Card>> CreateTripleLists(this List<Card> completedProjects) {

        List<List<Card>> buildings = new List<List<Card>>();
        List<Card> currentList = new List<Card>();
        int currentId = -1;

        foreach (Card c in completedProjects) {
            if (currentId != c.TripleId) {
                var newList = new List<Card>();
                newList.Add(c);
                buildings.Add(newList);
                currentList = newList;
                currentId = c.TripleId;
            } else {
                currentList.Add(c);
            }
        }

        return buildings;
    }

    private static Card WithTripleId(this Card card, int tripleId) {
        return new Card(card.Class, card.Dice, card.Number, tripleId);
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
