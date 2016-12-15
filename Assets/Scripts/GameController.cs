using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public bool IsInputEnabled;
    public Texture2D icon = null;

    public void setNextCursor(Texture2D nextIon = null)
    {
        if (nextIon == null) icon = null;
        else icon = nextIon;
    }

    public void EnableInput()
    {
        IsInputEnabled = true;
        Cursor.SetCursor(icon, Vector2.zero, CursorMode.Auto);
    }

    public void DisableInput()
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

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // set the PlayMode to stop
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
