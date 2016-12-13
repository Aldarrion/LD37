using UnityEngine;
using System.Collections;

public class ItemFactory : MonoBehaviour
{
    public Inventory Inventory;
    public string Name;
    public Sprite Sprite;
    public bool OnlyOne;

    void OnMouseDown()
    {
        if (GameController.Instance.IsInputEnabled)
        {
            SantaController.controller.ComeCloserToObj(gameObject.transform.position, Name);
        }
    }

    public void ProduceItem() {
        Inventory.AddItem(new Item(Name, Sprite));
        if (OnlyOne)
            Destroy(gameObject);
    }

}
