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
        l1.Add(new Item(42));
        l1.Add(new Item(3));
        l1.Add(new Item(9));
        l1.Add(new Item(23));
        l1.Add(new Item(11));

        List<Item> l2 = new List<Item>();
        l2.AddRange(l1);

        l2.Sort(
            delegate (Item i1, Item i2) {
                return i1.Value.CompareTo(i2.Value);
            }
        );

        foreach(Item i in l1) {
            UnityEngine.Debug.Log(i.Value);
        }

        foreach(Item i in l2) {
            UnityEngine.Debug.Log(i.Value);
        }

    }

}
