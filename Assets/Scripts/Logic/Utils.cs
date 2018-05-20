using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class Utils {

    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;

        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static string Describe(this List<Card> list) {
        string output = "";
        list.ForEach((Card obj) => output += obj.Class + " [" + obj.Dice + "], ");
        output = "{ " + output + " }";
        return output;
    }

    public static string Describe(this List<ProjectCard> list) {
        List<System.String> items = new List<System.String>();
        Repeat(6, () => items.Add("((" + (items.Count + 1) + ")): "));

        for (int i = 0; i < list.Count; i++) {
            ProjectCard obj = list[i];

            var pcString = obj.Card.Class + "[" + obj.Card.Dice + "], ";

            var row = getRowNumberByCardDice(obj.TakeProjectDice);
            items[row] = items[row] + pcString;
        }

        var output = "";

        items.ForEach((System.String obj) => { output += obj + "\n    "; });

        return output;
    }

    private static int getRowNumberByCardDice(CardDice cardDice) {
        switch (cardDice) {
            case CardDice.I:
                return 0;
            case CardDice.II:
                return 1;
            case CardDice.III:
                return 2;
            case CardDice.IV:
                return 3;
            case CardDice.V:
                return 4;
            case CardDice.VI:
                return 5;
            default:
                return -1;
        }

    }

    public static string Describe(this List<Action> list) {
        string output = "";

        for (int i = 0; i < list.Count; i++) {
            Action a = list[i];
            output += "    Key '" + KeyForAction(i) + "': " + a.Type + " " + Describe(a.TargetCard);

            if (a.Type == ActionType.UseSilver) {
                output += " (pay 3 s)";
            } else if (a.Type == ActionType.TakeSilverProject) {
                output += " (free)";
            } else {
                output += " using " + Describe(a.ActionCard);
            }

            if (a.WorkersNeeded > 0) {
                output += " (pay " + a.WorkersNeeded + " w)";
            }

            if (a.SilverSell > 0) {
                output += " (pay " + a.SilverSell + " s)";
            }

            if (a.WorkersSell > 0) {
                output += " (pay " + a.WorkersSell + " w)";
            }

            output += "\n";
        }

        return output;
    }

    public static string Describe(this Action action) {
        string output = "";

        output += action.Type + " " + Describe(action.TargetCard);

        if (action.Type != ActionType.EndTurn
            && action.Type != ActionType.TakeSilverProject
            && action.Type != ActionType.UseSilver) {
            output += " using " + Describe(action.ActionCard);
        }

        if (action.Type == ActionType.SellSilverAndWorkers) {
            output += " sold " + action.SilverSell + " silver and "
                        + action.WorkersSell + " workers, and got "
                        + (action.WorkersSell + action.SilverSell) / 3 + " Score";
        }

        return output + "\n";
    }

    public static string Describe(this Card card) {
        if (card.Class == CardClass.None) {
            return "";
        } else {
            return card.Class + " [" + card.Dice + "]";
        }
    }

    private static string KeyForAction(int no) {
        switch (no) {
            case 0:
                return "1";
            case 1:
                return "2";
            case 2:
                return "3";
            case 3:
                return "4";
            case 4:
                return "5";
            case 5:
                return "6";
            case 6:
                return "7";
            case 7:
                return "8";
            case 8:
                return "9";
            case 9:
                return "0";
            case 10:
                return "q";
            case 11:
                return "w";
            case 12:
                return "e";
            case 13:
                return "r";
            case 14:
                return "t";
            case 15:
                return "y";
            case 16:
                return "u";
            case 17:
                return "i";
            case 18:
                return "o";
            case 19:
                return "p";
            default:
                return "?";
        }
    }

    public static string GameStatusString(GameEngine gameEngine) {
        var statusText = "refresh game status:\n\n";

        statusText += "Actions Cards in Deck: \n" + gameEngine.GameState.MainDeck.Cards.Describe() + "\n\n";

        statusText += "Current player: " + gameEngine.GameState.CurrentPlayer.NickName + "\n\n";

        for (int i = 0; i < gameEngine.GameState.Players.Count; i++) {
            Player p = gameEngine.GameState.Players[i];

            statusText += p.NickName + "\n    ";
            statusText += "Cards: " + p.Cards.Describe();
            statusText += "\n    ";
            statusText += "Future Cards: " + p.FutureCards.Describe();
            statusText += "\n    ";
            statusText += "Projects: " + p.ProjectArea.Describe();
            statusText += "\n    ";

            if (p.BonusActionCards.Count > 0) {
                statusText += "Silver Action Cards:";
                statusText += p.BonusActionCards.Describe();
                statusText += "\n    ";
            }

            statusText += "Finished Buildings: " + p.CompletedProjects.Describe();
            statusText += "\n    ";
            statusText += "Animals: " + p.Animals.Describe();
            statusText += "\n    ";
            statusText += "Goods: " + p.Goods.Describe();
            statusText += "\n";
            statusText += "    Workers: " + p.WorkersCount;
            statusText += "    Silver: " + p.SilverCount;
            statusText += "    Score: " + p.Score;
            statusText += "\n";
            statusText += "\n";

        }

        statusText += "Available Projects\n    ";
        statusText += gameEngine.GameState.AvailableProjectCards.Describe();

        statusText += "\n\n";
        statusText += "Available Actions:\n";
        statusText += gameEngine.ActionHandler.GetAvailableActions().Describe();

        statusText += "\n";
        return statusText;
    }

    public static bool Has(this List<Action> actions, Action action) {

        foreach (Action a in actions) {
            if (a.IsEqualTo(action)) {
                return true;
            }
        }

        return false;
    }

    public static bool Has(this List<Card> cards, Card card) {

        foreach (Card c in cards) {
            if (c.IsEqualTo(card)) {
                return true;
            }
        }

        return false;
    }

    public static void Repeat(int repeatCount, System.Action action) {
        for (int i = 0; i < repeatCount; i++)
            action();
    }

    public static int ToInt(this CardDice cardDice) {
        switch (cardDice) {
            case CardDice.I:
                return 1;
            case CardDice.II:
                return 2;
            case CardDice.III:
                return 3;
            case CardDice.IV:
                return 4;
            case CardDice.V:
                return 5;
            case CardDice.VI:
                return 6;
            default:
                throw new System.InvalidProgramException("Cannot convert this enum " + cardDice + " to integer!");
        }
    }

    public static List<T> JoinWith<T>(this List<T> first, List<T> second) {
        List<T> result = new List<T>();

        result.AddRange(first);
        result.AddRange(second);

        return result;
    }

    public static bool IsMoveAvailable(this List<Action> actions, Card targetCard, Card actionCard) {
        switch (targetCard.Class) {

            case CardClass.Worker:
                return actions.HasBuyWorkersAction(actionCard);

            case CardClass.Silver:
                return actions.HasBuySilverAction(actionCard);

            case CardClass.AllStorages:
                return actions.HasShipGoodsAction(actionCard);

            case CardClass.SellSilverAndWorkers:
                return actions.HasSellSilverOrWorkersAction(actionCard);

            case CardClass.ShipGoods:
                return actions.HasShipGoodsAction(actionCard);

            case CardClass.Goods:
                return actions.HasTakeGoodsAction(actionCard);

            default:

                if (actions.HasBuildThisProjectAction(targetCard, actionCard)) {
                    return true;
                } else if (actions.HasTakeThisProjectAction(targetCard, actionCard)) {
                    return true;
                } else {
                    return false;
                }

        }
    }

    public static Action GetAvailableMove(this List<Action> actions, Card targetCard, Card actionCard) {
        switch (targetCard.Class) {

            case CardClass.Worker:
                return actions.GetBuyWorkersAction(actionCard);

            case CardClass.Silver:
                return actions.GetBuySilverAction(actionCard);

            case CardClass.AllStorages:
                return actions.GetShipGoodsAction(actionCard);

            case CardClass.SellSilverAndWorkers:
                return actions.GetSellSilverOrWorkersAction(actionCard);

            case CardClass.ShipGoods:
                return actions.GetShipGoodsAction(actionCard);

            default:
                if (actions.HasBuildThisProjectAction(targetCard, actionCard)) {
                    return actions.GetBuildThisProjectAction(targetCard, actionCard);
                } else if (actions.HasTakeThisProjectAction(targetCard, actionCard)) {
                    return actions.GetTakeThisProjectAction(targetCard, actionCard);
                } else {
                    throw new System.InvalidProgramException("Cannot get available move for " + Describe(targetCard));
                }

        }
    }

    public static Action GetBuildThisProjectAction(this List<Action> actions, Card targetCard, Card actionCard) {
        return actions.FindAll(
            (Action obj) => obj.Type == ActionType.BuildProject
            && obj.TargetCard.IsEqualTo(targetCard)
            && obj.ActionCard.IsEqualTo(actionCard))[0];
    }

    public static bool HasBuildThisProjectAction(this List<Action> actions, Card targetCard, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.BuildProject
            && obj.TargetCard.IsEqualTo(targetCard)
            && obj.ActionCard.IsEqualTo(actionCard)).Count == 1;
    }

    public static Action GetTakeThisProjectAction(this List<Action> actions, Card targetCard, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.TakeProject
            && obj.TargetCard.IsEqualTo(targetCard)
            && obj.ActionCard.IsEqualTo(actionCard))[0];
    }

    public static bool HasTakeThisProjectAction(this List<Action> actions, Card targetCard, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.TakeProject
            && obj.TargetCard.IsEqualTo(targetCard)
            && obj.ActionCard.IsEqualTo(actionCard)).Count == 1;
    }

    public static Action GetBuySilverAction(this List<Action> actions, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.BuySilver
            && obj.ActionCard.IsEqualTo(actionCard))[0];
    }

    public static bool HasBuySilverAction(this List<Action> actions, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.BuySilver
            && obj.ActionCard.IsEqualTo(actionCard)).Count == 1;
    }

    public static Action GetBuyWorkersAction(this List<Action> actions, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.BuyWorkers
            && obj.ActionCard.IsEqualTo(actionCard))[0];
    }

    public static bool HasBuyWorkersAction(this List<Action> actions, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.BuyWorkers
            && obj.ActionCard.IsEqualTo(actionCard)).Count == 1;
    }

    public static Action GetEndTurnAction(this List<Action> actions) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.EndTurn)[0];
    }

    public static bool HasEndTurnAction(this List<Action> actions) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.EndTurn).Count == 1;
    }

    public static bool HasBonusAction(this List<Action> actions) {

        foreach (Action a in actions) {
            switch (a.Type) {
                case ActionType.BonusCastle:
                case ActionType.BonusCarperter:
                case ActionType.BonusChurch:
                case ActionType.BonusMarket:
                case ActionType.BonusCityHall:
                case ActionType.BonusBoardinghouse:
                case ActionType.BonusWarehouse:
                    return true;
            }
        }

        return false;
    }

    public static Action GetSellSilverOrWorkersAction(this List<Action> actions, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.SellSilverAndWorkers
            && obj.ActionCard.IsEqualTo(actionCard))[0];
    }

    public static bool HasSellSilverOrWorkersAction(this List<Action> actions, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.SellSilverAndWorkers
            && obj.ActionCard.IsEqualTo(actionCard)).Count == 1;
    }

    public static Action GetShipGoodsAction(this List<Action> actions, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.ShipGoods
            && obj.ActionCard.IsEqualTo(actionCard))[0];
    }

    public static bool HasShipGoodsAction(this List<Action> actions, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.ShipGoods
            && obj.ActionCard.IsEqualTo(actionCard)).Count == 1;
    }

    //todo random goods
    public static bool HasTakeGoodsAction(this List<Action> actions, Card actionCard) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.TakeGoods
            && obj.ActionCard.IsEqualTo(actionCard)).Count == 1;
    }

    public static Action GetUseSilverAction(this List<Action> actions) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.UseSilver)[0];
    }

    public static bool HasUseSilverAction(this List<Action> actions) {
        return actions.FindAll((Action obj) => obj.Type == ActionType.UseSilver).Count == 1;
    }

    public static void print_(this object o, string message) {
        UnityEngine.Debug.Log(message);
    }

    public static bool IsActionType(this Card c) {
        switch (c.Class) {
            case CardClass.ActionCastle:
            case CardClass.ActionMine:
            case CardClass.ActionCloister:
            case CardClass.ActionKnowledge:
            case CardClass.ActionShip:
            case CardClass.ActionPasture:
            case CardClass.ActionCarpenter:
            case CardClass.ActionChurch:
            case CardClass.ActionMarket:
            case CardClass.ActionWatchtower:
            case CardClass.ActionBank:
            case CardClass.ActionBoardinghouse:
            case CardClass.ActionWarehouse:
            case CardClass.ActionCityHall:
                return true;
            default:
                return false;
        }
    }

    public static List<Card> OnlyShippableBonuses(this List<Card> cards) {
        return cards.FindAll((Card c) => c.IsActionType() || c.Class == CardClass.BonusCastle || c.Class == CardClass.BonusWarehouse);
    }

    public static List<Card> OnlyBuildAnyPojectBonuses(this List<Card> cards) {
        return cards.FindAll((Card c) => c.IsActionType() || c.Class == CardClass.BonusCastle || c.Class == CardClass.BonusCityHall);
    }

    public static List<Card> OnlySilverAndCastleBonuses(this List<Card> cards) {
        return cards.FindAll((Card c) => c.IsActionType() || c.Class == CardClass.BonusCastle);
    }

    public static List<Card> OnlySilverBonuses(this List<Card> cards) {
        return cards.FindAll((Card c) => c.IsActionType());
    }

    public static bool IsEmpty<T>(this List<T> list) {
        return list.Count == 0;
    }

    public static bool IsNotEmpty<T>(this List<T> list) {
        return list.Count > 0;
    }

}

