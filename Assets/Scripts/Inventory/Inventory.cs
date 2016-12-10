using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private int SlotCount;
    public GameObject InventoryPanel;
    public GameObject SlotPanel;
    public GameObject InventorySlot;
    public GameObject InventoryItem;

    public Item[] Items;
    public GameObject[] Slots;

    void Start()
    {
        SlotCount = 24;
        Items = new Item[SlotCount];
        Slots = new GameObject[SlotCount];

        for (int i = 0; i < SlotCount; i++)
        {
            Slots[i] = Instantiate(InventorySlot);
            Slots[i].transform.SetParent(SlotPanel.transform);
        }
    }

    private Item FindItem(Item item)
    {
        foreach (var i in Items)
        {
            if (i != null && i.Name == item.Name)
                return i;
        }
        return null;
    }

    public void AddItem(Item item, bool duplicates = false)
    {
        // We don't want duplicates -> if we already have one return
        if (!duplicates && FindItem(item) != null)
            return;

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
            {
                // Set item
                Items[i] = item;

                // Set displayed game object
                GameObject itemObj = Instantiate(InventoryItem);
                itemObj.transform.SetParent(Slots[i].transform);
                itemObj.transform.position = Slots[i].transform.position;
                itemObj.GetComponent<Image>().sprite = item.Sprite;
                itemObj.name = item.Name;

                break;
            }
        }
    }
}
