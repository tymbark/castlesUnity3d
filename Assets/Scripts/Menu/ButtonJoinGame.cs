using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonJoinGame : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        SceneLoader.LoadJoinGameScene();
    }

}