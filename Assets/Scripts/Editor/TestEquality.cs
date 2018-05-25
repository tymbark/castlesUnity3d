using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Collections.Generic;
using Models;
using RM = RandomModels;

public class TestEquality {

    [Test]
    public void TestTwoActionsAreEqual1() {

        Card targetCard = RM.RandomCard();
        Card actionCard = RM.RandomCard();

        Models.Action a1 = new Models.Action(ActionType.BonusCarperter, actionCard, targetCard, 5, 6, 7);
        Models.Action a2 = new Models.Action(ActionType.BonusCarperter, actionCard, targetCard, 5, 6, 7);

        Assert.IsTrue(a1.IsEqualTo(a2));
    }

    [Test]
    public void TestTwoActionsAreEqual2() {

        Card targetCard = RM.RandomCard();
        Card actionCard = RM.RandomCard();

        Models.Action a1 = new Models.Action(ActionType.BonusCastle, actionCard, targetCard, 0, 0, 0);
        Models.Action a2 = new Models.Action(ActionType.BonusCastle, actionCard, targetCard, 0, 0, 0);

        Assert.IsTrue(a1.IsEqualTo(a2));
    }

    [Test]
    public void TestTwoActionsAreNotEqual1() {

        Card targetCard = RM.RandomCard(1);
        Card actionCard = RM.RandomCard(2);
        Card c1 = RM.RandomCard(3);

        Models.Action a1 = new Models.Action(ActionType.BonusCastle, actionCard, targetCard, 0, 0, 0);
        Models.Action a2 = new Models.Action(ActionType.BonusCastle, actionCard, c1, 0, 0, 0);

        Assert.IsFalse(a1.IsEqualTo(a2));
    }

    [Test]
    public void TestTwoActionsAreNotEqual2() {

        Card targetCard = RM.RandomCard();
        Card actionCard = RM.RandomCard();

        Models.Action a1 = new Models.Action(ActionType.BonusCastle, actionCard, targetCard, 0, 0, 0);
        Models.Action a2 = new Models.Action(ActionType.BonusCastle, actionCard, targetCard, 1, 0, 0);

        Assert.IsFalse(a1.IsEqualTo(a2));
    }

    [Test]
    public void TestTwoActionsAreNotEqual4() {

        Card targetCard = RM.RandomCard();
        Card actionCard = RM.RandomCard();

        Models.Action a1 = new Models.Action(ActionType.BonusCastle, actionCard, targetCard, 0, 0, 0);
        Models.Action a2 = new Models.Action(ActionType.BonusCastle, actionCard, targetCard, 0, 0, 1);

        Assert.IsFalse(a1.IsEqualTo(a2));
    }

    [Test]
    public void TestTwoActionsAreNotEqual3() {

        Card targetCard = RM.RandomCard();
        Card actionCard = RM.RandomCard();

        Models.Action a1 = new Models.Action(ActionType.BonusCastle, actionCard, targetCard, 0, 0, 0);
        Models.Action a2 = new Models.Action(ActionType.BonusCastle, actionCard, targetCard, 0, 1, 0);

        Assert.IsFalse(a1.IsEqualTo(a2));
    }

