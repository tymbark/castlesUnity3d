using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using GD = GameDimensions;
using ED = EstatesDimensions;

public class AnimalsController : MonoBehaviour {

    private GameEngine GameEngine;

    private void Awake() {
        GameEngine = new GameEngine();
    }

    void Start() {
        for (int i = 0; i < GameEngine.GameState.Players.Count; i++) {
            Player p = GameEngine.GameState.Players[i];
            DrawPlayerAnimals(p, i);
        }
    }

    private void DrawPlayerAnimals(Player player, int index) {
        switch (index) {
            case 0:
                DrawPlayerName(ED.Player1Name, player.NickName);
                DrawPoints(ED.Player1Points, player.Score + "");
                DrawPlayerAnimals(player, ED.Player1Name.y);
                break;
            case 1:
                DrawPlayerName(ED.Player2Name, player.NickName);
                DrawPoints(ED.Player2Points, player.Score + "");
                DrawPlayerAnimals(player, ED.Player2Name.y);
                break;
            case 2:
                DrawPlayerName(ED.Player3Name, player.NickName);
                DrawPoints(ED.Player3Points, player.Score + "");
                DrawPlayerAnimals(player, ED.Player3Name.y);
                break;
            case 3:
                DrawPlayerName(ED.Player4Name, player.NickName);
                DrawPoints(ED.Player4Points, player.Score + "");
                DrawPlayerAnimals(player, ED.Player4Name.y);
                break;
        }
    }

    private static void DrawPlayerAnimals(Player player, float axisY) {
        float margin = ED.CardsSpaceStart;
        int numberOfCows = 0;
        int numberOfChickens = 0;
        int numberOfPigs = 0;
        int numberOfSheep = 0;

        for (int i = 0; i < player.Animals.Count; i++) {
            Card c = player.Animals[i];
            switch (c.Class) {
                case CardClass.Pig:
                    numberOfPigs++;
                    break;
                case CardClass.Cow:
                    numberOfCows++;
                    break;
                case CardClass.Chicken:
                    numberOfChickens++;
                    break;
                case CardClass.Sheep:
                    numberOfSheep++;
                    break;
            }
        }

        if (numberOfCows > 0) {
            Vector2 position = new Vector2(margin, axisY);
            DrawAnimalCard(position, new Card(CardClass.Cow), numberOfCows + "");
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOfChickens > 0) {
            Vector2 position = new Vector2(margin, axisY);
            DrawAnimalCard(position, new Card(CardClass.Chicken), numberOfChickens + "");
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOfPigs > 0) {
            Vector2 position = new Vector2(margin, axisY);
            DrawAnimalCard(position, new Card(CardClass.Pig), numberOfPigs + "");
            margin += GD.CardWidth + GD.MarginSmall;
        }

        if (numberOfSheep > 0) {
            Vector2 position = new Vector2(margin, axisY);
            DrawAnimalCard(position, new Card(CardClass.Sheep), numberOfSheep + "");
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

    private static void DrawPoints(Vector2 position, string points) {
        Object obj = Resources.Load("Prefabs/TextPoints");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        TMPro.TextMeshProUGUI textObj = prefab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textObj.text = "" + points;
    }

    public static GameObject DrawAnimalCard(Vector2 position, Card card, string text) {
        Object obj = Resources.Load("Prefabs/CardWithNumber");
        GameObject prefab = Object.Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = new Vector3(position.x, position.y, 0);

        Image image = prefab.GetComponent<Image>();
        image.overrideSprite = CardsGenerator.GetSpriteForCard(card);

        TMPro.TextMeshProUGUI textObj = prefab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textObj.text = "" + text;

        return prefab;
    }

    //private static void DrawCard(Vector2 position, Card card) {
    //    Object obj = Resources.Load("Prefabs/Card");
    //    GameObject prefab = Instantiate(obj) as GameObject;

    //    GameObject canvas = GameObject.Find("AnimalsCanvas");
    //    prefab.transform.SetParent(canvas.transform);

    //    Image image = prefab.GetComponent<Image>();
    //    image.overrideSprite = CardsGenerator.GetSpriteForCard(card);

    //    prefab.transform.position = new Vector3(position.x, position.y, 0);
    //}

}
