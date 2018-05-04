using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Models {

    public class Action {
        public ActionType Type { get; private set; }
        public Card ActionCard { get; private set; }
        public Card TargetCard { get; private set; }
        public int WorkersNeeded { get; private set; }
        public int WorkersSell { get; private set; }
        public int SilverSell { get; private set; }

        public Action(ActionType actionType,
                      Card actionCard,
                      Card targetCard,
                      int workersNeeded = 0,
                      int silverSell = 0,
                      int workersSell = 0) {
            Type = actionType;
            ActionCard = actionCard;
            TargetCard = targetCard;
            WorkersNeeded = workersNeeded;
            WorkersSell = workersSell;
            SilverSell = silverSell;
        }

        public bool CompareTo(Action action) {
            return action.Type == Type
                             && action.ActionCard.IsEqualTo(ActionCard)
                             && action.TargetCard.IsEqualTo(TargetCard);
        }

    }

    public enum ActionType {
        TakeProject,
        TakeSilverProject,
        BuildProject,
        ShipGoods,
        BuyWorkers,
        BuySilver,
        UseSilver,
        SellSilverAndWorkers,
        EndTurn

        // todo bonus Actions from Castle
    }
}
