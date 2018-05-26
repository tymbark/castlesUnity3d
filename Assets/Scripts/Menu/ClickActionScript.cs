using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using NetworkModels;

public class ClickActionScript : MonoBehaviour, IPointerClickHandler {

    public System.Action<object> ClickMethod;
    public object ClickParameter;
    private int time = -1;

    public void OnPointerClick(PointerEventData eventData) {
        int clickTime = (int)Time.time;

        if (clickTime - time <= 1) {
            print("ignored click ");
            return;
        } else {
            time = clickTime;
        }

        if (ClickMethod != null) {
            ClickMethod(ClickParameter);
        } else {
            throw new System.InvalidProgramException("Click method not set!");
        }
    }

}