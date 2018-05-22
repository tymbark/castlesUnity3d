using System.Collections;
using Models;
using System.Collections.Generic;

public static class ActionHandler {

    public static void ProcessAction(this GameEngine gameEngine, Action action, System.Action chooseBonusCallback = null) {

        var gameState = gameEngine.GameState;
        var availableProjectCards = gameState.AvailableProjectCards;
        var currentPlayer = gameState.CurrentPlayer;

        if (!gameState.GetAvailableActions().Has(action)) {
            throw new System.InvalidProgramException("Cannot use and action that is not in the available actions!");
        }

        UnityEngine.Debug.Log(currentPlayer.NickName + " used " + action.Describe());

        switch (action.Type) {
            case ActionType.TakeProject:
                currentPlayer.ExecuteTakeProjectAction(action);
                availableProjectCards.RemoveCardFromProjects(action.TargetCard);
                break;

            case ActionType.BuildProject:
                bool didFinishedTriple = currentPlayer.ExecuteBuildProjectAction(action, gameState);
                if (didFinishedTriple && chooseBonusCallback != null) {
                    chooseBonusCallback();
                }
                break;

            case ActionType.ShipGoods:
                currentPlayer.ExecuteShipGoodsAction(action, gameState);
                break;

            case ActionType.BuyWorkers:
                currentPlayer.ExecuteBuyWorkersAction(action);
                break;

            case ActionType.BuySilver:
                currentPlayer.ExecuteBuySilverAction(action);
                break;

            case ActionType.SellSilverAndWorkers:
                currentPlayer.ExecuteSellSilverAndWorkersAction(action);
                break;

            case ActionType.UseSilver:
                currentPlayer.ExecuteUseSilverAction(action, gameState.MainDeck);
                break;

            case ActionType.TakeSilverProject:
                currentPlayer.ExecuteTakeSilverProjectAction(action);
                break;

            case ActionType.EndTurn:
                currentPlayer.ExecuteEndTurnAction();
                gameEngine.ExecuteEndTurnAction();
                break;

            default:
                throw new System.InvalidProgramException("Unknown action type: " + action.Describe());
        }
    }

    public static void RemoveCardFromProjects(this List<ProjectCard> availableProjectCards, Card card) {
        UnityEngine.Debug.Log(" action handler 2 " + availableProjectCards.Describe());
        var projectToRemove = availableProjectCards.Find((ProjectCard obj) => obj.Card.IsEqualTo(card));
        UnityEngine.Debug.Log("removing " + projectToRemove.Stringify());
        availableProjectCards.Remove(projectToRemove);
        UnityEngine.Debug.Log(" action handler 3 " + availableProjectCards.Describe());
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
