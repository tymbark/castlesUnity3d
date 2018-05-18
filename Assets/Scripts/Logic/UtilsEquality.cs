using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using NetworkModels;

public static class UtilsEquality {

    public static bool IsEqualTo(this Card card1, Card card2) {
        return card1.Class == card2.Class && card1.Dice == card2.Dice && card1.Number == card2.Number;
    }

    public static bool IsEqualTo(this ProjectCard card1, ProjectCard card2) {
        return card1.Card.IsEqualTo(card2.Card) && card1.TakeProjectDice == card2.TakeProjectDice;
    }

    public static bool IsEqualTo(this List<Card> cards1, List<Card> cards2) {
        if (cards1.Count != cards2.Count) {
            return false;
        }

        if (cards1 == null || cards2 == null) {
            throw new System.InvalidProgramException("Cannot compare nulls!");
        }

        for (int i = 0; i < cards1.Count; i++) {
            Card c1 = cards1[i];
            Card c2 = cards2[i];
            if (!c1.IsEqualTo(c2)) {
                return false;
            }
        }
        return true;
    }

    public static bool IsEqualTo(this List<ProjectCard> cards1, List<ProjectCard> cards2) {
        if (cards1.Count != cards2.Count) {
            return false;
        }

        if (cards1 == null || cards2 == null) {
            throw new System.InvalidProgramException("Cannot compare nulls!");
        }

        for (int i = 0; i < cards1.Count; i++) {
            ProjectCard c1 = cards1[i];
            ProjectCard c2 = cards2[i];
            if (!c1.IsEqualTo(c2)) {
                return false;
            }
        }
        return true;
    }

    public static bool IsEqualTo(this List<Player> players1, List<Player> players2) {
        if (players1.Count != players2.Count) {
            return false;
        }

        if (players1 == null || players2 == null) {
            throw new System.InvalidProgramException("Cannot compare nulls!");
        }

        for (int i = 0; i < players1.Count; i++) {
            Player p1 = players1[i];
            Player p2 = players2[i];
            if (!p1.IsEqualTo(p2)) {
                return false;
            }
        }
        return true;
    }

    public static bool IsEqualTo(this List<BonusCard> l1, List<BonusCard> l2) {
        if (l1 == null || l2 == null) {
            throw new System.InvalidProgramException("Cannot compare nulls!");
        }

        if (l1.Count != l2.Count) {
            return false;
        }

        for (int i = 0; i < l1.Count; i++) {
            BonusCard bc1 = l1[i];
            BonusCard bc2 = l2[i];
            if (bc1 != bc2) {
                return false;
            }
        }
        return true;
    }

    public static bool IsEqualTo(this List<string> l1, List<string> l2) {
        if (l1 == null || l2 == null) {
            throw new System.InvalidProgramException("Cannot compare nulls!");
        }

        if (l1.Count != l2.Count) {
            return false;
        }

        for (int i = 0; i < l1.Count; i++) {
            string val1 = l1[i];
            string val2 = l2[i];
            if (val1 != val2) {
                return false;
            }
        }
        return true;
    }

    public static bool IsEqualTo(this Player p1, Player p2) {
        List<bool> equality = new List<bool>();

        equality.Add(p1.Cards.IsEqualTo(p2.Cards));
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("1"));
        equality.Add(p1.FutureCards.IsEqualTo(p2.FutureCards));
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("2"));
        equality.Add(p1.Animals.IsEqualTo(p2.Animals));
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("3"));
        equality.Add(p1.Goods.IsEqualTo(p2.Goods));
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("4"));
        equality.Add(p1.ProjectArea.IsEqualTo(p2.ProjectArea));
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("5"));
        equality.Add(p1.BonusActionCards.IsEqualTo(p2.BonusActionCards));
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("6"));
        equality.Add(p1.CompletedProjects.IsEqualTo(p2.CompletedProjects));
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("7"));
        equality.Add(p1.Id == p2.Id);
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("8"));
        equality.Add(p1.Name == p2.Name);
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("9"));
        equality.Add(p1.Score == p2.Score);
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("10"));
        equality.Add(p1.WorkersCount == p2.WorkersCount);
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("11"));
        equality.Add(p1.SilverCount == p2.SilverCount);
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("12"));
        equality.Add(p1.SilverActionDoneThisTurn == p2.SilverActionDoneThisTurn);
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("13"));
        equality.Add(p1.ReceivedBonuses.IsEqualTo(p2.ReceivedBonuses));
        equality.FindAll((bool obj) => !obj).ForEach((bool obj) => obj.print_("14"));


        return equality.TrueForAll((bool obj) => obj);
    }

    public static bool IsEqualTo(this GameState gs1, GameState gs2) {
        List<bool> equality = new List<bool>();

        equality.Add(gs1.Id == gs2.Id);
        equality.Add(gs1.Players.IsEqualTo(gs2.Players));
        equality.Add(gs1.HowManyPlayers == gs2.HowManyPlayers);
        equality.Add(gs1.CurrentRound == gs2.CurrentRound);
        equality.Add(gs1.CurrentPlayerIndex == gs2.CurrentPlayerIndex);
        equality.Add(gs1.CurrentTurn == gs2.CurrentTurn);
        equality.Add(gs1.IsFinished == gs2.IsFinished);
        equality.Add(gs1.MainDeck.Cards.IsEqualTo(gs2.MainDeck.Cards));
        equality.Add(gs1.AnimalsDeck.Cards.IsEqualTo(gs2.AnimalsDeck.Cards));
        equality.Add(gs1.GoodsDeck.Cards.IsEqualTo(gs2.GoodsDeck.Cards));
        equality.Add(gs1.AvailableProjectCards.IsEqualTo(gs2.AvailableProjectCards));
        equality.Add(gs1.AvailableBonusCards.IsEqualTo(gs2.AvailableBonusCards));

        return equality.TrueForAll((bool obj) => obj);
    }

    public static bool IsEqualTo(this GameInfo gi1, GameInfo gi2) {
        List<bool> equality = new List<bool>();

        equality.Add(gi1.Id == gi2.Id);
        equality.Add(gi1.Available == gi2.Available);
        equality.Add(gi1.CreatorName == gi2.CreatorName);
        equality.Add(gi1.PlayersNow == gi2.PlayersNow);
        equality.Add(gi1.PlayersMax == gi2.PlayersMax);
        equality.Add(gi1.PlayersIds.IsEqualTo(gi2.PlayersIds));


        return equality.TrueForAll((bool obj) => obj);

    }

}
