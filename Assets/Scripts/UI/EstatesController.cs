using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Models;
using GD = GameDimensions;
using ED = EstatesDimensions;
using GSP = GameStateProvider;

public class EstatesController : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

    float currentDragPosition = 0;
    float maxDragPosition = 0;
    bool draggingAllowed = true;

    private List<GameObject> GarbageCollector = new List<GameObject>();

    void Start() {
        UpdateUI();
    }

    private void UpdateUI() {
        for (int i = 0; i < GSP.GameState.Players.Count; i++) {
            Player p = GSP.GameState.Players[i];
            DrawPlayerEstate(p, i);
        }

    }

    private void DrawPlayerEstate(Player player, int index) {
        switch (index) {
            case 0:
                DrawPlayerName(ED.Player1Name, player.NickName);
                DrawPoints(ED.Player1Points, player.Score);
                DrawPlayerCards(player, ED.Player1Name.y);
                break;
            case 1:
                DrawPlayerName(ED.Player2Name, player.NickName);
                DrawPoints(ED.Player2Points, player.Score);
                DrawPlayerCards(player, ED.Player2Name.y);
                break;
            case 2:
                DrawPlayerName(ED.Player3Name, player.NickName);
                DrawPoints(ED.Player3Points, player.Score);
                DrawPlayerCards(player, ED.Player3Name.y);
                break;
            case 3:
                DrawPlayerName(ED.Player4Name, player.NickName);
                DrawPoints(ED.Player4Points, player.Score);
                DrawPlayerCards(player, ED.Player4Name.y);
                break;
        }
    }

    private void DrawPlayerName(Vector2 position, string text) {
        Object obj = Resources.Load("Prefabs/TextPlayerName");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        TMPro.TextMeshProUGUI textObj = prefab.GetComponent<TMPro.TextMeshProUGUI>();
        textObj.text = "" + text;

        GarbageCollector.Add(prefab);
    }

    private void DrawPoints(Vector2 position, int points) {
        GameObject pointsCard = CardsGenerator.CreateCardGameObject("small_card_empty", position, false, true);
        pointsCard.AddSmallText(points + "", false, true);

        GarbageCollector.Add(pointsCard);
    }

    private void DrawPlayerCards(Player player, float axisY) {
        float margin = ED.CardsSpaceStart + currentDragPosition;

        var triples = player.GetAllTriples();

        foreach (List<Card> cards in triples) {
            foreach (Card card in cards) {

                if (margin + GD.CardWidth >= 960) {
                    draggingAllowed = true;
                } else {
                    draggingAllowed = false;
                }

                DrawCard(new Vector2(margin, axisY), card);
                margin += GD.CardWidth * 0.6f;
            }
            margin += GD.CardWidth * 0.4f + GD.MarginBig;

        }

    }

    private void DrawCard(Vector2 position, Card card) {
        Object obj = Resources.Load("Prefabs/Card");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        Image image = prefab.GetComponent<Image>();
        image.overrideSprite = CardsGenerator.GetSpriteForCard(card);
        //print(CardsGenerator.GetSpriteForCard(card));

        prefab.transform.position = new Vector3(position.x, position.y, 0);
        GarbageCollector.Add(prefab);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        print("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData) {
        print("OnDrag");
        GarbageCollector.ForEach((GameObject obj) => Destroy(obj));
        GarbageCollector.Clear();
        UpdateUI();

        print(eventData.delta.x);
        print(currentDragPosition);

        if (draggingAllowed) {
            if (eventData.delta.x < 0 || currentDragPosition < 0) {
                currentDragPosition += eventData.delta.x;
            }
        } else {
            if (eventData.delta.x > 0 && currentDragPosition < 0) {
                currentDragPosition += eventData.delta.x;
            }
        }



        //if (Mathf.Abs(currentDragPosition) < maxDragPosition) {
        //    currentDragPosition += eventData.delta.x;
        //}

        //if (currentDragPosition > 0) {
        //    currentDragPosition = 0;
        //}

    }

    public void OnEndDrag(PointerEventData eventData) {
        print("OnEndDrag");
    }


}
