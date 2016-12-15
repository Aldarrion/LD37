using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Fungus;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder Instance { get; private set; }
    public bool IsHolding { get; private set; }
    public Flowchart GameFlow;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartHolding(Item item)
    {
        IsHolding = true;
        GetComponent<SpriteRenderer>().sprite = item.Sprite;
        gameObject.AddComponent<BoxCollider2D>();
        MoveToCursor();
        SantaController.controller.changeClothes(item.Name, false);
    }

    public void StopHolding(bool destroy = false)
    {
        IsHolding = false;
        if (destroy)
        {
            Destroy(GetComponentInChildren<ItemData>().gameObject);
        }
        else
        {
            GetComponentInChildren<ItemData>().ReturnToInventory();
        }
        transform.position = new Vector3(-5000, -5000, 5000);
        GetComponent<SpriteRenderer>().sprite = null;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
    }

    void Update()
    {
        if (IsHolding)
        {
            // Don't care where we are, right click returns item to inventory
            if (Input.GetMouseButtonDown(1))
            {
                StopHolding(false);
                return;
            }
            else
            {
                //change color
                GetComponent<SpriteRenderer>().color = Color.white;

                Item item = GetComponentInChildren<ItemData>().Item;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var objects = Physics2D.RaycastAll(ray.origin, ray.direction);

                foreach (var obj in objects)
                {
                    Trap t = obj.collider.gameObject.GetComponent<Trap>();
                    if (t != null)
                    {
                        if (t.CanUseOnSelf(item))
                        {
                            //change color
                            GetComponent<SpriteRenderer>().color = Color.yellow;
                            if (Input.GetMouseButton(0))
                            {
                                t.UseOnSelf();
                                StopHolding(true);
                                GameFlow.SendFungusMessage("PutItem");
                                SantaController.controller.ComeCloserToObj(obj.collider.gameObject.transform.position, "Use" + item.Name);
                                return;
                            }
                        }
                    }
                }
            }

            MoveToCursor();
        }
    }

    void MoveToCursor()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, -2.1f);
    }

}
