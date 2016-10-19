using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class mouseDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData) {

    }
}
