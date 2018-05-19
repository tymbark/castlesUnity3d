﻿using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Collections.Generic;
using Models;

public class TestDataParser {

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
    public void TestParsingListToString100() {
        List<Card> list1 = RandomList(100);

        List<Card> list2 = list1.Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToString1000() {
        List<Card> list1 = RandomList(1000);
        List<Card> list2 = list1.Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToString10000() {
        List<Card> list1 = RandomList(10000);
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
    public void TestParsingListToStringNotEqual500() {
        List<Card> list1 = RandomList(500);
        List<Card> list2 = list1.Stringify().ParseToCardsList();

        list1.Add(RandomCard());

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToStringNotEqual10000() {
        List<Card> list1 = RandomList(10000);
        List<Card> list2 = list1.Stringify().ParseToCardsList();

        list1.Remove(list1[list1.Count - 1]);
        list1.Add(RandomCard());

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToStringNotEqual1000() {
        List<Card> list1 = RandomList(1000);
        List<Card> list2 = list1.Stringify().ParseToCardsList();

        list1.Add(RandomCard(5));
        list2.Add(RandomCard(2));

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestDoubleParsingListToStringEqual1() {
        List<Card> list1 = RandomList(500);
        List<Card> list2 = list1.Stringify().ParseToCardsList().Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestEnumListEqual() {
        List<BonusCard> list1 = new List<BonusCard>();
        List<BonusCard> list2 = new List<BonusCard>();

        BonusCard b1 = RandomBonusCard();
        BonusCard b2 = RandomBonusCard();
        BonusCard b3 = RandomBonusCard();

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
        Player p1 = RandomPlayer(50, 500);

        Assert.IsTrue(p1.IsEqualTo(p1));
    }

    [Test]
    public void TestPlayerEquals2() {
        Player p1 = RandomPlayer(0, 50);

        Assert.IsTrue(p1.IsEqualTo(p1));
    }

    [Test]
    public void TestPlayerEquals3() {
        Player p1 = RandomPlayer(50, 0);

        Assert.IsTrue(p1.IsEqualTo(p1));
    }

    [Test]
    public void TestPlayerEquals4() {
        Player p1 = RandomPlayer(0, 0);

        Assert.IsTrue(p1.IsEqualTo(p1));
    }

    [Test]
    public void TestParsingPlayer1() {
        Player p1 = RandomPlayer(50, 50);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer2() {
        Player p1 = RandomPlayer(50, 0);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer3() {
        Player p1 = RandomPlayer(0, 0);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer4() {
        Player p1 = RandomPlayer(0, 0, 0);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer5() {
        Player p1 = RandomPlayer(50, 100, 0);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer6() {
        Player p1 = RandomPlayer(50, 100, 50);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer99Times() {
        for (int i = 0; i < 99; i++) {
            Player p1 = RandomPlayer(50, 100, 500);
            Player p2 = p1.Stringify().ParseToPlayer();

            Assert.IsTrue(p1.IsEqualTo(p2));
        }
    }

    [Test]
    public void TestDoubleParsingPlayer1() {
        Player p1 = RandomPlayer(10, 50);
        Player p2 = p1.Stringify().ParseToPlayer().Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestDoubleParsingPlayer2() {
        Player p1 = RandomPlayer(50, 0);
        Player p2 = p1.Stringify().ParseToPlayer().Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestDoubleParsingPlayer3() {
        Player p1 = RandomPlayer(0, 50);
        Player p2 = p1.Stringify().ParseToPlayer().Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestListPlayersEquals() {
        List<Player> l1 = new List<Player>();
        List<Player> l2 = new List<Player>();

        for (int i = 0; i < 50; i++) {
            var player = RandomPlayer(20, 30);
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
            l1.Add(RandomPlayer(20, 30));
            l2.Add(RandomPlayer(20, 30));
        }

        Assert.IsFalse(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestListPlayersNotEqual2() {
        List<Player> l1 = new List<Player>();
        List<Player> l2 = new List<Player>();

        for (int i = 0; i < 50; i++) {
            l1.Add(RandomPlayer(20, 30));
            l2.Add(RandomPlayer(20, 30));
        }
        l2.Add(RandomPlayer(20, 30));

        Assert.IsFalse(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestGameStateEqual1() {
        List<Player> players = new List<Player>();
        players.Add(RandomPlayer(23, 123));
        players.Add(RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RandomCard(), CardDice.IV));

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();

        Assert.IsTrue(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateEqual2() {
        List<Player> players = new List<Player>();
        players.Add(RandomPlayer(0, 123));
        players.Add(RandomPlayer(24, 0));
        List<ProjectCard> pc = new List<ProjectCard>();

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();

        Assert.IsTrue(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateEqual3() {
        List<Player> players = new List<Player>();
        List<ProjectCard> pc = new List<ProjectCard>();

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();

        Assert.IsTrue(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateEqual4() {
        List<Player> players = new List<Player>();
        List<ProjectCard> pc = new List<ProjectCard>();

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();

        Assert.IsTrue(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateEqual5() {
        List<Player> players = new List<Player>();
        List<ProjectCard> pc = new List<ProjectCard>();

        GameState gs1 = RandomGameState(players, pc, new List<BonusCard>());

        GameState gs2 = gs1.Stringify().ParseToGameState();

        Assert.IsTrue(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual1() {
        List<Player> players = new List<Player>();
        players.Add(RandomPlayer(23, 123));
        players.Add(RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RandomCard(), CardDice.IV));

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();

        if (gs2.CurrentRound == Round.A) {
            gs2.CurrentRound = Round.B;
        } else {
            gs2.CurrentRound = Round.A;

        }

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual2() {
        List<Player> players = new List<Player>();
        players.Add(RandomPlayer(23, 123));
        players.Add(RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RandomCard(), CardDice.IV));

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.Players.Clear();

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual3() {
        List<Player> players = new List<Player>();
        players.Add(RandomPlayer(23, 123));
        players.Add(RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RandomCard(), CardDice.IV));

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.AvailableProjectCards.Clear();

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual4() {
        List<Player> players = new List<Player>();
        players.Add(RandomPlayer(23, 123));
        players.Add(RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RandomCard(), CardDice.IV));

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.MainDeck.DrawCard();

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual5() {
        List<Player> players = new List<Player>();
        players.Add(RandomPlayer(23, 123));
        players.Add(RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RandomCard(), CardDice.IV));

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.IsFinished = !gs2.IsFinished;

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual6() {
        List<Player> players = new List<Player>();
        players.Add(RandomPlayer(23, 123));
        players.Add(RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RandomCard(), CardDice.IV));

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.CurrentPlayerNickName = "123";

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual7() {
        List<Player> players = new List<Player>();
        players.Add(RandomPlayer(23, 123));
        players.Add(RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RandomCard(), CardDice.IV));

        GameState gs1 = RandomGameState(players, pc, RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.AvailableBonusCards.Clear();

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    private static GameState RandomGameState(List<Player> players,
                                             List<ProjectCard> pc,
                                             List<BonusCard> bl) {
        return new GameState(
                    new System.Random().Next(9999) + "",
                    players,
                    new Deck(RandomList(10)),
                    new Deck(RandomList(20)),
                    new Deck(RandomList(30)),
                    pc,
                    bl,
                    (Round)new System.Random().Next((int)Round.E),
                    "" + new System.Random().Next(100),
                    new System.Random().Next(20),
                    new System.Random().Next(30),
                    new System.Random().Next(40) > 4
                );
    }

    [Test]
    public void TestGenericParserWithInts() {
        List<int> ints = new List<int>();
        ints.Add(1);
        ints.Add(2);
        ints.Add(3);

        List<int> newList = ints.Stringify().ParseToList<int>();
        0.print_(ints.Stringify());

        Assert.AreEqual(ints.Count, newList.Count);
        Assert.AreEqual(ints[0], newList[0]);
        Assert.AreEqual(ints[1], newList[1]);
        Assert.AreEqual(ints[2], newList[2]);
    }

    private enum Test {
        Foo, Bar, Xop
    }

    [Test]
    public void TestGenericParserWithEnum() {

        List<Test> list = new List<Test>();
        list.Add(Test.Foo);
        list.Add(Test.Bar);
        list.Add(Test.Xop);

        List<Test> newList = list.Stringify().ParseToList<Test>();

        Assert.AreEqual(list.Count, newList.Count);
        Assert.AreEqual(list[0], newList[0]);
        Assert.AreEqual(list[1], newList[1]);
        Assert.AreEqual(list[2], newList[2]);
    }

    public static Player RandomPlayer(int capacity1, int capacity2, int capacity3 = 200) {
        return new Player(
            RandomList(capacity1),
            RandomList(capacity2),
            RandomList(capacity1),
            RandomList(capacity2),
            RandomList(capacity1),
            RandomList(capacity2),
            RandomList(capacity1),
            RandomBonusList(capacity1),
            "" + new System.Random().Next(capacity3),
            new System.Random().Next(capacity1),
            new System.Random().Next(capacity2),
            new System.Random().Next(capacity1),
            false);
    }

    public static List<Card> RandomList(int capacity) {
        List<Card> result = new List<Card>();
        for (int i = 0; i < capacity; i++) {
            result.Add(RandomCard());
        }
        return result;
    }

    public static Card RandomCard(int classNo = -1) {
        System.Random random = new System.Random();
        CardClass cc;
        if (classNo == -1) {
            cc = (CardClass)random.Next((int)CardClass.ActionCityHall);
        } else {
            cc = (CardClass)classNo;
        }

        CardDice cd = (CardDice)random.Next(10);
        return new Card(cc, cd);
    }

    public static List<BonusCard> RandomBonusList(int capacity) {
        List<BonusCard> result = new List<BonusCard>();
        for (int i = 0; i < capacity; i++) {
            result.Add(RandomBonusCard());
        }
        return result;
    }

    public static BonusCard RandomBonusCard() {
        System.Random random = new System.Random();
        BonusCard bc = (BonusCard)random.Next((int)BonusCard.AllSeven1);
        return bc;
    }

}