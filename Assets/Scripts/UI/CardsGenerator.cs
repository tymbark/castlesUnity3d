using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputActions;
using Models;
using UnityEngine.UI;
using D = GameDimensions;

public static class CardsGenerator {

    public static GameObject DrawExitGameButton() {
        return DrawObjectFromPrefab(D.PositionExitButton, "ButtonExitGame");
    }

    public static GameObject DrawNextTurnButton() {
        return DrawObjectFromPrefab(D.PositionEndTurnButton, "ButtonEndTurn");
    }

    public static GameObject DrawTakeProjectCard(Card c, Vector2 coords) {
        GameObject gameObject = DrawCard(c, coords.x, coords.y, D.CardWidth, D.CardHeight, false);
        DropCardController dcc = gameObject.AddComponent<DropCardController>();
        dcc.Card = c;
        dcc.DragDropAction = DragDropAction.TakeProject;
        return gameObject;
    }

    public static GameObject DrawBuildProjectCard(Card c, Vector2 coords) {
        GameObject gameObject = DrawCard(c, coords.x, coords.y, D.CardWidth, D.CardHeight, false);
        DropCardController dcc = gameObject.AddComponent<DropCardController>();
        dcc.Card = c;
        dcc.DragDropAction = DragDropAction.BuildProject;
        return gameObject;
    }

    public static GameObject DrawStaticCard(Card c, Vector2 coords) {
        return DrawCard(c, coords.x, coords.y, D.CardWidth, D.CardHeight, false);
    }

    public static GameObject DrawBigBackgroundCard(Card c, Vector2 coords) {
        return DrawCard(c, coords.x, coords.y, D.CardWidth * 1.8f, D.CardHeight * 2, true);
    }

    public static GameObject DrawPointsElement(int points) {
        GameObject pointsCard = CreateCardGameObject("small_card_empty", D.PositionPointsButton, false, true);
        pointsCard.AddSmallText(points + "", false, true);

        return pointsCard;
    }

    public static GameObject DrawSellStuffCard() {
        GameObject workerCard = CreateCardGameObject("sell_silver", D.PositionSellSilverAndWorkersCard);
        workerCard.AddDropCardAction(DragDropAction.SellSilverAndWorkers);

        return workerCard;
    }

    public static GameObject DrawShipGoodsCard() {
        GameObject workerCard = CreateCardGameObject("ship_goods", D.PositionShipGoodsCard);
        workerCard.AddDropCardAction(DragDropAction.ShipGoods);

        return workerCard;
    }

    public static GameObject DrawWorkersCard(int howMany) {
        GameObject workerCard = CreateCardGameObject("workerGS", D.PositionWorkerCard);
        workerCard.AddSmallText(howMany + "");
        workerCard.AddClickAction(ClickAction.ShowWorkers);
        workerCard.AddDropCardAction(DragDropAction.BuyWorkers);

        return workerCard;
    }

    public static GameObject DrawSilverCard(int howMany, bool silverDoneThisTurn) {
        GameObject silverCard = CreateCardGameObject("silver", D.PositionSilverCard);
        silverCard.AddSmallText(howMany + "");
        silverCard.AddDropCardAction(DragDropAction.BuySilver);

        if (!silverDoneThisTurn && howMany >= 3) {
            silverCard.AddClickAction(ClickAction.UseSilver);
            var textObj = Object.Instantiate(Resources.Load("Prefabs/DefaultTextOrange80")) as GameObject;
            textObj.transform.SetParent(silverCard.transform);
            textObj.GetComponent<TMPro.TextMeshProUGUI>().text = "use";
            textObj.GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(0x00, 0x87, 0xD8, 0xFF);
            textObj.GetComponent<RectTransform>().sizeDelta = D.CardSize;
            textObj.transform.localPosition = new Vector2(0, -60);
        } else {
            silverCard.AddClickAction(ClickAction.ShowSilver);
        }

        return silverCard;
    }

