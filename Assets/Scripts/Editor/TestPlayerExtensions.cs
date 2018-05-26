using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Collections.Generic;
using Models;

public class TestPlayerExtensions {

    [Test]
    public void TestFindingTripleCards1() {
        Player player = RandomModels.RandomPlayer(10, 20, 30);
        player.CompletedProjects.Clear();

        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));

        Assert.AreEqual(1, player.GetAllTriples().Count);
    }

    [Test]
    public void TestFindingTripleCards2() {
        Player player = RandomModels.RandomPlayer(10, 20, 30);
        player.CompletedProjects.Clear();

        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 2));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));

        Assert.AreEqual(3, player.GetAllTriples().Count);
    }

    [Test]
    public void TestFindingTripleCards3() {
        Player player = RandomModels.RandomPlayer(10, 20, 30);
        player.CompletedProjects.Clear();
        Assert.AreEqual(0, player.GetAllTriples().Count);
    }

    [Test]
    public void TestFindingTripleCards4() {
        Player player = RandomModels.RandomPlayer(10, 20, 30);
        player.CompletedProjects.Clear();

        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 2));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 2));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 2));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));

        Assert.AreEqual(3, player.GetAllTriples().Count);
    }

    [Test]
    public void TestFindingTripleCards5() {
        Player player = RandomModels.RandomPlayer(10, 20, 30);
        player.CompletedProjects.Clear();

        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 2));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 2));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 2));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));

        Assert.AreEqual(3, player.GetAllTriples().Count);
    }

    [Test]
    public void TestFindingNotCompletedTripleCards1() {
        Player player = RandomModels.RandomPlayer(10, 20, 30);
        player.CompletedProjects.Clear();

        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));

        Assert.AreEqual(2, player.GetNotCompletedTriples().Count);
    }

    [Test]
    public void TestFindingNotCompletedTripleCards2() {
        Player player = RandomModels.RandomPlayer(10, 20, 30);
        player.CompletedProjects.Clear();

        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 3));

        Assert.AreEqual(1, player.GetNotCompletedTriples().Count);
    }

    [Test]
    public void TestFindingNotCompletedTripleCards3() {
        Player player = RandomModels.RandomPlayer(10, 20, 30);
        player.CompletedProjects.Clear();

        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));
        player.CompletedProjects.Add(new Card(CardClass.ActionCastle, CardDice.I, 0, 1));

        Assert.AreEqual(0, player.GetNotCompletedTriples().Count);
    }

    [Test]
    public void TestFindingNotCompletedTripleCards4() {
        Player player = RandomModels.RandomPlayer(10, 20, 30);
        player.CompletedProjects.Clear();

        Assert.AreEqual(0, player.GetNotCompletedTriples().Count);
    }

}