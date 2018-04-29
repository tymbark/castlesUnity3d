using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

    private Vector3 StartingPosition;
    private Canvas canvas;

    public void OnBeginDrag(PointerEventData eventData) {
        StartingPosition = transform.position;
        canvas.sortingOrder = 1;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.position = StartingPosition;
        canvas.sortingOrder = -1;
    }

    void Start() {
        canvas = GetComponent<Canvas>();
        canvas.sortingOrder = -1;
    }

    void Update() {

    }
}
