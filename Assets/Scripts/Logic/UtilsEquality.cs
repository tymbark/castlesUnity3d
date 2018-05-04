using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class UtilsEquality {

    public static bool IsEqualTo(this Card card1, Card card2) {
        return card1.Class == card2.Class && card1.Dice == card2.Dice;
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

    public static bool IsEqualTo(this Player p1, Player p2) {
        bool cardsEqual = p1.Cards.IsEqualTo(p2.Cards);
        bool futureCardsEqual = p1.FutureCards.IsEqualTo(p2.FutureCards);
        bool animalsEqual = p1.Animals.IsEqualTo(p2.Animals);
        bool goodsEqual = p1.Goods.IsEqualTo(p2.Goods);
        bool projectAreaEqual = p1.ProjectArea.IsEqualTo(p2.ProjectArea);
        bool silverActionCardsEqual = p1.SilverActionCards.IsEqualTo(p2.SilverActionCards);
        bool completedProjectsEqual = p1.CompletedProjects.IsEqualTo(p2.CompletedProjects);
        bool namesEqual = p1.Name == p2.Name;
        bool scoreEqual = p1.Score == p2.Score;
        bool workersEqual = p1.WorkersCount == p2.WorkersCount;
        bool silverEqual = p1.SilverCount == p2.SilverCount;
        bool silverDoneEqual = p1.SilverActionDoneThisTurn == p2.SilverActionDoneThisTurn;

        return cardsEqual && futureCardsEqual && animalsEqual && goodsEqual && projectAreaEqual
            && silverActionCardsEqual && completedProjectsEqual;
    }

    public static bool IsEqualTo(this GameState gs1, GameState gs2) {
        bool playersEqual = gs1.Players.IsEqualTo(gs2.Players);
        bool currentPlayerEqual = gs1.CurrentPlayerIndex == gs2.CurrentPlayerIndex;
        bool mainDeckEqual = gs1.MainDeck.Cards.IsEqualTo(gs2.MainDeck.Cards);
        bool animalsDeckEqual = gs1.AnimalsDeck.Cards.IsEqualTo(gs2.AnimalsDeck.Cards);
        bool goodsDeckEqual = gs1.GoodsDeck.Cards.IsEqualTo(gs2.GoodsDeck.Cards);
        bool availableProjectCardsEqual = gs1.AvailableProjectCards.IsEqualTo(gs2.AvailableProjectCards);
        bool noPlayersEqual = gs1.HowManyPlayers == gs2.HowManyPlayers;
        bool currentRoundEqual = gs1.CurrentRound == gs2.CurrentRound;

        return playersEqual && currentPlayerEqual && mainDeckEqual && animalsDeckEqual
            && goodsDeckEqual && availableProjectCardsEqual && noPlayersEqual
            && currentRoundEqual;
    }


}
