using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    private int SlotCount;
    public GameObject InventoryPanel;
    public GameObject SlotPanel;
    public GameObject InventorySlot;
    public GameObject InventoryItem;

    public Sprite coat;
    public Sprite hat;
    public Sprite shoes;
    public Sprite gloves;
    public Sprite presents;
    public Item[] Items;
    public GameObject[] Slots;

    private bool IsOpen;

    public void Toggle()
    {
        if (Instance != null)
        {
            if (GameController.Instance.IsInputEnabled)
            {
                if (Instance.IsOpen)
                    Close();
                else
                    Open();
            }
        }
    }

    public static void Open()
    {
        if (Instance != null)
        {
            Instance.InventoryPanel.SetActive(true);
            Instance.IsOpen = true;
        }
    }

    public static void Close()
    {
        if (Instance != null)
        {
            Instance.InventoryPanel.SetActive(false);
            Instance.IsOpen = false;
        }
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            IsOpen = true;
            SlotCount = 24;
            Items = new Item[SlotCount];
            Slots = new GameObject[SlotCount];

            for (int i = 0; i < SlotCount; i++)
            {
                Slots[i] = Instantiate(InventorySlot);
                Slots[i].transform.SetParent(SlotPanel.transform);
            }

            AddItem(new Item("Coat", coat), false, false);
            AddItem(new Item("Hat", hat), false, false);
            AddItem(new Item("Gloves", gloves), false, false);
            AddItem(new Item("Shoes", shoes), false, false);
            AddItem(new Item("Presents", presents), false, false);

            Close();
        }
        else
        {
            Destroy(gameObject);
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

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].Name == item.Name)
            {
                Items[i] = null;
                return;
            }
        }
    }

    public void AddItem(Item item, bool duplicates = false, bool shake = true)
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
                itemObj.GetComponent<ItemData>().Item = item;


                // Blink inventory button
                if(shake)
                    GameObject.FindGameObjectWithTag("InventoryToggle").GetComponent<InventoryToggle>().Pulse();
                break;
            }
        }
    }
}
