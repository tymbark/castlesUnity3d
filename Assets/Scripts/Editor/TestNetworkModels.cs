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
    public void TestParsingGameInfoEquals0() {
        GameInfo gi1 = new GameInfo(
            "" ,
            new System.Random().Next(10) > 4,
            "" ,
            new System.Random().Next(5),
            new System.Random().Next(5),
            RandomStringList(5)
        );

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoEquals1() {
        GameInfo gi1 = RandomGameInfo(20, 20, 20, 20, 20);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoEquals2() {
        GameInfo gi1 = RandomGameInfo(20, 20, 20, 20, 0);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoEquals3() {
        GameInfo gi1 = RandomGameInfo(20, 20, 20, 0, 0);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoEquals4() {
        GameInfo gi1 = RandomGameInfo(20, 20, 0, 0, 0);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoEquals5() {
        GameInfo gi1 = RandomGameInfo(20, 0, 0, 0, 0);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoEquals6() {
        GameInfo gi1 = RandomGameInfo(0, 0, 0, 0, 0);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoEquals7() {
        GameInfo gi1 = RandomGameInfo(10, 100, 1000, 10000, 100000);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();

        Assert.IsTrue(gi1.IsEqualTo(gi1));
    }

    [Test]
    public void TestParsingGameInfoNotEquals1() {
        GameInfo gi1 = RandomGameInfo(20, 20, 20, 20, 20);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.Available = !gi1.Available;

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    [Test]
    public void TestParsingGameInfoNotEquals2() {
        GameInfo gi1 = RandomGameInfo(20, 30, 40, 50, 60);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.Id = "test";

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    [Test]
    public void TestParsingGameInfoNotEquals3() {
        GameInfo gi1 = RandomGameInfo(20, 0, 40, 50, 0);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.CreatorNickName = "test";

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    [Test]
    public void TestParsingGameInfoNotEquals4() {
        GameInfo gi1 = RandomGameInfo(20, 30, 40, 50, 60);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.PlayersMax = -20;

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    [Test]
    public void TestParsingGameInfoNotEquals5() {
        GameInfo gi1 = RandomGameInfo(0, 0, 0, 0, 0);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.PlayersNow = -20;

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    [Test]
    public void TestParsingGameInfoNotEquals6() {
        GameInfo gi1 = RandomGameInfo(20, 30, 0, 50, 60);

        GameInfo gi2 = gi1.ToJson().ParseToGameInfo();
        gi1.PlayersNicknames.Clear();

        Assert.IsFalse(gi1.IsEqualTo(gi2));
    }

    private GameInfo RandomGameInfo(int c1, int c2, int c3, int c4, int c5) {
        return new GameInfo(
            "" + new System.Random().Next(c1),
            new System.Random().Next(10) > 4,
            "" + new System.Random().Next(c2),
            new System.Random().Next(c3),
            new System.Random().Next(c4),
            RandomStringList(c5)
        );
    }

    private List<string> RandomStringList(int howMany) {
        var result = new List<string>();
        for (int i = 0; i < howMany; i++) {
            result.Add("" + new System.Random().Next(200));
        }
        return result;
    }

}