    [Test]
    public void TestTwoCardsAreEqual1() {

        Card c1 = new Card(CardClass.ActionBank, CardDice.II);
        Card c2 = new Card(CardClass.ActionBank, CardDice.II);

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestTwoCardsAreEqual2() {

        Card c1 = new Card(CardClass.ActionBank);
        Card c2 = new Card(CardClass.ActionBank);

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestTwoCardsAreNotEqual1() {

        Card c1 = new Card(CardClass.ActionBank);
        Card c2 = new Card(CardClass.ActionCastle);

        Assert.IsFalse(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestTwoCardsAreNotEqual2() {

        Card c1 = new Card(CardClass.ActionCastle, CardDice.I, 2);
        Card c2 = new Card(CardClass.ActionCastle, CardDice.I, 3);

        Assert.IsFalse(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestTwoCardsAreNotEqual3() {

        Card c1 = new Card(CardClass.ActionCastle, CardDice.I);
        Card c2 = new Card(CardClass.ActionCastle, CardDice.II);

        Assert.IsFalse(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestTwoNotEmptyCardListsAreEqual1() {
        List<Card> list1 = new List<Card>();
        List<Card> list2 = new List<Card>();
        for (int i = 0; i < 100; i++) {
            var card = RM.RandomCard();
            list1.Add(card);
            list2.Add(card);
        }

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestTwoNotEmptyCardListsAreEqual2() {
        List<Card> list1 = new List<Card>();
        List<Card> list2 = new List<Card>();
        for (int i = 0; i < 10000; i++) {
            var card = RM.RandomCard();
            list1.Add(card);
            list2.Add(card);
        }

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestTwoNotEmptyCardListsAreNotEqual1() {
        List<Card> list1 = new List<Card>();
        List<Card> list2 = new List<Card>();
        for (int i = 0; i < 100; i++) {
            var card = RM.RandomCard();
            list1.Add(card);
            list2.Add(card);
        }
        list2.Add(RM.RandomCard());

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestTwoEmptyCardListsAreNotEqual1() {
        List<Card> list1 = new List<Card>();
        List<Card> list2 = new List<Card>();
        for (int i = 0; i < 100; i++) {
            var card = RM.RandomCard();
            list1.Add(card);
        }

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestTwoEmptyCardListsAreEqual1() {
        List<Card> list1 = new List<Card>();
        List<Card> list2 = new List<Card>();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestEnumListEqual() {
        List<BonusCard> list1 = new List<BonusCard>();
        List<BonusCard> list2 = new List<BonusCard>();

        BonusCard b1 = RM.RandomBonusCard();
        BonusCard b2 = RM.RandomBonusCard();
        BonusCard b3 = RM.RandomBonusCard();

        list1.Add(b1);
        list1.Add(b2);
        list1.Add(b3);

        list2.Add(b1);
        list2.Add(b2);
        list2.Add(b3);

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestPlayerEquals1() {
        Player p1 = RM.RandomPlayer(50, 500);

        Assert.IsTrue(p1.IsEqualTo(p1));
    }

    [Test]
    public void TestPlayerEquals2() {
        Player p1 = RM.RandomPlayer(0, 50);

        Assert.IsTrue(p1.IsEqualTo(p1));
    }

    [Test]
    public void TestPlayerEquals3() {
        Player p1 = RM.RandomPlayer(50, 0);

        Assert.IsTrue(p1.IsEqualTo(p1));
    }

    [Test]
    public void TestPlayerEquals4() {
        Player p1 = RM.RandomPlayer(0, 0);

        Assert.IsTrue(p1.IsEqualTo(p1));
    }

    [Test]
    public void TestListPlayersEquals() {
        List<Player> l1 = new List<Player>();
        List<Player> l2 = new List<Player>();

        for (int i = 0; i < 50; i++) {
            var player = RM.RandomPlayer(20, 30);
            l1.Add(player);
            l2.Add(player);
        }

        Assert.IsTrue(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestListPlayersNotEqual1() {
        List<Player> l1 = new List<Player>();
        List<Player> l2 = new List<Player>();

        for (int i = 0; i < 50; i++) {
            l1.Add(RM.RandomPlayer(20, 30));
            l2.Add(RM.RandomPlayer(20, 30));
        }

        Assert.IsFalse(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestListPlayersNotEqual2() {
        List<Player> l1 = new List<Player>();
        List<Player> l2 = new List<Player>();

        for (int i = 0; i < 50; i++) {
            l1.Add(RM.RandomPlayer(20, 30));
            l2.Add(RM.RandomPlayer(20, 30));
        }
        l2.Add(RM.RandomPlayer(20, 30));

        Assert.IsFalse(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestTwoProjectCardsAreEqual1() {
        var card = RM.RandomCard();
        ProjectCard pc1 = new ProjectCard(card, CardDice.II);
        ProjectCard pc2 = new ProjectCard(card, CardDice.II);

        Assert.IsTrue(pc1.IsEqualTo(pc2));
    }

    [Test]
    public void TestTwoProjectCardsAreNotEqual1() {
        var card = RM.RandomCard();
        ProjectCard pc1 = new ProjectCard(card, CardDice.II);
        ProjectCard pc2 = new ProjectCard(card, CardDice.I);

        Assert.IsFalse(pc1.IsEqualTo(pc2));
    }

    [Test]
    public void TestTwoProjectCardsAreNotEqual2() {
        var card1 = RM.RandomCard(3);
        var card2 = RM.RandomCard(4);
        ProjectCard pc1 = new ProjectCard(card1, CardDice.II);
        ProjectCard pc2 = new ProjectCard(card2, CardDice.II);

        Assert.IsFalse(pc1.IsEqualTo(pc2));
    }

    [Test]
    public void TestTwoProjectCardsAreNotEqual3() {
        var card1 = new Card(CardClass.ActionBank, CardDice.I, 6, 8);
        var card2 = new Card(CardClass.ActionBank, CardDice.I, 7, 8);

        ProjectCard pc1 = new ProjectCard(card1, CardDice.II);
        ProjectCard pc2 = new ProjectCard(card2, CardDice.II);

        Assert.IsFalse(pc1.IsEqualTo(pc2));
    }

    [Test]
    public void TestTwoProjectCardsAreNotEqual4() {
        var card1 = new Card(CardClass.ActionBank, CardDice.I, 6, 8);
        var card2 = new Card(CardClass.ActionBank, CardDice.I, 6, 9);

        ProjectCard pc1 = new ProjectCard(card1, CardDice.II);
        ProjectCard pc2 = new ProjectCard(card2, CardDice.II);

        Assert.IsFalse(pc1.IsEqualTo(pc2));
    }

}
