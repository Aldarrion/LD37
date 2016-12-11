using UnityEngine;
using System.Collections;

public class Item
{
    public string Name { get; private set; }
    public Sprite Sprite { get; private set; }

    public Item(string name, Sprite sprite)
    {
        Name = name;
        Sprite = sprite;
    }
}
