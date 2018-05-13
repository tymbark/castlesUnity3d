using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinGameButtonScript : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        SceneManager.LoadScene("AvailableGames");
    }

}