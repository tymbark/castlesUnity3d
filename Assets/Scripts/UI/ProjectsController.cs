using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using GD = GameDimensions;
using ED = EstatesDimensions;
using GSP = GameStateProvider;

public class ProjectsController : MonoBehaviour {

    void Start() {
        for (int i = 0; i < GSP.GameState.Players.Count; i++) {
            Player p = GSP.GameState.Players[i];
            DrawPlayerProjects(p, i);
        }
    }

    private void DrawPlayerProjects(Player player, int index) {
        switch (index) {
            case 0:
                DrawPlayerName(ED.Player1Name, player.NickName);
                DrawPoints(ED.Player1Points, player.Score);
                DrawPlayerCards(player, ED.Player1Name.y);
                DrawSilverCard(ED.Player1Silver, player.SilverCount);
                DrawWorkersCard(ED.Player1Workers, player.WorkersCount);
                break;
            case 1:
                DrawPlayerName(ED.Player2Name, player.NickName);
                DrawPoints(ED.Player2Points, player.Score);
                DrawPlayerCards(player, ED.Player2Name.y);
                DrawSilverCard(ED.Player2Silver, player.SilverCount);
                DrawWorkersCard(ED.Player2Workers, player.WorkersCount);
                break;
            case 2:
                DrawPlayerName(ED.Player3Name, player.NickName);
                DrawPoints(ED.Player3Points, player.Score);
                DrawPlayerCards(player, ED.Player3Name.y);
                DrawSilverCard(ED.Player3Silver, player.SilverCount);
                DrawWorkersCard(ED.Player3Workers, player.WorkersCount);
                break;
            case 3:
                DrawPlayerName(ED.Player4Name, player.NickName);
                DrawPoints(ED.Player4Points, player.Score);
                DrawPlayerCards(player, ED.Player4Name.y);
                DrawSilverCard(ED.Player4Silver, player.SilverCount);
                DrawWorkersCard(ED.Player4Workers, player.WorkersCount);
                break;
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

    private static GameObject DrawPoints(Vector2 position, int points) {
        GameObject pointsCard = CardsGenerator.CreateCardGameObject("small_card_empty", position, false, true);
        pointsCard.AddSmallText(points + "", false, true);

        return pointsCard;
    }

    private static void DrawPlayerCards(Player player, float axisY) {
        float margin = ED.CardsSpaceStart;

        for (int i = 0; i < player.ProjectArea.Count; i++) {
            Card c = player.ProjectArea[i];

            Vector2 position = new Vector2(margin, axisY);
            DrawCard(position, c);
            margin += GD.CardWidth + GD.MarginSmall;
        }
    }

    private static void DrawCard(Vector2 position, Card card) {
        Object obj = Resources.Load("Prefabs/Card");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        Image image = prefab.GetComponent<Image>();
        image.overrideSprite = CardsGenerator.GetSpriteForCard(card);

        prefab.transform.position = new Vector3(position.x, position.y, 0);
    }

    private static GameObject DrawWorkersCard(Vector2 position, int howMany) {
        GameObject workerCard = CardsGenerator.CreateCardGameObject("workerGS", position);
        workerCard.AddSmallText(howMany + "");
        return workerCard;
    }

    private static GameObject DrawSilverCard(Vector2 position, int howMany) {
        GameObject silverCard = CardsGenerator.CreateCardGameObject("silver", position);
        silverCard.AddSmallText(howMany + "");
        return silverCard;
    }

}
