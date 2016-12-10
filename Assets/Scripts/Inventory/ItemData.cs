using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ItemData : MonoBehaviour, IPointerClickHandler
{
    Item Item;

    private bool IsDragged;
    private Vector3 OriginalPosition;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !IsDragged)
        {
            IsDragged = true;
            OriginalPosition = transform.position;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            IsDragged = false;
            transform.position = OriginalPosition;
        }
    }

    void Update()
    {
        if (IsDragged)
        {
            transform.position = Input.mousePosition;
        }
    }
}
