﻿using System.Collections;
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

        GameObject canvas = GameObject.Find("PopupsCanvas");
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

        GameObject canvas = GameObject.Find("PopupsCanvas");
        messageGameObject.transform.SetParent(canvas.transform);
        return messageGameObject;
    }

    public static void ShowAreYouSurePopup(System.Action yesClicked) {
        Object prefab = Resources.Load("Prefabs/AreYouSure");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = Vector3.one;

        GameObject canvas = GameObject.Find("PopupsCanvas");
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
        messageGameObject.transform.position = new Vector3(0, 0, -36);

        GameObject canvas = GameObject.Find("PopupsCanvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseAnimalController controller = messageGameObject.AddComponent<ChooseAnimalController>();
        controller.UpdateView(howMany);
        controller.DoneCallback = callback;
    }

    public static void ShowTakeGoodsOrAnimal(System.Action callback) {
        Object prefab = Resources.Load("Prefabs/ChooseCardPopup");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -36);

        GameObject canvas = GameObject.Find("PopupsCanvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseBothController controller = messageGameObject.AddComponent<ChooseBothController>();
        controller.DoneCallback = callback;
        controller.UpdateView();
    }

    public static void ShowTakeBuildingRewardCard(System.Action callback, CardClass bonusCardClass) {
        Object prefab = Resources.Load("Prefabs/ChooseBuildingRewardCard");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -36);

        GameObject canvas = GameObject.Find("PopupsCanvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseBuildingRewardController controller = messageGameObject.AddComponent<ChooseBuildingRewardController>();
        controller.BonusCardClass = bonusCardClass;
        controller.DoneCallback = callback;
        controller.UpdateView();
    }

    public static void ShowChooseGoodsPopup(int howMany, System.Action callback) {
        Object prefab = Resources.Load("Prefabs/ChooseCardPopup");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -36);

        GameObject canvas = GameObject.Find("PopupsCanvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseGoodsController controller = messageGameObject.AddComponent<ChooseGoodsController>();
        controller.UpdateView(howMany);
        controller.DoneCallback = callback;
    }

    public static void ShowChooseTripleStackPopup(Card card, System.Action<int> callback) {
        Object prefab = Resources.Load("Prefabs/ChooseStackPopup");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -36);

        GameObject canvas = GameObject.Find("PopupsCanvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseStackController controller = messageGameObject.AddComponent<ChooseStackController>();
        controller.UpdateView(card);
        controller.DoneCallback = callback;
    }

    public static void ShowChooseTripleBonusPopup(System.Action callback) {
        Object prefab = Resources.Load("Prefabs/ChooseTripleBonusPopup");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -36);

        GameObject canvas = GameObject.Find("PopupsCanvas");
        messageGameObject.transform.SetParent(canvas.transform);
        ChooseBonusController controller = messageGameObject.AddComponent<ChooseBonusController>();
        controller.DoneCallback = callback;
    }

    public static void ShowYourTurnPopup() {
        Object prefab = Resources.Load("Prefabs/YourTurnPopup");
        GameObject messageGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        messageGameObject.transform.position = new Vector3(0, 0, -36);

        GameObject canvas = GameObject.Find("PopupsCanvas");
        messageGameObject.transform.SetParent(canvas.transform);
        YourTurnController controller = messageGameObject.AddComponent<YourTurnController>();
    }

    public static void ShowCardZoomPopup(Card card) {
        Object prefab = Resources.Load("Prefabs/CardZoom");
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        obj.transform.position = new Vector3(0, 0, -36);

        GameObject canvas = GameObject.Find("PopupsCanvas");
        obj.transform.SetParent(canvas.transform);
        CardZoomController controller = obj.AddComponent<CardZoomController>();
        controller.UpdateView(card);
    }

    public void RemovePopups() {
        foreach (GameObject obj in Trash) {
            Destroy(obj);
        }
        Trash.Clear();
        GameController.ClicksEnabled = true;
    }

}
