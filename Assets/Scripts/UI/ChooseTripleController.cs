using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GD = GameDimensions;

public class ChooseTripleController : MonoBehaviour {

    private bool clickable = true;
    private GameState GameState;
    private Vector2 posAvailableCards;
    private Vector2 posYourCards;
    private GameObject dragCardText;
    private Card Card;
    public System.Action<int> DoneCallback;
    private readonly List<GameObject> GarbageCollector = new List<GameObject>();

    private void Awake() {
        GameState = new GameEngine().GameState;
    }

    public void UpdateView(Card newCard) {
        foreach (GameObject obj in GarbageCollector) {
            Destroy(obj);
        }
        GarbageCollector.Clear();

        Card = newCard;
        posYourCards = new Vector2(-200, -350);
        posAvailableCards = new Vector2(-200, 200);

        dragCardText = GameObject.Find("drag_card_text");

        GameObject.Find("popup_title")
                  .GetComponent<TMPro.TextMeshProUGUI>()
                  .text = "choose stack";

        GameObject.Find("how_many_text")
                  .GetComponent<TMPro.TextMeshProUGUI>()
                  .text = "pick triple stack";

        GameObject.Find("available_cards_text")
                  .GetComponent<TMPro.TextMeshProUGUI>()
                  .text = "just completed card";

        GameObject.Find("your_cards_text")
                  .GetComponent<TMPro.TextMeshProUGUI>()
                  .text = "available triple stacks";

        DrawAvailableTriples();
        DrawNewCard();
    }

    private void DrawAvailableTriples() {
        float margin = posYourCards.x;

        var triples = GameState.CurrentPlayer.GetNotCompletedTriplesForCard(Card);

        foreach (List<Card> cards in triples) {

            foreach (Card c in cards) {

                var card = CardsGenerator.CreateCardGameObject(c.GetResIdForCard(), new Vector2(margin, posYourCards.y), parent: gameObject);
                GarbageCollector.Add(card);

                var clickComponent = card.AddComponent<ClickActionScript>();
                clickComponent.ClickMethod = (x) => {
                    if (!clickable) return;
                    clickable = false;

                    GameState.SaveGameState();

                    DoneCallback(c.TripleId);
                    Destroy(gameObject);

                };

                margin = margin + GD.CardWidth * 0.6f;
            }

            margin = margin + GD.MarginBig;

        }

    }


    private void DrawNewCard() {
        var card = CardsGenerator.CreateCardGameObject(Card.GetResIdForCard(), posAvailableCards, parent: gameObject);
        GarbageCollector.Add(card);
    }

}
