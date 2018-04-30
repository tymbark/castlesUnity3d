using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

    private Vector3 StartingPosition;
    private List<GameObject> Colliders;
    private GameObject closestObject = null;
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

    private void Awake() {
        Colliders = new List<GameObject>();
    }

    private void Start() {
        canvas = GetComponent<Canvas>();
        canvas.sortingOrder = -1;
    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Colliders.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Colliders.Remove(collision.gameObject);
        if (Colliders.Count == 0) {
            ResetColor(closestObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        float smallestDistance = 999999;

        foreach (GameObject obj in Colliders) {
            var distance = Vector2.Distance(transform.position, obj.transform.position);
            if (distance < smallestDistance) {

                if (closestObject != null) {
                    ResetColor(closestObject);
                }

                smallestDistance = distance;
                closestObject = obj;
            }
        }

        SetBlueColor(closestObject);
    }

    private void ResetColor(GameObject obj) {
        Image image = obj.GetComponent<Image>();
        image.color = new Color(1, 1, 1, 1);
    }

    private void SetBlueColor(GameObject obj) {
        Image image = obj.GetComponent<Image>();
        image.color = new Color(0.5f, 0.5f, 1, 1);
    }

}
