using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder Instance { get; private set; }
    public bool IsHolding { get; private set; }

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
        MoveToCurosr();
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
                StopHolding();
                return;
            }
            else if(Input.GetMouseButton(0))
            {
                Item item = GetComponentInChildren<ItemData>().Item;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var objects = Physics2D.RaycastAll(ray.origin, ray.direction);

                foreach (var obj in objects)
                {
                    Trap t = obj.collider.gameObject.GetComponent<Trap>();
                    if (t != null)
                    {
                        if(t.UseOnSelf(item))
                        {
                            StopHolding(true);
                            return;
                        }
                    }
                }
            }

            MoveToCurosr();
        }
    }

    void MoveToCurosr()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, -2.1f);
    }
}
