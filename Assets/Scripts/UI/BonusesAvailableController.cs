using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using GD = GameDimensions;
using ED = EstatesDimensions;
using GSP = GameStateProvider;

public class BonusesAvailableController : MonoBehaviour {

    void Start() {
        float margin = GD.ScreenTopLeft.x + GD.MarginBig + GD.CardHeight;
        List<BonusCard> cards = GSP.GameState.AvailableBonusCards;
        for (int i = 0; i < cards.Count; i++) {
            BonusCard bc = cards[i];
            DrawCard(new Vector2(margin, ED.Player1Name.y), bc);
            margin += GD.CardWidth + GD.MarginSmall;
        }

    }

    private static void DrawCard(Vector2 position, BonusCard card) {
        Object obj = Resources.Load("Prefabs/Card");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        Image image = prefab.GetComponent<Image>();
        image.overrideSprite = CardsGenerator.GetSpriteForBonus(card);

        prefab.transform.position = new Vector3(position.x, position.y, 0);
    }
}
