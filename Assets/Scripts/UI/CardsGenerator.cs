using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.UI;
using D = GameDimensions;

public static class CardsGenerator {

    public static GameObject DrawClickableButtonCard(Card c, Vector2 coords) {
        return DrawCard(c, coords.x, coords.y, D.CardWidth, D.CardWidth, false, false, false, true);
    }

    public static GameObject DrawExecutableCard(Card c, Vector2 coords) {
        return DrawCard(c, coords.x, coords.y, D.CardWidth, D.CardHeight, false, false, true, false);
    }

    public static GameObject DrawStaticCard(Card c, Vector2 coords) {
        return DrawCard(c, coords.x, coords.y, D.CardWidth, D.CardHeight, false, false, false, false);
    }

    public static GameObject DrawClickableHorizontalCard(Card c, Vector2 coords) {
        return DrawCard(c, coords.x, coords.y, D.CardWidth, D.CardHeight, true, false, false, false);
    }

    public static GameObject DrawClickableAndExecutableCard(Card c, Vector2 coords) {
        return DrawCard(c, coords.x, coords.y, D.CardWidth, D.CardHeight, false, false, true, false);
    }

    public static GameObject DrawClickableCard(Card c, Vector2 coords) {
        return DrawCard(c, coords.x, coords.y, D.CardWidth, D.CardHeight, false, false, false, true);
    }

    public static GameObject DrawBigBackgroundCard(Card c, Vector2 coords) {
        return DrawCard(c, coords.x, coords.y, D.CardWidth * 1.8f, D.CardHeight * 2, true, true, false, false);
    }

    public static GameObject DrawHandCard(Card card, Vector2 coords, int order, bool disabled = false) {
        GameObject parentCanvas = GameObject.Find("Canvas");
        GameObject newCard = new GameObject();

        RectTransform rectTransform = newCard.AddComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        rectTransform.sizeDelta = new Vector2(D.CardWidth, D.CardHeight);

        newCard.AddComponent<CanvasRenderer>();
        CardController controller = newCard.AddComponent<CardController>();
        controller.Card = card;
        controller.draggable = !disabled;
        controller.InitialLayerOrder = order;

        Image image = newCard.AddComponent<Image>();
        image.overrideSprite = GetSpriteForCard(card);
        if (disabled) image.color = new Color(0.8f, 0.8f, 0.8f);

        BoxCollider2D boxCollider = newCard.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(D.CardWidth, D.CardHeight);
        Rigidbody2D ridigBody = newCard.AddComponent<Rigidbody2D>();
        ridigBody.bodyType = RigidbodyType2D.Kinematic;

        newCard.transform.SetParent(parentCanvas.transform);
        newCard.transform.position = new Vector3(coords.x, coords.y, 0);

        Canvas canvas = newCard.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        GraphicRaycaster graphicRaycaster = newCard.AddComponent<GraphicRaycaster>();

        return newCard;
    }

    private static GameObject DrawCard(Card c, float x, float y, float width, float height, bool horizontal, bool transparent, bool executable, bool clickable) {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject newCard = new GameObject();
        newCard.AddComponent<CanvasRenderer>();

        BoxCollider2D boxCollider = newCard.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(width, height);

        RectTransform rectTransform = newCard.AddComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        if (horizontal) rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        rectTransform.sizeDelta = new Vector2(width, height);

        Image image = newCard.AddComponent<Image>();
        image.overrideSprite = GetSpriteForCard(c);
        //if (transparent) image.color = new Color(255, 255, 255, (float)0.8);

        newCard.transform.position = new Vector3(x, y, 0);
        newCard.transform.SetParent(canvas.transform);

        StaticCardsController controller = newCard.AddComponent<StaticCardsController>();
        controller.Card = c;
        controller.clickable = clickable;
        controller.executable = executable;

        return newCard;
    }

    public static GameObject DrawPointsButton(int points) {
        return DrawPrefabCardWithText(StaticCards.DummyPoints, D.PositionPointsButton, "ButtonPoints", points + "");
    }

    public static GameObject DrawWorkersCard(int howMany) {
        return DrawPrefabCardWithText(StaticCards.DummyWorker, D.PositionWorkerCard, "CardWorker", howMany + "");
    }

    public static GameObject DrawSilverCard(int howMany) {
        return DrawPrefabCardWithText(StaticCards.DummySilver, D.PositionSilverCard, "CardSilver", howMany + "");
    }

    public static GameObject DrawAnimalsCard(int howMany) {
        return DrawPrefabCardWithText(StaticCards.DummyAllAnimals, D.PositionAllAnimalsCard, "CardAnimals", howMany + "");
    }

    public static GameObject DrawStorageCard(int howMany) {
        return DrawPrefabCardWithText(StaticCards.DummyAllStorages, D.PositionAllStoragesCard, "CardStorage", howMany + "");
    }

