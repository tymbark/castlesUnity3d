using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GD = GameDimensions;
using GSP = GameStateProvider;

public class ChooseStackController : MonoBehaviour {

    private bool clickable = true;
    private GameObject AvailableCardsObj;
    private GameObject CompletedProjectObj;
    private GameObject dragCardText;
    private Card Card;
    public System.Action<int> DoneCallback;
    private readonly List<GameObject> GarbageCollector = new List<GameObject>();

    public void UpdateView(Card newCard) {
        foreach (GameObject obj in GarbageCollector) {
            Destroy(obj);
        }
        GarbageCollector.Clear();

        Card = newCard;
        CompletedProjectObj = GameObject.Find("completed_project_position");
        AvailableCardsObj = GameObject.Find("available_stacks_position");

        dragCardText = GameObject.Find("drag_card_text");

        GameObject.Find("create_new_stack")
                  .AddComponent<ClickActionScript>()
                  .ClickMethod = (x) => {
                      //generate new triple ID for new stack 
                      DoneCallback(GSP.GameState.CurrentPlayer.CompletedProjects.Count + 1);
                      Destroy(gameObject);
                  };

        DrawAvailableTriples();
        DrawNewCard();
    }

    private void DrawAvailableTriples() {
        float margin = 0;

        var triples = GSP.GameState.CurrentPlayer.GetNotCompletedTriplesForCard(Card);

        foreach (List<Card> cards in triples) {

            foreach (Card c in cards) {

                var card = CardsGenerator.CreateCardGameObject(c.GetResIdForCard(), new Vector2(margin, 0), parent: AvailableCardsObj);
                GarbageCollector.Add(card);

                var clickComponent = card.AddComponent<ClickActionScript>();
                clickComponent.ClickMethod = (x) => {
                    if (!clickable) return;
                    clickable = false;

                    DoneCallback(c.TripleId);
                    Destroy(gameObject);

                };

                margin = margin + GD.CardWidth * 0.6f;
            }

            margin = margin + GD.CardWidth + GD.MarginSmall;

        }

    }


    private void DrawNewCard() {
        var card = CardsGenerator.CreateCardGameObject(Card.GetResIdForCard(), Vector2.zero, parent: CompletedProjectObj);
        GarbageCollector.Add(card);
    }

}
