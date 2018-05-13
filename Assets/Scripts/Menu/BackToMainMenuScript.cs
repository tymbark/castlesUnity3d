using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenuScript : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        SceneManager.LoadScene("MainMenu");
    }

}