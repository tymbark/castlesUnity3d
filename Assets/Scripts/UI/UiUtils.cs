using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UiUtils {

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

    public static StaticCardsController GetStaticController(this GameObject obj) {
        StaticCardsController controller = obj.GetComponent<StaticCardsController>();
        return controller;
    }

}
