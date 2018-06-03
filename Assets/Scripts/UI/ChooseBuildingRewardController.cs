using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GD = GameDimensions;
using GSP = GameStateProvider;

public class ChooseBuildingRewardController : MonoBehaviour {

    private bool clickable = true;
    private GameObject availableCardsObj;
    public System.Action DoneCallback;
    public CardClass BonusCardClass;

    public void UpdateView() {
        availableCardsObj = GameObject.Find("available_cards_position");
        DrawAvailableCards();
    }

    private void DrawAvailableCards() {
        List<Card> availableCards = new List<Card>();
        print("Bonus card class" + BonusCardClass);

        var cards = new List<Card>();
        switch (BonusCardClass) {
            case CardClass.ActionCarpenter:
                cards = GSP.GameState.AvailableProjectCards
                   .FindAll((obj) => obj.Card.IsBuildingType() || obj.Card.Class == CardClass.ActionKnowledge)
                   .ConvertAll((ProjectCard input) => input.Card);

                print("draw cards" + cards.Describe());

                DrawCards(cards);
                break;

            case CardClass.ActionChurch:
                cards = GSP.GameState.AvailableProjectCards
                   .ConvertAll((ProjectCard input) => input.Card)
                   .FindAll((obj) => obj.Class == CardClass.ActionCloister
                                  || obj.Class == CardClass.ActionCastle
                                  || obj.Class == CardClass.ActionMine);

                print("draw cards" + cards.Describe());
                DrawCards(cards);
                break;

            case CardClass.ActionMarket:
                cards = GSP.GameState.AvailableProjectCards
                   .ConvertAll((ProjectCard input) => input.Card)
                   .FindAll((obj) => obj.Class == CardClass.ActionPasture
                                  || obj.Class == CardClass.ActionShip);

                print("draw cards" + cards.Describe());
                DrawCards(cards);
                break;

            case CardClass.ActionCityHall:
                cards = GSP.GameState.AvailableProjectCards
                   .ConvertAll((ProjectCard input) => input.Card);

                print("draw cards" + cards.Describe());
                DrawCards(cards);
                break;
        }

    }

    private void DrawCards(List<Card> cards) {
        float margin = 0;
        foreach (Card card in cards) {
            var cardObj = CardsGenerator.CreateCardGameObject(card.GetResIdForCard(), new Vector2(margin, 0), parent: availableCardsObj);

            cardObj.AddComponent<ClickActionScript>()
                   .ClickMethod = (x) => {
                       if (!clickable) return;

                       clickable = false;
                       GiveThisCardToPlayer(card);
                       Invoke("Destroy", 0.5f);
                   };

            margin = margin + GD.CardWidth + GD.MarginSmall;
        }
    }

    private void Destroy() {
        Destroy(gameObject);
        DoneCallback();
    }

    private void GiveThisCardToPlayer(Card card) {
        GSP.GameState.AvailableProjectCards.RemoveCardFromProjects(card);
        GSP.GameState.CurrentPlayer.ProjectArea.Add(card);
    }


}
