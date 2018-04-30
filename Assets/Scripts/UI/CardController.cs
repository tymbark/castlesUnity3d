using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Models;

public class CardController : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

    private Vector3 StartingPosition;
    private List<GameObject> Colliders = new List<GameObject>();
    private GameObject closestObject = null;
    private Canvas canvas;
    private GameController GameController;
    private bool dragging = false;
    public readonly bool executable = false;
    public readonly bool clickable = false;
    public readonly bool draggable = true;
    public Card Card;


    private void Start() {
        canvas = GetComponent<Canvas>();
        canvas.sortingOrder = -1;

        GameObject gameObj = GameObject.Find("GameObjectController");
        GameController = gameObj.GetComponent<GameController>();
    }

    void Update() {

    }

    public void OnBeginDrag(PointerEventData eventData) {
        dragging = true;
        StartingPosition = transform.position;
        canvas.sortingOrder = 1;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        dragging = false;
        transform.position = StartingPosition;
        canvas.sortingOrder = -1;
        if (closestObject != null) {
            GameController.HandleCardDroppedAction(gameObject, closestObject);
            closestObject = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (dragging) {
            StaticCardsController controller = collision.gameObject.GetComponent<StaticCardsController>();
            if (controller != null && controller.executable) {
                Colliders.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject == closestObject) {
            GameController.HandleCardLeaveAction(gameObject, closestObject);
            closestObject = null;
        }

        if (Colliders.Contains(collision.gameObject)) {
            Colliders.Remove(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!dragging) return;

        float smallestDistance = 9999999;
        bool closestHasChanged = false;

        if (closestObject != null) {
            smallestDistance = Vector2.Distance(transform.position, closestObject.transform.position);
        }


        foreach (GameObject obj in Colliders) {
            float distance = Vector2.Distance(transform.position, obj.transform.position);
            if (distance < smallestDistance) {

                if (closestObject != null && closestObject != obj) {
                    GameController.HandleCardLeaveAction(gameObject, closestObject);
                }

                smallestDistance = distance;
                closestObject = obj;
                closestHasChanged = true;
            }
        }

        if (closestObject != null && closestHasChanged) {
            GameController.HandleCardHoverAction(gameObject, closestObject);
        }
    }

}
