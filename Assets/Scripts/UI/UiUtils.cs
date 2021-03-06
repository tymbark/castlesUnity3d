﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UiUtils {

    public enum ClickAction {
        Options, Exit, NextTurn,
        AllProjects, AllEstates, AllAnimals, AllStorages, AllBonuses,
        UseSilver, SellSilverAndWorkers
    }

    public static void ResetColor(this GameObject obj) {
        Image image = obj.GetComponent<Image>();
        image.color = new Color(1, 1, 1, 1);
    }

    public static void SetBlueColor(this GameObject obj) {
        Image image = obj.GetComponent<Image>();
        image.color = new Color(0.5f, 0.5f, 1, 1);
    }

    public static void SetRedColor(this GameObject obj) {
        Image image = obj.GetComponent<Image>();
        image.color = new Color(1f, 0.5f, 0.5f, 1);
    }

    public static DropCardController GetDragDropController(this GameObject obj) {
        DropCardController controller = obj.GetComponent<DropCardController>();
        return controller;
    }

    public static CardController GetCardController(this GameObject obj) {
        CardController controller = obj.GetComponent<CardController>();
        return controller;
    }

}
