using UnityEngine;
using System.Collections;

public class SceneItem : MonoBehaviour
{
    public Inventory Inventory;
    public string Name;
    public Sprite Sprite;

    void OnMouseDown()
    {
        Inventory.AddItem(new Item(Name, Sprite));
    }
}