    public static GameObject DrawAllAnimalsCard(int howMany) {
        GameObject animalsCard = CreateCardGameObject("all_animals", D.PositionAllAnimalsCard);
        animalsCard.AddSmallText(howMany + "");
        animalsCard.AddClickAction(ClickAction.ShowAnimals);

        return animalsCard;
    }

    public static GameObject DrawStorageCard(int howMany) {
        GameObject storageCard = CreateCardGameObject("store", D.PositionAllStoragesCard, true);
        storageCard.AddSmallText(howMany + "", true);
        storageCard.AddClickAction(ClickAction.ShowStorage);

        return storageCard;
    }

    public static GameObject DrawEstateCard(int howMany) {
        GameObject estateCard = CreateCardGameObject("estate", D.PositionAllEstatesCard, true);
        estateCard.AddSmallText(howMany + "", true);
        estateCard.AddClickAction(ClickAction.ShowEstates);

        return estateCard;
    }

    public static GameObject DrawProjectsCard(int howMany) {
        GameObject projectsCard = CreateCardGameObject("project", D.PositionAllProjectsCard, true);
        projectsCard.AddSmallText(howMany + "", true);
        projectsCard.AddClickAction(ClickAction.ShowProjects);

        return projectsCard;
    }

    public static GameObject CreateCardGameObject(string imageResId,
                                                  Vector2 position,
                                                  bool horizontal = false,
                                                  bool small = false,
                                                  GameObject parent = null) {
        GameObject newCard = new GameObject();

        if (parent == null) {
            GameObject canvas = GameObject.Find("Canvas");
            newCard.transform.SetParent(canvas.transform);
        } else {
            newCard.transform.SetParent(parent.transform);
        }

        newCard.AddComponent<CanvasRenderer>();

        RectTransform rectTransform = newCard.AddComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1f, 1f, 1f);

        if (small) {
            rectTransform.sizeDelta = new Vector2(D.CardWidth, D.CardWidth);
        } else {
            rectTransform.sizeDelta = D.CardSize;
        }

        if (horizontal) {
            rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }

        Image image = newCard.AddComponent<Image>();
        image.overrideSprite = Resources.Load<Sprite>("Cards/" + imageResId);

        newCard.transform.localPosition = new Vector3(position.x, position.y, 0);


