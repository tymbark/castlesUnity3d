using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsController : MonoBehaviour {

    private readonly List<GameObject> Trash = new List<GameObject>();
    private GameController GameController;

    private void Start() {        
        GameController = GetComponent<GameController>();
    }

    public void ShowMessageCannotFinishTurn() {
        ShowMessageCannotFinishTurn("first use a card");
    }

    public void ShowMessageUseBonusCard() {
        ShowMessageCannotFinishTurn("use bonus card first");
    }

    private void ShowMessageCannotFinishTurn(string message) {
        GameController.ClicksEnabled = false;
        Object prefab = Resources.Load("Prefabs/TextUseACard");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = Vector3.one;

        TMPro.TextMeshProUGUI text = messageGameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text = message;

        GameObject canvas = GameObject.Find("Canvas");
        messageGameObject.transform.SetParent(canvas.transform);
        Trash.Add(messageGameObject);
        Invoke("RemovePopups", 2.5f);
    }

    public void ShowMessageNextTurn() {
        GameController.ClicksEnabled = false;
        Object prefab = Resources.Load("Prefabs/TextNextTurn");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = Vector3.one;

        TMPro.TextMeshProUGUI text = messageGameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text = "next player: " + GameController.GameEngine.GameState.CurrentPlayer.Name;

        GameObject canvas = GameObject.Find("Canvas");
        messageGameObject.transform.SetParent(canvas.transform);
        Trash.Add(messageGameObject);
        Invoke("RemovePopups", 2.5f);
        Invoke("Refresh", 2.6f);
    }

    public void Refresh() {
        GameController.RefreshTable();
    }

    public void RemovePopups() {
        foreach (GameObject obj in Trash) {
            Destroy(obj);
        }
        Trash.Clear();
        GameController.ClicksEnabled = true;
    }

}
