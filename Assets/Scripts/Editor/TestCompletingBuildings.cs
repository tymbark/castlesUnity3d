using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Collections.Generic;
using Models;

public class TestCompletingBuildings {

    [Test]
    public void TestListMatches1() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCastle));
        cards.Add(new Card(CardClass.ActionCastle));

        var test = new Card(CardClass.ActionCastle);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches2() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCastle));
        cards.Add(new Card(CardClass.ActionCloister));

        var test = new Card(CardClass.ActionCastle);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches3() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCloister));
        cards.Add(new Card(CardClass.ActionCloister));

        var test = new Card(CardClass.ActionCastle);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches4() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCastle));
        cards.Add(new Card(CardClass.ActionCastle));

        var test = new Card(CardClass.ActionCloister);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches5() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCloister));
        cards.Add(new Card(CardClass.ActionCloister));

        var test = new Card(CardClass.ActionCloister);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches6() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionBank));
        cards.Add(new Card(CardClass.ActionChurch));

        var test = new Card(CardClass.ActionCarpenter);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches7() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCityHall));
        cards.Add(new Card(CardClass.ActionWarehouse));

        var test = new Card(CardClass.ActionWatchtower);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches8() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionBoardinghouse));
        cards.Add(new Card(CardClass.ActionCityHall));

        var test = new Card(CardClass.ActionWatchtower);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches9() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionBoardinghouse));
        cards.Add(new Card(CardClass.ActionCityHall));

        var test = new Card(CardClass.ActionCloister);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches10() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionPasture));
        cards.Add(new Card(CardClass.ActionCityHall));

        var test = new Card(CardClass.ActionCloister);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches11() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCloister));
        cards.Add(new Card(CardClass.ActionCloister));

        var test = new Card(CardClass.ActionCityHall);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListMatches12() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionPasture));
        cards.Add(new Card(CardClass.ActionCloister));

        var test = new Card(CardClass.ActionCloister);

        Assert.IsTrue(cards.MatchesCard(test));
    }

    [Test]
    public void TestListNotMatches1() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCastle));
        cards.Add(new Card(CardClass.ActionCastle));

        var test = new Card(CardClass.ActionCityHall);

        Assert.IsFalse(cards.MatchesCard(test));
    }

    [Test]
    public void TestListNotMatches2() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCastle));

        var test = new Card(CardClass.ActionMine);

        Assert.IsFalse(cards.MatchesCard(test));
    }

    [Test]
    public void TestListNotMatches3() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCloister));
        cards.Add(new Card(CardClass.ActionCastle));

        var test = new Card(CardClass.ActionMine);

        Assert.IsFalse(cards.MatchesCard(test));
    }

    [Test]
    public void TestListNotMatches4() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionBoardinghouse));
        cards.Add(new Card(CardClass.ActionWarehouse));

        var test = new Card(CardClass.ActionMine);

        Assert.IsFalse(cards.MatchesCard(test));
    }

    [Test]
    public void TestListNotMatches5() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionKnowledge));
        cards.Add(new Card(CardClass.ActionKnowledge));

        var test = new Card(CardClass.ActionMine);

        Assert.IsFalse(cards.MatchesCard(test));
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard1() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));


        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionBank));

        Assert.IsTrue(result.Count == 1);
        Assert.IsTrue(result[0][0].TripleId == 2);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard2() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionBank));

        Assert.IsTrue(result.Count == 0);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard3() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionBank));

        Assert.IsTrue(result.Count == 1);
        Assert.IsTrue(result[0][0].TripleId == 1);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard4() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionBank));

        Assert.IsTrue(result.Count == 1);
        Assert.IsTrue(result[0][0].TripleId == 1);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard5() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionMine, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionMine, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionCloister));

        Assert.IsTrue(result.Count == 1);
        Assert.IsTrue(result[0][0].TripleId == 3);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard6() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionBoardinghouse, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCityHall, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionCloister));

        Assert.IsTrue(result.Count == 2);
        Assert.IsTrue(result[0][0].TripleId == 3);
        Assert.IsTrue(result[1][0].TripleId == 5);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard7() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionBoardinghouse, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCityHall, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionBank));

        Assert.IsTrue(result.Count == 2);
        Assert.IsTrue(result[0][0].TripleId == 3);
        Assert.IsTrue(result[1][0].TripleId == 5);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard8() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 4));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 4));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionCastle));

        Assert.IsTrue(result.Count == 1);
        Assert.IsTrue(result[0][0].TripleId == 4);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard9() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionCastle));

        Assert.IsTrue(result.Count == 0);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard10() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionCastle));

        Assert.IsTrue(result.Count == 0);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard11() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 4));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 4));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionCastle));

        Assert.IsTrue(result.Count == 4);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard12() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 4));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 4));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionCloister));

        Assert.IsTrue(result.Count == 4);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard13() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 4));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 4));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionCastle));

        Assert.IsTrue(result.Count == 4);
    }

    [Test]
    public void TestGetNotCompletedTriplesForCard14() {
        Player player = RandomModels.RandomPlayer(12, 20, 30);
        var completedProjects = player.CompletedProjects;
        completedProjects.Clear();
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));

        var result = player.GetNotCompletedTriplesForCard(new Card(CardClass.ActionMine));

        Assert.AreEqual(0, result.Count);
    }

    [Test]
    public void TestMatchesCard1() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCastle));
        cards.Add(new Card(CardClass.ActionCastle));

        Assert.IsFalse(cards.MatchesCard(new Card(CardClass.ActionMine)));
    }

    [Test]
    public void TestMatchesCard2() {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(CardClass.ActionCastle));
        cards.Add(new Card(CardClass.ActionCastle));

        Assert.IsTrue(cards.MatchesCard(new Card(CardClass.ActionCastle)));
    }

}