using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour
{
    public Texture2D icon;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        if (GameController.Instance.IsInputEnabled)
            Cursor.SetCursor(icon, hotSpot, cursorMode);
        else GameController.Instance.setNextCursor(icon);
    }
    void OnMouseExit()
    {
        if (GameController.Instance.IsInputEnabled)
            Cursor.SetCursor(null, hotSpot, cursorMode);
        else GameController.Instance.setNextCursor(null);
    }
}
