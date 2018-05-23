using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

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

    //public void ShowMessageNextTurn() {
    //    GameController.ClicksEnabled = false;
    //    Object prefab = Resources.Load("Prefabs/TextNextTurn");
    //    GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
    //    messageGameObject.transform.position = Vector3.one;

    //    TMPro.TextMeshProUGUI text = messageGameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    //    text.text = "next player: " + GameController.GameEngine.GameState.CurrentPlayer.NickName;

    //    GameObject canvas = GameObject.Find("Canvas");
    //    messageGameObject.transform.SetParent(canvas.transform);
    //    Trash.Add(messageGameObject);
    //    Invoke("RemovePopups", 2.5f);
    //    Invoke("Refresh", 2.6f);
    //}

    public static GameObject ShowMessageWaitForYourTurn(string name) {
        Object prefab = Resources.Load("Prefabs/TextWaitForTurn");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = Vector3.one;

        TMPro.TextMeshProUGUI text = messageGameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text = "current turn: " + name;

        GameObject canvas = GameObject.Find("Canvas");
        messageGameObject.transform.SetParent(canvas.transform);
        return messageGameObject;
    }

    public static void ShowAreYouSurePopup(System.Action yesClicked) {
        Object prefab = Resources.Load("Prefabs/AreYouSure");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = Vector3.one;

        GameObject canvas = GameObject.Find("Canvas");
        messageGameObject.transform.SetParent(canvas.transform);

        messageGameObject.transform
                         .Find("Yes")
                         .GetComponent<ClickActionScript>()
                         .ClickMethod = (item) => {
                             Destroy(messageGameObject, 0.2f);
                             yesClicked();
                         };

        messageGameObject.transform
                         .Find("No")
                         .GetComponent<ClickActionScript>()
                         .ClickMethod = (item) => {
                             Destroy(messageGameObject, 0.2f);
                         };
    }

    public static void ShowChooseAnimalPopup(List<Card> availableCards) {
        Object prefab = Resources.Load("Prefabs/ChooseAnimalPopup");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -11);

        GameObject canvas = GameObject.Find("Canvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseAnimalController chooseAnimalController = messageGameObject.GetComponent<ChooseAnimalController>();
        chooseAnimalController.Setup(availableCards);
    }

    public void RemovePopups() {
        foreach (GameObject obj in Trash) {
            Destroy(obj);
        }
        Trash.Clear();
        GameController.ClicksEnabled = true;
    }

}
