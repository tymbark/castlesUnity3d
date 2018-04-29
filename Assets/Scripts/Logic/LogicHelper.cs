using UnityEngine;
using System.Collections.Generic;
using Models;

public static class LogicHelper {

    public static int HowManyWorkersNeeded(CardDice dice1, CardDice dice2) {
        switch (Mathf.Abs(dice1.ToInt() - dice2.ToInt())) {
            case 0:
                return 0;
            case 1:
            case 5:
                return 1;
            case 2:
            case 4:
                return 2;
            case 3:
                return 3;
            default:
                throw new System.InvalidProgramException("Illegal value in difference between two dices!");
        }
    }

    public static int HowManyWorkersNeededToShip(CardDice dice, CardDice ship) {

        switch (ship) {
            case CardDice.I_II:

                if (dice == CardDice.I) return 0;
                if (dice == CardDice.II) return 0;
                if (dice == CardDice.III) return 1;
                if (dice == CardDice.IV) return 2;
                if (dice == CardDice.V) return 2;
                if (dice == CardDice.VI) return 1;

                break;
            case CardDice.III_IV:

                if (dice == CardDice.I) return 2;
                if (dice == CardDice.II) return 1;
                if (dice == CardDice.III) return 0;
                if (dice == CardDice.IV) return 0;
                if (dice == CardDice.V) return 1;
                if (dice == CardDice.VI) return 2;

                break;
            case CardDice.V_VI:

                if (dice == CardDice.I) return 1;
                if (dice == CardDice.II) return 2;
                if (dice == CardDice.III) return 2;
                if (dice == CardDice.IV) return 1;
                if (dice == CardDice.V) return 0;
                if (dice == CardDice.VI) return 0;

                break;
        }

        throw new System.InvalidProgramException("Card type shipment can with illegal dice! " + ship);
    }

    public static List<Action> SellStuffAllCombinations(Card actionCard, int silver, int workers) {

        List<Action> actions = new List<Action>();

        if (workers + silver < 3) return actions;

        for (int i = 0; i <= silver; i++) {
            for (int j = 0; j <= workers; j++) {

                if (i + j < 3) continue;

                if ((i + j) % 3 == 0) {
                    actions.Add(new Action(ActionType.SellSilverAndWorkers, actionCard,
                                           Card.Dummy, 0, i, j));
                }
            }
        }

        return actions;
    }

}
