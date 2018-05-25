using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
using Models;
using RM = RandomModels;

public class TestDataParser {

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
        Card c1 = RM.RandomCard();
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
        Card c1 = RM.RandomCard();
        Card c2 = c1.Stringify().ParseToCard();

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestParseCard8() {
        Card c1 = new Card(CardClass.ActionCastle, CardDice.II, 4, 7);
        Card c2 = c1.Stringify().ParseToCard();

        Assert.IsTrue(c1.IsEqualTo(c2));
    }

    [Test]
    public void TestParsingListToString100() {
        List<Card> list1 = RM.RandomList(100);

        List<Card> list2 = list1.Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToString1000() {
        List<Card> list1 = RM.RandomList(1000);
        List<Card> list2 = list1.Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToString10000() {
        List<Card> list1 = RM.RandomList(10000);
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
        List<Card> list1 = RM.RandomList(500);
        List<Card> list2 = list1.Stringify().ParseToCardsList();

        list1.Add(RM.RandomCard());

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingListToStringNotEqual1000() {
        List<Card> list1 = RM.RandomList(1000);
        List<Card> list2 = list1.Stringify().ParseToCardsList();

        list1.Add(RM.RandomCard(5));
        list2.Add(RM.RandomCard(2));

        Assert.IsFalse(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestDoubleParsingListToStringEqual1() {
        List<Card> list1 = RM.RandomList(500);
        List<Card> list2 = list1.Stringify().ParseToCardsList().Stringify().ParseToCardsList();

        Assert.IsTrue(list1.IsEqualTo(list2));
    }

    [Test]
    public void TestParsingPlayer1() {
        Player p1 = RM.RandomPlayer(50, 50);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer2() {
        Player p1 = RM.RandomPlayer(50, 0);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer3() {
        Player p1 = RM.RandomPlayer(0, 0);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer4() {
        Player p1 = RM.RandomPlayer(0, 0, 0);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer5() {
        Player p1 = RM.RandomPlayer(50, 100, 0);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer6() {
        Player p1 = RM.RandomPlayer(50, 100, 50);
        Player p2 = p1.Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestParsingPlayer99Times() {
        for (int i = 0; i < 99; i++) {
            Player p1 = RM.RandomPlayer(50, 100, 500);
            Player p2 = p1.Stringify().ParseToPlayer();

            Assert.IsTrue(p1.IsEqualTo(p2));
        }
    }

    [Test]
    public void TestDoubleParsingPlayer1() {
        Player p1 = RM.RandomPlayer(10, 50);
        Player p2 = p1.Stringify().ParseToPlayer().Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestDoubleParsingPlayer2() {
        Player p1 = RM.RandomPlayer(50, 0);
        Player p2 = p1.Stringify().ParseToPlayer().Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestDoubleParsingPlayer3() {
        Player p1 = RM.RandomPlayer(0, 50);
        Player p2 = p1.Stringify().ParseToPlayer().Stringify().ParseToPlayer();

        Assert.IsTrue(p1.IsEqualTo(p2));
    }

    [Test]
    public void TestProjectCardParse() {
        var card1 = new Card(CardClass.ActionBank, CardDice.I, 6, 8);

        ProjectCard pc1 = new ProjectCard(card1, CardDice.II);
        ProjectCard pc2 = pc1.Stringify().ParseToProjectCard();

        Assert.IsTrue(pc1.IsEqualTo(pc2));
    }

    [Test]
    public void TestGameStateEqual1() {
        List<Player> players = new List<Player>();
        players.Add(RM.RandomPlayer(23, 123));
        players.Add(RM.RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.IV));

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();

        Assert.IsTrue(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateEqual2() {
        List<Player> players = new List<Player>();
        players.Add(RM.RandomPlayer(0, 123));
        players.Add(RM.RandomPlayer(24, 0));
        List<ProjectCard> pc = new List<ProjectCard>();

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();

        Assert.IsTrue(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateEqual3() {
        List<Player> players = new List<Player>();
        List<ProjectCard> pc = new List<ProjectCard>();

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();

        Assert.IsTrue(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateEqual4() {
        List<Player> players = new List<Player>();
        List<ProjectCard> pc = new List<ProjectCard>();

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();

        Assert.IsTrue(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateEqual5() {
        List<Player> players = new List<Player>();
        List<ProjectCard> pc = new List<ProjectCard>();

        GameState gs1 = RM.RandomGameState(players, pc, new List<BonusCard>());

        GameState gs2 = gs1.Stringify().ParseToGameState();

        Assert.IsTrue(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual1() {
        List<Player> players = new List<Player>();
        players.Add(RM.RandomPlayer(23, 123));
        players.Add(RM.RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.IV));

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

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
        players.Add(RM.RandomPlayer(23, 123));
        players.Add(RM.RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.IV));

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.Players.Clear();

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual3() {
        List<Player> players = new List<Player>();
        players.Add(RM.RandomPlayer(23, 123));
        players.Add(RM.RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.IV));

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.AvailableProjectCards.Clear();

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual4() {
        List<Player> players = new List<Player>();
        players.Add(RM.RandomPlayer(23, 123));
        players.Add(RM.RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.IV));

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.MainDeck.DrawCard();

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual5() {
        List<Player> players = new List<Player>();
        players.Add(RM.RandomPlayer(23, 123));
        players.Add(RM.RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.IV));

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.IsFinished = !gs2.IsFinished;

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual6() {
        List<Player> players = new List<Player>();
        players.Add(RM.RandomPlayer(23, 123));
        players.Add(RM.RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.IV));

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.CurrentPlayerNickName = "123";

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGameStateNotEqual7() {
        List<Player> players = new List<Player>();
        players.Add(RM.RandomPlayer(23, 123));
        players.Add(RM.RandomPlayer(24, 92));
        List<ProjectCard> pc = new List<ProjectCard>();
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.I));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.II));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.III));
        pc.Add(new ProjectCard(RM.RandomCard(), CardDice.IV));

        GameState gs1 = RM.RandomGameState(players, pc, RM.RandomBonusList(20));

        GameState gs2 = gs1.Stringify().ParseToGameState();
        gs2.AvailableBonusCards.Clear();

        Assert.IsFalse(gs1.IsEqualTo(gs2));
    }

    [Test]
    public void TestGenericParserWithInts() {
        List<int> ints = new List<int>();
        ints.Add(1);
        ints.Add(2);
        ints.Add(3);

        List<int> newList = ints.Stringify().ParseToList<int>();

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

}