    public static GameObject DrawEstateCard(int howMany) {
        return DrawPrefabCardWithText(StaticCards.DummyAllEstates, D.PositionAllEstatesCard, "CardEstate", howMany + "");
    }

    public static GameObject DrawProjectsCard(int howMany) {
        return DrawPrefabCardWithText(StaticCards.DummyAllProjects, D.PositionAllProjectsCard, "CardProjects", howMany + "");
    }

    private static GameObject DrawPrefabCardWithText(Card c, Vector2 position, string prefabRes, string text) {
        Object obj = Resources.Load("Prefabs/" + prefabRes);
        GameObject prefab = Object.Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        StaticCardsController controller = prefab.GetComponent<StaticCardsController>();
        controller.Card = c;

        TMPro.TextMeshProUGUI textObj = prefab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textObj.text = "" + text;

        return prefab;
    }

    public static void DrawDot(Vector2 coords) {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject dot = new GameObject();
        dot.AddComponent<CanvasRenderer>();

        RectTransform rectTransform = dot.AddComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        rectTransform.sizeDelta = new Vector2(10, 10);

        Image image = dot.AddComponent<Image>();
        image.color = new Color(255, 0, 255);

        dot.transform.position = new Vector3(coords.x, coords.y, 0);
        dot.transform.SetParent(canvas.transform);
    }

