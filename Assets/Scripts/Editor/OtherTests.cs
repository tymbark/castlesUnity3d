using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Collections.Generic;
using Models;

public class OtherTests {


    class Item {
        public int Value;
        public Item(int val) {
            Value = val;
        }
    }


    [Test]
    public void TestSortingList() {

        List<Item> l1 = new List<Item>();
        l1.Add(new Item(5));
        l1.Add(new Item(3));
        l1.Add(new Item(2));
        l1.Add(new Item(1));
        l1.Add(new Item(4));

        List<Item> l2 = new List<Item>();
        l2.AddRange(l1);

        l2.Sort(
            delegate (Item i1, Item i2) {
                return i1.Value.CompareTo(i2.Value);
            }
        );

        Assert.AreEqual(0, l2.ConvertAll((input) => input.Value).IndexOf(1));
        Assert.AreEqual(1, l2.ConvertAll((input) => input.Value).IndexOf(2));
        Assert.AreEqual(2, l2.ConvertAll((input) => input.Value).IndexOf(3));
        Assert.AreEqual(3, l2.ConvertAll((input) => input.Value).IndexOf(4));
        Assert.AreEqual(4, l2.ConvertAll((input) => input.Value).IndexOf(5));

        Assert.AreEqual(0, l1.ConvertAll((input) => input.Value).IndexOf(5));
        Assert.AreEqual(1, l1.ConvertAll((input) => input.Value).IndexOf(3));
        Assert.AreEqual(2, l1.ConvertAll((input) => input.Value).IndexOf(2));
        Assert.AreEqual(3, l1.ConvertAll((input) => input.Value).IndexOf(1));
        Assert.AreEqual(4, l1.ConvertAll((input) => input.Value).IndexOf(4));

    }

}
