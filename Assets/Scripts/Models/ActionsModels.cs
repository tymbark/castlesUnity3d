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

    }

    public enum ActionType {
        TakeProject,
        TakeSilverProject,
        BuildProject,
        ShipGoods,
        TakeGoods, //todo random goods
        BuyWorkers,
        BuySilver,
        UseSilver,
        SellSilverAndWorkers,
        EndTurn,

        BonusCastle,
        BonusCarperter,
        BonusChurch,
        BonusMarket,
        BonusCityHall,
        BonusBoardinghouse,
        BonusWarehouse

    }
}
