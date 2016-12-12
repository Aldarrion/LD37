using UnityEngine;
using System.Collections;

public class RoomController : MonoBehaviour {
    public static RoomController controller = null;

    public bool isSoundOn;
    public bool isUI = false;
    public float lastUITime = -1f;

    public Transform fallStartPoint;
    public Transform fallEndPoint;

    //Debug
    public bool fallSwitch = false;

    public void scenarioStart() {
        SantaController.controller.transform.position = fallStartPoint.position;
        SantaController.controller.FallTo(fallEndPoint);
    }

    void Awake()
    {
        if (controller == null)
        {
            controller = this;
            GetComponent<AudioSource>().enabled = isSoundOn;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        GetComponent<AudioSource>().enabled = isSoundOn;
    }

    void Update() {
        if (fallSwitch)
        {
            fallSwitch = false;
            scenarioStart();
        }
    }
}