        return newCard;
    }

    public static void AddSmallText(this GameObject gameObject, string text, bool horizontal = false, bool centerText = false) {
        var textObj = Object.Instantiate(Resources.Load("Prefabs/DefaultTextOrange80")) as GameObject;
        textObj.transform.SetParent(gameObject.transform);
        textObj.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        textObj.GetComponent<RectTransform>().sizeDelta = D.CardSize;

        if (horizontal) {
            textObj.transform.localPosition = new Vector2(-38, 72);
        } else if (centerText) {
            textObj.transform.localPosition = new Vector2(0, 0);
        } else {
            textObj.transform.localPosition = new Vector2(35, 65);
        }
    }

    public static void AddClickAction(this GameObject gameObject, ClickAction clickAction) {
        var clickComponent = gameObject.AddComponent<ClickableObjectController>();
        clickComponent.ClickAction = clickAction;
    }

    private static void AddDropCardAction(this GameObject gameObject, DragDropAction dropAction) {
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = D.CardSize;
        var dropController = gameObject.AddComponent<DropCardController>();
        dropController.DragDropAction = dropAction;
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

    private static GameObject DrawCard(Card card, float x, float y, float width, float height, bool horizontal) {
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
        image.overrideSprite = GetSpriteForCard(card);
        //if (transparent) image.color = new Color(255, 255, 255, (float)0.8);

        newCard.transform.position = new Vector3(x, y, 0);
        newCard.transform.SetParent(canvas.transform);

        return newCard;
    }

    public static GameObject DrawCurrentBonusCard(Round currentRound) {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject newCard = new GameObject();
        newCard.AddComponent<CanvasRenderer>();

        BoxCollider2D boxCollider = newCard.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = D.CardSize;

        RectTransform rectTransform = newCard.AddComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        rectTransform.sizeDelta = D.CardSize;

        Image image = newCard.AddComponent<Image>();
        image.overrideSprite = GetSpriteForCurrentRoundBonus(currentRound);

        newCard.transform.position = D.PositionCurrentBonusCard;
        newCard.transform.SetParent(canvas.transform);

        ClickableObjectController controller = newCard.AddComponent<ClickableObjectController>();
        controller.ClickAction = ClickAction.ShowBonuses;

        return newCard;
    }

    private static GameObject DrawObjectFromPrefab(Vector2 position, string prefabRes) {
        Object obj = Resources.Load("Prefabs/" + prefabRes);
        GameObject prefab = Object.Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);
        return prefab;
    }

    public static GameObject DrawObjectWithTextFromPrefab(Vector2 position, string prefabRes, string text) {
        Object obj = Resources.Load("Prefabs/" + prefabRes);
        GameObject prefab = Object.Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

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
            case CardClass.AllProjects:
                fileUri += "project";
                break;
            case CardClass.Exit:
                fileUri += "exit";
                break;
            case CardClass.EndTurn:
                fileUri += "finish";
                break;
            case CardClass.Barrel:
                fileUri += "barrel1";
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
                    default:
                        fileUri += "generic_goods";
                        break;
                }
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
                fileUri += "ship_goods";
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

    public static string GetResIdForCard(this Card card) {

        var fileUri = "";

        switch (card.Class) {
            case CardClass.Worker:
                fileUri += "workerGS";
                break;
            case CardClass.Silver:
                fileUri += "silver";
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
            case CardClass.Barrel:
                fileUri += "barrel1";
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
                    default:
                        fileUri += "generic_goods";
                        break;
                }
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
                fileUri += "ship_goods";
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

        return fileUri;
    }

    public static string GetResIdForCurrentRoundBonus(this Round round) {
        var fileUri = "";

        switch (round) {
            case Round.A:
                fileUri += "bonusA";
                break;
            case Round.B:
                fileUri += "bonusB";
                break;
            case Round.C:
                fileUri += "bonusC";
                break;
            case Round.D:
                fileUri += "bonusD";
                break;
            case Round.E:
                fileUri += "bonusE";
                break;
        }

        return fileUri;
    }

    public static Sprite GetSpriteForCurrentRoundBonus(Round round) {
        var fileUri = "Cards/";

        switch (round) {
            case Round.A:
                fileUri += "bonusA";
                break;
            case Round.B:
                fileUri += "bonusB";
                break;
            case Round.C:
                fileUri += "bonusC";
                break;
            case Round.D:
                fileUri += "bonusD";
                break;
            case Round.E:
                fileUri += "bonusE";
                break;
        }

        return Resources.Load<Sprite>(fileUri);
    }

    public static Sprite GetSpriteForBonus(BonusCard bonusCard) {
        var fileUri = "Cards/";

        switch (bonusCard) {
            case BonusCard.FirstPlayer:
                fileUri += "1player";
                break;
            case BonusCard.FirstPlayerReverse:
                fileUri += "1playerQ";
                break;
            case BonusCard.Building:
                fileUri += "bonusBuilding";
                break;
            case BonusCard.Castle:
                fileUri += "bonusCastle";
                break;
            case BonusCard.Mine:
                fileUri += "bonusMine";
                break;
            case BonusCard.Cloister:
                fileUri += "bonusCloister";
                break;
            case BonusCard.Knowledge:
                fileUri += "bonusKnowledge";
                break;
            case BonusCard.Ship:
                fileUri += "bonusShip";
                break;
            case BonusCard.Pasture:
                fileUri += "bonusPasture";
                break;
            case BonusCard.AllSeven4:
                fileUri += "bonus4";
                break;
            case BonusCard.AllSeven3:
                fileUri += "bonus3";
                break;
            case BonusCard.AllSeven2:
                fileUri += "bonus2";
                break;
            case BonusCard.AllSeven1:
                fileUri += "bonus1";
                break;
        }

        return Resources.Load<Sprite>(fileUri);
    }

}
