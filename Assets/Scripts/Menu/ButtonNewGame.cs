using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonNewGame : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        SceneLoader.LoadNewGameScene();
    }

}