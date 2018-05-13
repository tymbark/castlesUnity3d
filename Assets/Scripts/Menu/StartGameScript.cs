using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    public int howManyPlayers = 0;

    public void OnPointerClick(PointerEventData eventData) {
        SceneManager.LoadScene("MainGame");
    }

}