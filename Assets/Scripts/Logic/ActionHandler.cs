using System.Collections;
using Models;
using System.Collections.Generic;

public class ActionHandler {

    private readonly GameState GameState;
    private readonly List<ProjectCard> AvailableProjectCards;

    public ActionHandler(GameState gameState, List<ProjectCard> availableProjectCards) {
        AvailableProjectCards = availableProjectCards;
        GameState = gameState;
    }

    public void ProcessAction(Action action) {
        if (!GetAvailableActions().Has(action)) {
            throw new System.InvalidProgramException("Cannot use and action that is not in the available actions!");
        }

        UnityEngine.Debug.Log(CurrentPlayer().Name + " used " + action.Describe());

        switch (action.Type) {
            case ActionType.TakeProject:

                CurrentPlayer().ExecuteTakeProjectAction(action);
                RemoveCardFromProjects(action.TargetCard);

                break;
            case ActionType.BuildProject:

                CurrentPlayer().ExecuteBuildProjectAction(action);

                break;
            case ActionType.ShipGoods:

                CurrentPlayer().ExecuteShipGoodsAction(action);
                break;

            case ActionType.BuyWorkers:

                CurrentPlayer().ExecuteBuyWorkersAction(action);
                break;

            case ActionType.BuySilver:

                CurrentPlayer().ExecuteBuySilverAction(action);
                break;

            case ActionType.SellSilverAndWorkers:

                CurrentPlayer().ExecuteSellSilverAndWorkersAction(action);
                break;

            case ActionType.UseSilver:
                
                CurrentPlayer().ExecuteUseSilverAction(action, GameState.ActionsDeck);
                break;

            case ActionType.TakeSilverProject:

                CurrentPlayer().ExecuteTakeSilverProjectAction(action);
                break;

            case ActionType.EndTurn:
                
                CurrentPlayer().ExecuteEndTurnAction();
                GameState.NextTurn();
                break;

            default:
                throw new System.InvalidProgramException("Unknown action type: " + action.Describe());
        }
    }

    public void RemoveCardFromProjects(Card card) {
        var projectToRemove = AvailableProjectCards.Find((ProjectCard obj) => obj.Card.CompareTo(card));
        AvailableProjectCards.Remove(projectToRemove);

    }

    public List<Action> GetAvailableActions() {

        List<Action> availableActions = new List<Action>();

        if (CurrentPlayer().TakeProjectActionAvailable(AvailableProjectCards)) {
            availableActions.AddRange(CurrentPlayer().ReadyToTakeProjectActions(AvailableProjectCards));
        }

        if (CurrentPlayer().BuildProjectActionAvailable()) {
            availableActions.AddRange(CurrentPlayer().ReadyToBuildProjectActions());
        }

        if (CurrentPlayer().ShipGoodsActionAvailable()) {
            availableActions.AddRange(CurrentPlayer().ReadyToShipGoodsActions());
        }

        if (CurrentPlayer().BuyWorkersActionAvailable()) {
            availableActions.AddRange(CurrentPlayer().ReadyToBuyWorkersActions());
        }

        if (CurrentPlayer().BuySilverActionAvailable()) {
            availableActions.AddRange(CurrentPlayer().ReadyToBuySilverActions());
        }

        if (CurrentPlayer().SellWorkersAndSilverActionAvailable()) {
            availableActions.AddRange(CurrentPlayer().ReadyToSellSilverAndWorkersActions());
        }

        if (CurrentPlayer().UseSilverActionAvailable()) {
            availableActions.AddRange(CurrentPlayer().ReadyToUseSilverActions());
        }

        if (CurrentPlayer().TakeFreeSilverProjectActionsAvailable()) {
            availableActions.AddRange(CurrentPlayer().ReadyToTakeFreeSilverProjectActions());
        }

        if (CurrentPlayer().EndTurnActionAvailable()) {
            availableActions.Add(new Action(ActionType.EndTurn, Card.EndTurnCard(), Card.EndTurnCard()));
        }


        return availableActions;
    }
    private Player CurrentPlayer() {
        return GameState.CurrentPlayer;
    }
}
