using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public bool IsInputEnabled;

    void EnableInput()
    {
        IsInputEnabled = true;
    }

    void DisableInput()
    {
        IsInputEnabled = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

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
}
