using System.Collections;
using Models;
using System.Collections.Generic;

public static class ActionHandler {

    public static void RemoveCardFromProjects(this List<ProjectCard> availableProjectCards, Card card) {
        var cards = availableProjectCards.FindAll((ProjectCard obj) => obj.Card.IsEqualTo(card));
        if (cards.Count > 1) {
            throw new System.Exception("There cannot be two different targets for action. " +
                                       "Found " + cards.Count + " : " + cards[0].Stringify() +
                                       " " + cards[1].Stringify());
        }
        var projectToRemove = cards[0];
        availableProjectCards.Remove(projectToRemove);
    }

    public static List<Action> GetAvailableActions(this GameState gameState) {

        var availableProjectCards = gameState.AvailableProjectCards;
        var currentPlayer = gameState.CurrentPlayer;

        List<Action> availableActions = new List<Action>();

        if (currentPlayer.TakeProjectActionAvailable(availableProjectCards)) {
            availableActions.AddRange(currentPlayer.ReadyToTakeProjectActions(availableProjectCards));
        }

        if (currentPlayer.BuildProjectActionAvailable()) {
            availableActions.AddRange(currentPlayer.ReadyToBuildProjectActions());
        }

        if (currentPlayer.ShipGoodsActionAvailable()) {
            availableActions.AddRange(currentPlayer.ReadyToShipGoodsActions());
        }

        if (currentPlayer.BuyWorkersActionAvailable()) {
            availableActions.AddRange(currentPlayer.ReadyToBuyWorkersActions());
        }

        if (currentPlayer.BuySilverActionAvailable()) {
            availableActions.AddRange(currentPlayer.ReadyToBuySilverActions());
        }

        if (currentPlayer.SellWorkersAndSilverActionAvailable()) {
            availableActions.AddRange(currentPlayer.ReadyToSellSilverAndWorkersActions());
        }

        if (currentPlayer.UseSilverActionAvailable()) {
            availableActions.AddRange(currentPlayer.ReadyToUseSilverActions());
        }

        if (currentPlayer.TakeFreeSilverProjectActionsAvailable()) {
            availableActions.AddRange(currentPlayer.ReadyToTakeFreeSilverProjectActions());
        }

        if (currentPlayer.EndTurnActionAvailable()) {
            availableActions.Add(new Action(ActionType.EndTurn, StaticCards.DummyEndTurn, StaticCards.DummyEndTurn));
        }


        return availableActions;
    }
}