    public static Sprite GetSpriteForCard(Card card) {
        var fileUri = "Cards/";


        switch (card.Class) {
            case CardClass.Worker:
                fileUri += "workerGS";
                break;
            case CardClass.Silver:
                fileUri += "silver";
                break;
            case CardClass.AllEstates:
                fileUri += "estate";
                break;
            case CardClass.AllAnimals:
                fileUri += "all_animals";
                break;
            case CardClass.AllStorages:
                fileUri += "store";
                break;
            case CardClass.AllProjects:
                fileUri += "project";
                break;
            case CardClass.Exit:
                fileUri += "exit";
                break;
            case CardClass.EndTurn:
                fileUri += "finish";
                break;
            case CardClass.Dice:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "dice1";
                        break;
                    case CardDice.II:
                        fileUri += "dice2";
                        break;
                    case CardDice.III:
                        fileUri += "dice3";
                        break;
                    case CardDice.IV:
                        fileUri += "dice4";
                        break;
                    case CardDice.V:
                        fileUri += "dice5";
                        break;
                    case CardDice.VI:
                        fileUri += "dice6";
                        break;
                }
                break;
            case CardClass.Chicken:
                fileUri += "hen";
                break;
            case CardClass.Cow:
                fileUri += "cow";
                break;
            case CardClass.Sheep:
                fileUri += "lamb";
                break;
            case CardClass.Pig:
                fileUri += "pig";
                break;
            case CardClass.Goods:

                switch (card.Dice) {
                    case CardDice.I_II:
                        fileUri += "goods1-2";
                        break;
                    case CardDice.III_IV:
                        fileUri += "goods3-4";
                        break;
                    case CardDice.V_VI:
                        fileUri += "goods5-6";
                        break;
                }
                break;
            case CardClass.BonusA:
                fileUri += "bonusA";
                break;
            case CardClass.BonusB:
                fileUri += "bonusB";
                break;
            case CardClass.BonusC:
                fileUri += "bonusC";
                break;
            case CardClass.BonusD:
                fileUri += "bonusD";
                break;
            case CardClass.BonusE:
                fileUri += "bonusE";
                break;
            case CardClass.BonusCastleCompleted:
                break;
            case CardClass.BonusMine:
                break;
            case CardClass.BonusCloister:
                break;
            case CardClass.BonusKnowledge:
                break;
            case CardClass.BonusShip:
                break;
            case CardClass.BonusPasture:
                break;
            case CardClass.BonusAllSeven1:
                break;
            case CardClass.BonusAllSeven2:
                break;
            case CardClass.BonusAllSeven3:
                break;
            case CardClass.BonusAllSeven4:
                break;
            case CardClass.ActionCastle:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "c1";
                        break;
                    case CardDice.II:
                        fileUri += "c2";
                        break;
                    case CardDice.III:
                        fileUri += "c3";
                        break;
                    case CardDice.IV:
                        fileUri += "c4";
                        break;
                    case CardDice.V:
                        fileUri += "c5";
                        break;
                    case CardDice.VI:
                        fileUri += "c6";
                        break;
                }
                break;
            case CardClass.ActionMine:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "m1";
                        break;
                    case CardDice.II:
                        fileUri += "m2";
                        break;
                    case CardDice.III:
                        fileUri += "m3";
                        break;
                    case CardDice.IV:
                        fileUri += "m4";
                        break;
                    case CardDice.V:
                        fileUri += "m5";
                        break;
                    case CardDice.VI:
                        fileUri += "m6";
                        break;
                }
                break;
            case CardClass.ActionCloister:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "cl1";
                        break;
                    case CardDice.II:
                        fileUri += "cl2";
                        break;
                    case CardDice.III:
                        fileUri += "cl3";
                        break;
                    case CardDice.IV:
                        fileUri += "cl4";
                        break;
                    case CardDice.V:
                        fileUri += "cl5";
                        break;
                    case CardDice.VI:
                        fileUri += "cl6";
                        break;
                }
                break;
            case CardClass.ActionKnowledge:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "k1";
                        break;
                    case CardDice.II:
                        fileUri += "k2";
                        break;
                    case CardDice.III:
                        fileUri += "k3";
                        break;
                    case CardDice.IV:
                        fileUri += "k4";
                        break;
                    case CardDice.V:
                        fileUri += "k5";
                        break;
                    case CardDice.VI:
                        fileUri += "k6";
                        break;
                }
                break;
            case CardClass.ActionShip:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "s1";
                        break;
                    case CardDice.II:
                        fileUri += "s2";
                        break;
                    case CardDice.III:
                        fileUri += "s3";
                        break;
                    case CardDice.IV:
                        fileUri += "s4";
                        break;
                    case CardDice.V:
                        fileUri += "s5";
                        break;
                    case CardDice.VI:
                        fileUri += "s6";
                        break;
                }
                break;
            case CardClass.ActionPasture:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "p1";
                        break;
                    case CardDice.II:
                        fileUri += "p2";
                        break;
                    case CardDice.III:
                        fileUri += "p3";
                        break;
                    case CardDice.IV:
                        fileUri += "p4";
                        break;
                    case CardDice.V:
                        fileUri += "p5";
                        break;
                    case CardDice.VI:
                        fileUri += "p6";
                        break;
                }
                break;
            case CardClass.ActionCarpenter:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "car1";
                        break;
                    case CardDice.II:
                        fileUri += "car2";
                        break;
                    case CardDice.III:
                        fileUri += "car3";
                        break;
                }
                break;
            case CardClass.ActionChurch:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "ch1";
                        break;
                    case CardDice.II:
                        fileUri += "ch2";
                        break;
                    case CardDice.III:
                        fileUri += "ch3";
                        break;
                }
                break;
            case CardClass.ActionMarket:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "mar1";
                        break;
                    case CardDice.II:
                        fileUri += "mar2";
                        break;
                    case CardDice.III:
                        fileUri += "mar3";
                        break;
                }
                break;
            case CardClass.ActionWatchtower:
                switch (card.Dice) {
                    case CardDice.I:
                        fileUri += "wtw1";
                        break;
                    case CardDice.II:
                        fileUri += "wtw2";
                        break;
                    case CardDice.III:
                        fileUri += "wtw3";
                        break;
                }
                break;
            case CardClass.ActionBank:
                switch (card.Dice) {
                    case CardDice.IV:
                        fileUri += "bank4";
                        break;
                    case CardDice.V:
                        fileUri += "bank5";
                        break;
                    case CardDice.VI:
                        fileUri += "bank6";
                        break;
                }
                break;
            case CardClass.ActionBoardinghouse:
                switch (card.Dice) {
                    case CardDice.IV:
                        fileUri += "bh4";
                        break;
                    case CardDice.V:
                        fileUri += "bh5";
                        break;
                    case CardDice.VI:
                        fileUri += "bh6";
                        break;
                }
                break;
            case CardClass.ActionWarehouse:
                switch (card.Dice) {
                    case CardDice.IV:
                        fileUri += "wa4";
                        break;
                    case CardDice.V:
                        fileUri += "wa5";
                        break;
                    case CardDice.VI:
                        fileUri += "wa6";
                        break;
                }
                break;
            case CardClass.ActionCityHall:
                switch (card.Dice) {
                    case CardDice.IV:
                        fileUri += "cityh4";
                        break;
                    case CardDice.V:
                        fileUri += "cityh5";
                        break;
                    case CardDice.VI:
                        fileUri += "cityh6";
                        break;
                }
                break;
            case CardClass.SellSilverAndWorkers:
                fileUri += "sell_silver";
                break;
            case CardClass.ShipGoods:
                fileUri += "sell_goods";
                break;
            case CardClass.BonusCastle:
                fileUri += "bonus_castle";
                break;
            case CardClass.BonusCarperter:
                fileUri += "bonus_car";
                break;
            case CardClass.BonusChurch:
                fileUri += "bonus_ch";
                break;
            case CardClass.BonusMarket:
                fileUri += "bonus_mar";
                break;
            case CardClass.BonusCityHall:
                fileUri += "bonus_cityh";
                break;
            case CardClass.BonusBoardinghouse:
                fileUri += "bonus_bh";
                break;
            case CardClass.BonusWarehouse:
                fileUri += "bonus_wa";
                break;
        }

        return Resources.Load<Sprite>(fileUri);
    }


}
