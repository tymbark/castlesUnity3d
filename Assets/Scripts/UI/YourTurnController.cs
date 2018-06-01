using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using GD = GameDimensions;
using GSP = GameStateProvider;

public class YourTurnController : MonoBehaviour {

    private GameObject CurrentRoundCard;
    private GameObject NewCard;
    private GameObject YourCards;

    private GameObject DoneButton;
    public System.Action DoneCallback;

    public void Start() {
        CurrentRoundCard = GameObject.Find("current_round_card_position");
        NewCard = GameObject.Find("new_card_position");
        YourCards = GameObject.Find("cards_in_hand_position");
        DoneButton = GameObject.Find("ok_button");

        DoneButton.AddComponent<ClickActionScript>().ClickMethod = (obj) => {
            Destroy(gameObject, 0.5f);
        };

        DrawCardsLeftText();
        DrawCurrenRoundCard();
        DrawPlayerCards();
        DrawNewCard();
    }

    private void DrawNewCard() {
        if (GSP.GameState.CurrentPlayer.Cards.Count > 1) {
            CardsGenerator.CreateCardGameObject(GSP.GameState.CurrentPlayer.Cards[1].GetResIdForCard(), Vector2.zero, parent: NewCard);
        }
    }

    private static void DrawCardsLeftText() {
        GameObject.Find("cards_left_number")
                  .GetComponent<TMPro.TextMeshProUGUI>()
                  .text = GSP.GameState
                             .CurrentPlayer
                             .FutureCards.Count + "";
    }

    private void DrawPlayerCards() {
        float margin = 0;
        GSP.GameState.CurrentPlayer.Cards.ForEach((Card obj) => {
            CardsGenerator.CreateCardGameObject(obj.GetResIdForCard(), new Vector2(margin, 0), parent: YourCards);
            margin = margin + GD.CardWidth * 0.8f;
        });
    }

    private void DrawCurrenRoundCard() {
        string imageResId = GSP.GameState.CurrentRound.GetResIdForCurrentRoundBonus();
        CardsGenerator.CreateCardGameObject(imageResId, Vector2.zero, parent: CurrentRoundCard);
    }
}
