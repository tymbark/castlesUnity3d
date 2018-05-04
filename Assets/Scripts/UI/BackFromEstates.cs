using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackFromEstates : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        SceneManager.LoadScene("MainGame");
    }

}
