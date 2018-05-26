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

    public static void ShowChooseAnimalPopup(int howMany, System.Action callback) {
        Object prefab = Resources.Load("Prefabs/ChooseCardPopup");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -11);

        GameObject canvas = GameObject.Find("Canvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseAnimalController controller = messageGameObject.AddComponent<ChooseAnimalController>();
        controller.UpdateView(howMany);
        controller.DoneCallback = callback;
    }

    public static void ShowChooseGoodsPopup(int howMany, System.Action callback) {
        Object prefab = Resources.Load("Prefabs/ChooseCardPopup");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -11);

        GameObject canvas = GameObject.Find("Canvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseGoodsController controller = messageGameObject.AddComponent<ChooseGoodsController>();
        controller.UpdateView(howMany);
        controller.DoneCallback = callback;
    }

    public static void ShowChooseTripleStackPopup(Card card, System.Action<int> callback) {
        Object prefab = Resources.Load("Prefabs/ChooseCardPopup");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -11);

        GameObject canvas = GameObject.Find("Canvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseTripleController controller = messageGameObject.AddComponent<ChooseTripleController>();
        controller.UpdateView(card);
        controller.DoneCallback = callback;
    }

    public static void ShowChooseTripleBonusPopup(System.Action callback) {
        UnityEngine.Debug.Log("show triple popup");
        Object prefab = Resources.Load("Prefabs/ChooseBonusPopup");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -11);

        GameObject canvas = GameObject.Find("Canvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseBonusController controller = messageGameObject.AddComponent<ChooseBonusController>();
        controller.DoneCallback = callback;
    }

    public void RemovePopups() {
        foreach (GameObject obj in Trash) {
            Destroy(obj);
        }
        Trash.Clear();
        GameController.ClicksEnabled = true;
    }

}
