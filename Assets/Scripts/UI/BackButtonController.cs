using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour, IPointerClickHandler {

    public string TargetScene = "";

    public void OnPointerClick(PointerEventData eventData) {
        if (TargetScene.Length == 0) {
            SceneLoader.LoadMainGameScene();
        } else {
            print("Load Scene " + TargetScene);
            SceneManager.LoadScene(TargetScene);
        }
    }

}
