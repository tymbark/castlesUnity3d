using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Collections.Generic;
using NetworkModels;

public class TestNetworkModels {

    [Test]
    public void TestStringListEqual1() {
        List<string> l1 = new List<string>();
        List<string> l2 = new List<string>();

        Assert.IsTrue(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestStringListEqual2() {
        List<string> l1 = new List<string>();
        List<string> l2 = new List<string>();
        l1.Add("test");
        l2.Add("test");

        Assert.IsTrue(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestStringListEqual3() {
        List<string> l1 = new List<string>();
        List<string> l2 = new List<string>();

        for (int i = 0; i < 200; i++) {
            var item = "" + new System.Random().Next(200);
            l1.Add(item);
            l2.Add(item);
        }

        Assert.IsTrue(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestStringListNotEqual1() {
        List<string> l1 = new List<string>();
        List<string> l2 = new List<string>();

        for (int i = 0; i < 200; i++) {
            var item = "" + new System.Random().Next(200);
            l1.Add(item);
            l1.Add(item);
            l2.Add(item);
        }

        Assert.IsFalse(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestStringListNotEqual2() {
        List<string> l1 = new List<string>();
        List<string> l2 = new List<string>();

        for (int i = 0; i < 200; i++) {
            var item = "" + new System.Random().Next(200);
            l1.Add(item);
        }

        Assert.IsFalse(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestStringListNotEqual3() {
        List<string> l1 = new List<string>();
        List<string> l2 = new List<string>();

        var item = "" + new System.Random().Next(200);
        l1.Add(item);

        Assert.IsFalse(l1.IsEqualTo(l2));
    }

    [Test]
    public void TestParsingGameInfoEquals1() {
        GameInfo gi1 = new GameInfo(
            "" + new System.Random().Next(200),
            new System.Random().Next(10) > 4,
            "" + new System.Random().Next(200),
            new System.Random().Next(200),
            new System.Random().Next(200),
            RandomStringList(20)
        );

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoEquals2() {
        GameInfo gi1 = new GameInfo(
            "" + new System.Random().Next(0),
            new System.Random().Next(10) > 4,
            "" + new System.Random().Next(0),
            new System.Random().Next(0),
            new System.Random().Next(0),
            RandomStringList(0)
        );

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoEquals3() {
        GameInfo gi1 = new GameInfo(
            "" + new System.Random().Next(10),
            new System.Random().Next(10) > 4,
            "" + new System.Random().Next(0),
            new System.Random().Next(0),
            new System.Random().Next(0),
            RandomStringList(10)
        );

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoNotEquals1() {
        GameInfo gi1 = new GameInfo(
            "" + new System.Random().Next(10),
            new System.Random().Next(10) > 4,
            "" + new System.Random().Next(0),
            new System.Random().Next(0),
            new System.Random().Next(0),
            RandomStringList(10)
        );

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.Available = !gi1.Available;

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    [Test]
    public void TestParsingGameInfoNotEquals2() {
        GameInfo gi1 = new GameInfo(
            "" + new System.Random().Next(10),
            new System.Random().Next(10) > 4,
            "" + new System.Random().Next(20),
            new System.Random().Next(30),
            new System.Random().Next(40),
            RandomStringList(10)
        );

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.Id = "test";

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    [Test]
    public void TestParsingGameInfoNotEquals3() {
        GameInfo gi1 = new GameInfo(
            "" + new System.Random().Next(10),
            new System.Random().Next(10) > 4,
            "" + new System.Random().Next(20),
            new System.Random().Next(30),
            new System.Random().Next(40),
            RandomStringList(10)
        );

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.CreatorName = "test";

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    [Test]
    public void TestParsingGameInfoNotEquals4() {
        GameInfo gi1 = new GameInfo(
            "" + new System.Random().Next(10),
            new System.Random().Next(10) > 4,
            "" + new System.Random().Next(20),
            new System.Random().Next(30),
            new System.Random().Next(40),
            RandomStringList(10)
        );

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.PlayersMax = -20;

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    [Test]
    public void TestParsingGameInfoNotEquals5() {
        GameInfo gi1 = new GameInfo(
            "" + new System.Random().Next(10),
            new System.Random().Next(10) > 4,
            "" + new System.Random().Next(20),
            new System.Random().Next(30),
            new System.Random().Next(40),
            RandomStringList(10)
        );

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.PlayersNow = -20;

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    [Test]
    public void TestParsingGameInfoNotEquals6() {
        GameInfo gi1 = new GameInfo(
            "" + new System.Random().Next(10),
            new System.Random().Next(10) > 4,
            "" + new System.Random().Next(20),
            new System.Random().Next(30),
            new System.Random().Next(40),
            RandomStringList(10)
        );

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.PlayersIds.Clear();

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    private List<string> RandomStringList(int howMany) {
        var result = new List<string>();
        for (int i = 0; i < howMany; i++) {
            result.Add("" + new System.Random().Next(200));
        }
        return result;
    }

}
