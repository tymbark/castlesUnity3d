using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using NetworkModels;

public class ClickActionScript : MonoBehaviour, IPointerClickHandler {

    public System.Action<object> ClickMethod;
    public object ClickParameter;

    public void OnPointerClick(PointerEventData eventData) {
        if (ClickMethod != null) {
            ClickMethod(ClickParameter);
        } else {
            throw new System.InvalidProgramException("Click method not set!");
        }
    }

}