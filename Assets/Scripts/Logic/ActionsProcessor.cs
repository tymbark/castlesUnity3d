using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GSP = GameStateProvider;

public static class ActionsProcessor {

    public static void ProcessAction(this Action action,
                                     System.Action doneCallback = null) {

        var cp = GSP.GameState.CurrentPlayer;
        var availableProjectCards = GSP.GameState.AvailableProjectCards;

        if (!GSP.GameState.GetAvailableActions().Has(action)) {
            throw new System.InvalidProgramException("Cannot use and action that is not in the available actions!");
        }

        if (doneCallback == null) {
            doneCallback = () => {
                0.print_("here");
            };
        }

        UnityEngine.Debug.Log(cp.NickName + " used " + action.Describe());

        switch (action.Type) {
            case ActionType.TakeProject:

                cp.WithdrawUsedCard(action.ActionCard);
                cp.UseWorkers(action.WorkersNeeded);
                cp.ProjectArea.Add(action.TargetCard);
                availableProjectCards.RemoveCardFromProjects(action.TargetCard);
                doneCallback();
                break;

            case ActionType.BuildProject:

                cp.WithdrawUsedCard(action.ActionCard);
                cp.UseWorkers(action.WorkersNeeded);


                cp.ApplyCompletingSingleBuildingBonus(action.TargetCard, () => {
                    UnityEngine.Debug.Log("single bonus done");

                    if (cp.GetNotCompletedTriplesForCard(action.TargetCard).IsEmpty()) {
                        UnityEngine.Debug.Log("there are no not completed triples for this card");

                        int newTripleId = cp.CompletedProjects.Count + 1;
                        bool didFinishTriple = GSP.GameState.CompleteProject(action.TargetCard, newTripleId);
                        GSP.GameState.CurrentPlayer.ApplyAllKindsSevenBonusIfPossible();

                        if (didFinishTriple) {
                            UnityEngine.Debug.Log("triple finished");
                            cp.ApplyCompletingTripleBonusPoints(newTripleId);

                            PopupsController.ShowChooseTripleBonusPopup(() => {
                                UnityEngine.Debug.Log("triple bonus done");
                                doneCallback();
                            });

                        } else {
                            UnityEngine.Debug.Log("triple not yet finished");
                            doneCallback();
                        }

                    } else {
                        UnityEngine.Debug.Log("there are some not completed triples. Show popup");
                        PopupsController.ShowChooseTripleStackPopup(action.TargetCard, (tripleId) => {

                            UnityEngine.Debug.Log("choose triple stack done");
                            bool didFinishTriple = GSP.GameState.CompleteProject(action.TargetCard, tripleId);

                            GSP.GameState.CurrentPlayer.ApplyAllKindsSevenBonusIfPossible();

                            if (didFinishTriple) {
                                UnityEngine.Debug.Log("triple finished");
                                cp.ApplyCompletingTripleBonusPoints(tripleId);

                                PopupsController.ShowChooseTripleBonusPopup(() => {
                                    UnityEngine.Debug.Log("triple bonus done");
                                    doneCallback();
                                });

                            } else {
                                UnityEngine.Debug.Log("triple not yet finished");
                                doneCallback();
                            }

                        });
                    }

                });


                break;

            case ActionType.ShipGoods:
                cp.WithdrawUsedCard(action.ActionCard);
                cp.UseWorkers(action.WorkersNeeded);
                cp.ShipGoods(action.TargetCard);
                cp.MoveFirstPlayerBonusCard();
                doneCallback();
                break;

            case ActionType.BuyWorkers:
                cp.WithdrawUsedCard(action.ActionCard);
                if (cp.WorkersCount < 2) {
                    cp.WorkersCount = 2;
                }
                doneCallback();
                break;

            case ActionType.BuySilver:
                cp.WithdrawUsedCard(action.ActionCard);
                cp.SilverCount += 1;
                doneCallback();
                break;

            case ActionType.SellSilverAndWorkers:
                cp.WithdrawUsedCard(action.ActionCard);
                cp.SilverCount -= action.SilverSell;
                cp.WorkersCount -= action.WorkersSell;
                cp.Score += (action.WorkersSell + action.SilverSell) / 3;
                doneCallback();
                break;

            case ActionType.UseSilver:
                cp.SilverCount = cp.SilverCount - 3;
                Utils.Repeat(3, () => { cp.BonusActionCards.Add(GSP.GameState.MainDeck.DrawCard()); });
                cp.SilverActionDoneThisTurn = true;
                doneCallback();
                break;

            case ActionType.TakeSilverProject:
                cp.ProjectArea.Add(action.ActionCard);
                cp.BonusActionUsed(action.ActionCard);
                doneCallback();
                break;

            case ActionType.EndTurn:
                GameEngine.ExecuteEndTurnAction();
                cp.SilverActionDoneThisTurn = false;
                doneCallback();
                break;

            default:
                throw new System.InvalidProgramException("Unknown action type: " + action.Describe());
        }

    }


}
