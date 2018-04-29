using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

    private float StartingX;
    private float StartingY;

    private float StartingDraggingX;
    private float StartingDraggingY;

    public void OnBeginDrag(PointerEventData eventData) {
        StartingX = transform.position.x;
        StartingY = transform.position.y;
        StartingDraggingX = eventData.position.x;
        StartingDraggingY = eventData.position.y;
    }

    public void OnDrag(PointerEventData eventData) {
        //float dx = eventData.position.x - StartingDraggingX;
        //float dy = eventData.position.y - StartingDraggingY;

        //float newX = StartingX + dx;
        //float newY = StartingY + dy;

        //this.transform.position = new Vector3(newX, newY, transform.position.z);
        transform.position += (Vector3)eventData.delta;

    }

    public void OnEndDrag(PointerEventData eventData) {

    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
