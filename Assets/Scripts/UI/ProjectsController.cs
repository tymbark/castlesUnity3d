using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using GD = GameDimensions;
using ED = EstatesDimensions;

public class ProjectsController : MonoBehaviour {

    private GameEngine GameEngine;

    private void Awake() {
        GameEngine = new GameEngine();
    }

    void Start() {
        for (int i = 0; i < GameEngine.GameState.Players.Count; i++) {
            Player p = GameEngine.GameState.Players[i];
            DrawPlayerProjects(p, i);
        }
    }

    private void DrawPlayerProjects(Player player, int index) {
        switch (index) {
            case 0:
                DrawPlayerName(ED.Player1Name, player.NickName);
                DrawPoints(ED.Player1Points, player.Score + "");
                DrawPlayerCards(player, ED.Player1Name.y);
                DrawSilverCardWithText(ED.Player1Silver, player.SilverCount + "");
                DrawWorkersCardWithText(ED.Player1Workers, player.WorkersCount + "");
                break;
            case 1:
                DrawPlayerName(ED.Player2Name, player.NickName);
                DrawPoints(ED.Player2Points, player.Score + "");
                DrawPlayerCards(player, ED.Player2Name.y);
                DrawSilverCardWithText(ED.Player2Silver, player.SilverCount + "");
                DrawWorkersCardWithText(ED.Player2Workers, player.WorkersCount + "");
                break;
            case 2:
                DrawPlayerName(ED.Player3Name, player.NickName);
                DrawPoints(ED.Player3Points, player.Score + "");
                DrawPlayerCards(player, ED.Player3Name.y);
                DrawSilverCardWithText(ED.Player3Silver, player.SilverCount + "");
                DrawWorkersCardWithText(ED.Player3Workers, player.WorkersCount + "");
                break;
            case 3:
                DrawPlayerName(ED.Player4Name, player.NickName);
                DrawPoints(ED.Player4Points, player.Score + "");
                DrawPlayerCards(player, ED.Player4Name.y);
                DrawSilverCardWithText(ED.Player4Silver, player.SilverCount + "");
                DrawWorkersCardWithText(ED.Player4Workers, player.WorkersCount + "");
                break;
        }
    }

    private static void DrawPlayerName(Vector2 position, string text) {
        Object obj = Resources.Load("Prefabs/TextPlayerName");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("ProjectsCanvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        TMPro.TextMeshProUGUI textObj = prefab.GetComponent<TMPro.TextMeshProUGUI>();
        textObj.text = "" + text;
    }

    private static void DrawPoints(Vector2 position, string points) {
        Object obj = Resources.Load("Prefabs/TextPoints");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("ProjectsCanvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        TMPro.TextMeshProUGUI textObj = prefab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textObj.text = "" + points;
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

        GameObject canvas = GameObject.Find("ProjectsCanvas");
        prefab.transform.SetParent(canvas.transform);

        Image image = prefab.GetComponent<Image>();
        image.overrideSprite = CardsGenerator.GetSpriteForCard(card);

        prefab.transform.position = new Vector3(position.x, position.y, 0);
    }

    private static void DrawWorkersCardWithText(Vector2 position, string text) {
        Object obj = Resources.Load("Prefabs/CardWorker");
        GameObject prefab = Instantiate(obj) as GameObject;
        Destroy(prefab.GetComponent<DropCardController>());

        GameObject canvas = GameObject.Find("ProjectsCanvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        TMPro.TextMeshProUGUI textObj = prefab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textObj.text = "" + text;
    }

    private static void DrawSilverCardWithText(Vector2 position, string text) {
        Object obj = Resources.Load("Prefabs/CardSilver");
        GameObject prefab = Instantiate(obj) as GameObject;
        Destroy(prefab.GetComponent<DropCardController>());

        GameObject canvas = GameObject.Find("ProjectsCanvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        prefab.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "" + text;
        prefab.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }

}
