using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using GD = GameDimensions;
using ED = EstatesDimensions;

public class GoodsController : MonoBehaviour {

    private GameEngine GameEngine;

    private void Awake() {
        GameEngine = new GameEngine();
    }

    void Start() {
        for (int i = 0; i < GameEngine.GameState.Players.Count; i++) {
            Player p = GameEngine.GameState.Players[i];
            DrawGoods(p, i);
        }
    }

    private void DrawGoods(Player player, int index) {
        switch (index) {
            case 0:
                DrawPlayerName(ED.Player1Name, player.NickName);
                DrawPoints(ED.Player1Points, player.Score);
                DrawPlayerGoods(player, ED.Player1Name.y);
                break;
            case 1:
                DrawPlayerName(ED.Player2Name, player.NickName);
                DrawPoints(ED.Player2Points, player.Score);
                DrawPlayerGoods(player, ED.Player2Name.y);
                break;
            case 2:
                DrawPlayerName(ED.Player3Name, player.NickName);
                DrawPoints(ED.Player3Points, player.Score);
                DrawPlayerGoods(player, ED.Player3Name.y);
                break;
            case 3:
                DrawPlayerName(ED.Player4Name, player.NickName);
                DrawPoints(ED.Player4Points, player.Score);
                DrawPlayerGoods(player, ED.Player4Name.y);
                break;
        }
    }

    private static void DrawPlayerGoods(Player player, float axisY) {
        float margin = ED.CardsSpaceStart;

        int numberOf_I_II = player.Goods.FindAll((Card obj) => obj.Dice == CardDice.I_II).Count;
        int numberOf_III_IV = player.Goods.FindAll((Card obj) => obj.Dice == CardDice.III_IV).Count;
        int numberOf_V_VI = player.Goods.FindAll((Card obj) => obj.Dice == CardDice.V_VI).Count;

        if (numberOf_I_II > 0) {
            Vector2 position = new Vector2(margin, axisY);
            DrawGoodsCard(position, "goods1-2", numberOf_I_II);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOf_III_IV > 0) {
            Vector2 position = new Vector2(margin, axisY);
            DrawGoodsCard(position, "goods3-4", numberOf_III_IV);
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOf_V_VI > 0) {
            Vector2 position = new Vector2(margin, axisY);
            DrawGoodsCard(position, "goods5-6", numberOf_V_VI);
            margin += GD.CardWidth + GD.MarginSmall;
        }

    }

    private static void DrawPlayerName(Vector2 position, string text) {
        Object obj = Resources.Load("Prefabs/TextPlayerName");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        TMPro.TextMeshProUGUI textObj = prefab.GetComponent<TMPro.TextMeshProUGUI>();
        textObj.text = "" + text;
    }

    private static void DrawPoints(Vector2 position, int points) {
        GameObject pointsCard = CardsGenerator.CreateCardGameObject("small_card_empty", position, false, true);
        pointsCard.AddSmallText(points + "", false, true);
    }

    private static GameObject DrawGoodsCard(Vector2 position, string resId, int howMany) {
        GameObject card = CardsGenerator.CreateCardGameObject(resId, position);
        card.AddSmallText(howMany + "");
        return card;
    }

}
