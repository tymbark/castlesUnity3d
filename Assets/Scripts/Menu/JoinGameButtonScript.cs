using UnityEngine.EventSystems;
using UnityEngine;

public class JoinGameButtonScript : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        SceneLoader.LoadJoinGameScene();
    }

}