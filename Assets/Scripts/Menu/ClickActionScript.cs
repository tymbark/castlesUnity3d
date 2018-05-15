using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using NetworkModels;

public class ClickActionScript : MonoBehaviour, IPointerClickHandler {

    public System.Action<Game> action;
    public Game game;

    public void OnPointerClick(PointerEventData eventData) {
        if (action != null && game != null) {
            action(game);
        } else {
            throw new System.InvalidProgramException("Game or action is null!");
        }
    }

}