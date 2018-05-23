using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    void Start() {

        UpdateNickNameText();

        GameObject.Find("Cancel")
                  .GetComponent<ClickActionScript>()
                  .ClickMethod = obj => { SceneLoader.LoadMainMenuScene(); };

        GameObject.Find("Done")
                  .GetComponent<ClickActionScript>()
                  .ClickMethod = obj => {
                      string newNickname = GameObject.Find("TextNewNickname").GetComponent<Text>().text;
                      DataPersistance.SavePlayerNickName(newNickname);
                      UpdateNickNameText();
                  };
    }

    private static void UpdateNickNameText() {
        GameObject.Find("CurrentNickName")
                          .GetComponent<TMPro.TextMeshProUGUI>()
                  .text = "current nickname: " + DataPersistance.GetPlayerNickName();
    }
}
