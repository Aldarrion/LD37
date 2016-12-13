using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour
{
    public Texture2D icon;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        if(GameController.Instance.IsInputEnabled)
            Cursor.SetCursor(icon, hotSpot, cursorMode);
    }
    void OnMouseExit()
    {
        if (GameController.Instance.IsInputEnabled)
            Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}
