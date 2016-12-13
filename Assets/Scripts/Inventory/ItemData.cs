using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ItemData : MonoBehaviour, IPointerClickHandler
{
    public Item Item { get; set; }

    private bool IsDragged;
    private Vector3 OriginalPosition;
    private Transform OriginalParent;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !IsDragged)
        {
            IsDragged = true;
            OriginalPosition = transform.position;
            OriginalParent = transform.parent;

            ItemHolder.Instance.GetComponent<ItemHolder>().StartHolding(Item);
            transform.SetParent(ItemHolder.Instance.transform);

            Inventory.Close();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            ReturnToInventory();
        }
    }

    public void ReturnToInventory()
    {
        IsDragged = false;
        transform.SetParent(OriginalParent);
        transform.position = OriginalPosition;
        SantaController.controller.changeClothes(Item.Name, true);
    }
}
