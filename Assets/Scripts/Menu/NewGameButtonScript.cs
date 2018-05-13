using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButtonScript : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        SceneManager.LoadScene("NewGame");
    }

}