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
    public void TestBuildingsHaveCorrectSize0() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));

        List<List<Card>> result = completedProjects.CreateTripleLists();

        Assert.IsTrue(result.Count == 1);
    }

    [Test]
    public void TestBuildingsHaveCorrectSize1() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));

        List<List<Card>> result = completedProjects.CreateTripleLists();

        Assert.IsTrue(result.Count == 2);
    }

    [Test]
    public void TestBuildingsHaveCorrectSize2() {
        List<Card> completedProjects = new List<Card>();

        List<List<Card>> result = completedProjects.CreateTripleLists();

        Assert.IsTrue(result.Count == 0);
    }

    [Test]
    public void TestBuildingsHaveCorrectSize3() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 3));

        List<List<Card>> result = completedProjects.CreateTripleLists();

        Assert.IsTrue(result.Count == 3);
    }

    [Test]
    public void TestBuildingsHaveCorrectSize4() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 7));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 8));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 8));

        List<List<Card>> result = completedProjects.CreateTripleLists();

        Assert.IsTrue(result.Count == 5);
    }

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
    public void TestFindAvailableSpots1() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));


        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionBank));

        result.print_(result.Count + "");

        Assert.IsTrue(result.Count == 1);
        Assert.IsTrue(result[0][0].TripleId == 2);
    }

    [Test]
    public void TestFindAvailableSpots2() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionBank));

        Assert.IsTrue(result.Count == 0);
    }

    [Test]
    public void TestFindAvailableSpots3() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionBank));

        Assert.IsTrue(result.Count == 1);
        Assert.IsTrue(result[0][0].TripleId == 1);
    }

    [Test]
    public void TestFindAvailableSpots4() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 2));

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionBank));

        Assert.IsTrue(result.Count == 1);
        Assert.IsTrue(result[0][0].TripleId == 1);
    }

    [Test]
    public void TestFindAvailableSpots5() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionMine, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionMine, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionCloister));

        Assert.IsTrue(result.Count == 1);
        Assert.IsTrue(result[0][0].TripleId == 3);
    }

    [Test]
    public void TestFindAvailableSpots6() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionBoardinghouse, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCityHall, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionCloister));

        Assert.IsTrue(result.Count == 2);
        Assert.IsTrue(result[0][0].TripleId == 3);
        Assert.IsTrue(result[1][0].TripleId == 5);
    }

    [Test]
    public void TestFindAvailableSpots7() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionBoardinghouse, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCityHall, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));
        completedProjects.Add(new Card(CardClass.ActionBank, CardDice.O, 0, 5));

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionBank));

        Assert.IsTrue(result.Count == 2);
        Assert.IsTrue(result[0][0].TripleId == 3);
        Assert.IsTrue(result[1][0].TripleId == 5);
    }

    [Test]
    public void TestFindAvailableSpots8() {
        List<Card> completedProjects = new List<Card>();
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

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionCastle));

        Assert.IsTrue(result.Count == 1);
        Assert.IsTrue(result[0][0].TripleId == 4);
    }

    [Test]
    public void TestFindAvailableSpots9() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionCastle));

        Assert.IsTrue(result.Count == 0);
    }

    [Test]
    public void TestFindAvailableSpots10() {
        List<Card> completedProjects = new List<Card>();

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionCastle));

        Assert.IsTrue(result.Count == 0);
    }

    [Test]
    public void TestFindAvailableSpots11() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 4));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 4));

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionCastle));

        Assert.IsTrue(result.Count == 4);
    }

    [Test]
    public void TestFindAvailableSpots12() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 4));
        completedProjects.Add(new Card(CardClass.ActionCastle, CardDice.O, 0, 4));

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionCloister));

        Assert.IsTrue(result.Count == 4);
    }

    [Test]
    public void TestFindAvailableSpots13() {
        List<Card> completedProjects = new List<Card>();
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 1));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 2));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 3));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 4));
        completedProjects.Add(new Card(CardClass.ActionCloister, CardDice.O, 0, 4));

        var result = completedProjects.FindAvailableSpots(new Card(CardClass.ActionCastle));

        Assert.IsTrue(result.Count == 4);
    }
}