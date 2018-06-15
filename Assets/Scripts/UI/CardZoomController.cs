using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class CardZoomController : MonoBehaviour {

    private GameObject cardObject;
    private GameObject cardName;
    private GameObject cardDescription;
    private GameObject doneButton;
    private Card card;

    public void UpdateView(Card card) {
        print("UpdateView");
        cardObject = GameObject.Find("card_object");
        cardName = GameObject.Find("card_name");
        cardDescription = GameObject.Find("description_test");
        doneButton = GameObject.Find("done");

        this.card = card;
        cardDescription.GetComponent<TMPro.TextMeshProUGUI>().text = GetDescription();
        cardName.GetComponent<TMPro.TextMeshProUGUI>().text = GetCardName();

        cardObject.GetComponent<Image>().overrideSprite = CardsGenerator.GetSpriteForCard(card);

        doneButton.AddComponent<ClickActionScript>()
                  .ClickMethod = (item) => {
                      Destroy(gameObject, 0.5f);
                  };
    }

    private string GetCardName() {
        string description = "";

        switch (card.Class) {
            case CardClass.ActionCastle:
                description += "Castle";
                break;
            case CardClass.ActionMine:
                description += "Mine"; ;
                break;
            case CardClass.ActionCloister:
                description += "Cloister"; ;
                break;
            case CardClass.ActionKnowledge:
                description += "Knowledge"; ;
                break;
            case CardClass.ActionShip:
                description += "Ship";
                break;
            case CardClass.ActionPasture:
                description += "Pasture";
                break;
            case CardClass.ActionCarpenter:
                description += "Carpenter";
                break;
            case CardClass.ActionChurch:
                description += "Church";
                break;
            case CardClass.ActionMarket:
                description += "Market";
                break;
            case CardClass.ActionWatchtower:
                description += "Watchtower";
                break;
            case CardClass.ActionBank:
                description += "Bank";
                break;
            case CardClass.ActionBoardinghouse:
                description += "Boarding House";
                break;
            case CardClass.ActionWarehouse:
                description += "Warehouse";
                break;
            case CardClass.ActionCityHall:
                description += "City Hall";
                break;
        }

        return description;
    }

    private string GetDescription() {
        string description = "";

        switch (card.Class) {
            case CardClass.ActionCastle:
                description += "After completing this card you will receive one additional bonus card. This card can be used as any action card";
                break;
            case CardClass.ActionMine:
                description += "After completing this card you will receive two additional units of silver"; ;
                break;
            case CardClass.ActionCloister:
                description += "This card has no additional bonus, but it can be used as a wildcard to complete any triple card"; ;
                break;
            case CardClass.ActionKnowledge:
                description += "After completing this card you will receive two additional workers"; ;
                break;
            case CardClass.ActionShip:
                description += "After completing this card you can take one goods card from one of the decks";
                break;
            case CardClass.ActionPasture:
                description += "After completing this card you can take one animal card from one of the decks";
                break;
            case CardClass.ActionCarpenter:
                description += "After completing this card you can take one additional project of type Building or Knowledge";
                break;
            case CardClass.ActionChurch:
                description += "After completing this card you can take one additional project of type Cloister, Castle or Mine";
                break;
            case CardClass.ActionMarket:
                description += "After completing this card you can take one additional project of type Pasture or Ship";
                break;
            case CardClass.ActionWatchtower:
                description += "After completing this card you will receive one victory point";
                break;
            case CardClass.ActionBank:
                description += "After completing this card you will receive three units of silver";
                break;
            case CardClass.ActionBoardinghouse:
                description += "After completing this card you can take one animal or goods card from one of the decks";
                break;
            case CardClass.ActionWarehouse:
                description += "After completing this card you can sell one type of goods";
                break;
            case CardClass.ActionCityHall:
                description += "After completing this card you can take one additional project card";
                break;
        }

        return description;
    }

}
