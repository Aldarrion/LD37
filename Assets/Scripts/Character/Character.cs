using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public static Character Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
