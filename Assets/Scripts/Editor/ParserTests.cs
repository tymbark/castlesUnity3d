using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Collections.Generic;
using Models;

public class ParserTests {

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

        Card c1 = new Card(CardClass.ActionCastle, CardDice.I);
        Card c2 = new Card(CardClass.ActionCastle, CardDice.II);

        Assert.IsFalse(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestParseCard1() {
        Card c1 = new Card(CardClass.ActionBank, CardDice.II);
        Card c2 = c1.Stringify().ParseToCard();

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestParseCard2() {
        Card c1 = new Card(CardClass.ActionBank, CardDice.III);
        Card c2 = c1.Stringify().ParseToCard();

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestParseCard3() {
        Card c1 = new Card(CardClass.ActionBank, CardDice.O);
        Card c2 = c1.Stringify().ParseToCard();

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestParseCard4() {
        Card c1 = new Card(CardClass.None, CardDice.I_II);
        Card c2 = c1.Stringify().ParseToCard();

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestParseCard5() {
        Card c1 = RandomCard();
        Card c2 = c1.Stringify().ParseToCard();

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestParseCard6() {
        Card c1 = new Card(CardClass.Silver, CardDice.II);
        Card c2 = c1.Stringify().ParseToCard();

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestParseCard7() {
        Card c1 = RandomCard();
        Card c2 = c1.Stringify().ParseToCard();

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestTwoNotEmptyCardListsAreEqual1() {
        List<Card> list1 = new List<Card>();
        List<Card> list2 = new List<Card>();
        for (int i = 0; i < 100; i++) {
            var card = RandomCard();
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
            var card = RandomCard();
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
            var card = RandomCard();
            list1.Add(card);
            list2.Add(card);
        }
        list2.Add(RandomCard());

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestTwoEmptyCardListsAreNotEqual1() {
        List<Card> list1 = new List<Card>();
        List<Card> list2 = new List<Card>();
        for (int i = 0; i < 100; i++) {
            var card = RandomCard();
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
    public void TestParsingListToString1() {
        List<Card> list1 = new List<Card>();

        for (int i = 0; i < 100; i++) {
            list1.Add(RandomCard());
        }

        List<Card> list2 = list1.Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToString2() {
        List<Card> list1 = new List<Card>();

        for (int i = 0; i < 1000; i++) {
            list1.Add(RandomCard());
        }

        List<Card> list2 = list1.Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToString3() {
        List<Card> list1 = new List<Card>();

        for (int i = 0; i < 10000; i++) {
            list1.Add(RandomCard());
        }

        List<Card> list2 = list1.Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingEmptyListToString1() {
        List<Card> list1 = new List<Card>();
        List<Card> list2 = list1.Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToStringNotEqual1() {
        List<Card> list1 = new List<Card>();


        for (int i = 0; i < 10000; i++) {
            list1.Add(RandomCard());
        }

        List<Card> list2 = list1.Stringify().ParseToCardsList();
        list1.Add(RandomCard());

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToStringNotEqual2() {
        List<Card> list1 = new List<Card>();


        for (int i = 0; i < 10000; i++) {
            list1.Add(RandomCard());
        }

        List<Card> list2 = list1.Stringify().ParseToCardsList();
        list1.Remove(list1[list1.Count - 1]);
        list1.Add(RandomCard());

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToStringNotEqual3() {
        List<Card> list1 = new List<Card>();


        for (int i = 0; i < 10000; i++) {
            var card = RandomCard();
            list1.Add(card);
        }

        List<Card> list2 = list1.Stringify().ParseToCardsList();
        list1.Add(RandomCard(5));
        list2.Add(RandomCard(2));

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestDoubleParsingListToStringEqual1() {
        List<Card> list1 = new List<Card>();


        for (int i = 0; i < 10000; i++) {
            list1.Add(RandomCard());
        }

        List<Card> list2 = list1.Stringify().ParseToCardsList().Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    private static Card RandomCard(int classNo = -1) {
        System.Random random = new System.Random();
        CardClass cc;
        if (classNo == -1) {
            cc = (CardClass)random.Next(46);
        } else {
            cc = (CardClass)classNo;
        }

        CardDice cd = (CardDice)random.Next(10);
        return new Card(cc, cd);
    }

}
