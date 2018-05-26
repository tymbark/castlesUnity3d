using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public static class PlayerExtensions {

    public static List<List<Card>> GetAllTriples(this Player player) {
        List<List<Card>> triples = new List<List<Card>>();

        int currentTripleId = -1;
        List<Card> currentList = null;

        var sortedProjects = new List<Card>();
        sortedProjects.AddRange(player.CompletedProjects);
        sortedProjects.Sort(
            delegate (Card c1, Card c2) {
                return c1.TripleId.CompareTo(c2.TripleId);
            });

        foreach (var item in sortedProjects) {

            if (currentTripleId == -1) {
                currentTripleId = item.TripleId;
                currentList = new List<Card>();
                currentList.Add(item);
                triples.Add(currentList);
                continue;
            }

            if (currentTripleId == item.TripleId) {
                currentList.Add(item);
            } else {
                currentTripleId = item.TripleId;
                currentList = new List<Card>();
                currentList.Add(item);
                triples.Add(currentList);
            }
        }

        return triples;
    }

    public static List<List<Card>> GetNotCompletedTriples(this Player player) {
        return player.GetAllTriples().FindAll((List<Card> obj) => obj.Count < 3);
    }

    public static List<List<Card>> GetNotCompletedTriplesForCard(this Player player, Card card) {
        var notCompletedTriples = player.GetNotCompletedTriples();
        var result = new List<List<Card>>();

        foreach (List<Card> list in notCompletedTriples) {
            if (list.MatchesCard(card)) {
                result.Add(list);
            }
        }

        return result;
    }

    public static bool MatchesCard(this List<Card> cards, Card test) {
        bool listMatches = true;

        foreach (Card c in cards) {

            bool theSameAction = test.Class == c.Class;
            bool bothBuildings = test.IsBuildingType() && c.IsBuildingType();
            bool cloisterType = test.Class == CardClass.ActionCloister || c.Class == CardClass.ActionCloister;

            if (!theSameAction && !bothBuildings && !cloisterType) {
                listMatches = false;
            }
        }

        return listMatches;
    }

    public static bool IsBuildingType(this Card card) {
        switch (card.Class) {
            case CardClass.ActionCarpenter:
            case CardClass.ActionChurch:
            case CardClass.ActionMarket:
            case CardClass.ActionWatchtower:
            case CardClass.ActionBank:
            case CardClass.ActionBoardinghouse:
            case CardClass.ActionWarehouse:
            case CardClass.ActionCityHall:
                return true;
        }
        return false;
    }

    public static Card WithTripleId(this Card card, int tripleId) {
        return new Card(card.Class, card.Dice, card.Number, tripleId);
    }

}