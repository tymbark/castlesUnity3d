using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using GD = GameDimensions;
using ED = EstatesDimensions;

public class GameFinishedController : MonoBehaviour {

    private GameEngine GameEngine;

    private void Awake() {
        GameEngine = new GameEngine();
    }

    void Start() {

        List<Player> sortedPlayers = new List<Player>();
        sortedPlayers.AddRange(GameEngine.GameState.Players);
        sortedPlayers.Sort(
            delegate (Player p1, Player p2) {
                return p1.Score.CompareTo(p2.Score);
            }
        );

        float marginTop = 260f;
        float diff = 170f;

        for (int i = 0; i < sortedPlayers.Count; i++) {
            Player p = sortedPlayers[i];
            DrawPlayerFinishRow(i, p, new Vector2(0, marginTop));
            marginTop = marginTop - diff;
        }

        GameObject.Find("MainMenuButton")
                  .GetComponent<ClickActionScript>()
                  .ClickMethod = (obj) => SceneLoader.LoadMainMenuScene();
    }

    void DrawPlayerFinishRow(int place, Player player, Vector2 position) {
        Object obj = Resources.Load("Prefabs/FinishGamePlayerRow");
        GameObject prefab = Instantiate(obj) as GameObject;

        GameObject canvas = GameObject.Find("Canvas");
        prefab.transform.SetParent(canvas.transform);

        prefab.transform.position = position;

        prefab.transform
              .GetChild(0)
              .GetComponentInChildren<TMPro.TextMeshProUGUI>()
              .text = GetPlaceString(place);

        prefab.transform
              .GetChild(1)
              .GetComponentInChildren<TMPro.TextMeshProUGUI>()
              .text = player.NickName;

        prefab.transform
              .GetChild(2)
              .GetChild(0)
              .GetComponentInChildren<TMPro.TextMeshProUGUI>()
              .text = player.Score + "";

        TMPro.TextMeshProUGUI textObj = prefab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textObj.text = GetPlaceString(place);

    }

    private string GetPlaceString(int place) {
        switch (place) {
            case 0:
                return "1st place";
            case 1:
                return "2nd place";
            case 2:
                return "3rd place";
            case 3:
                return "4th place";
            default:
                return "unknown";
        }
    }

}
