using UnityEngine;
using System.Collections;

public class ExternalTrapTrigger : MonoBehaviour {

    public Trap trap;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Character>() != null)
        {
            trap.Trigger();
        }
    }

}
