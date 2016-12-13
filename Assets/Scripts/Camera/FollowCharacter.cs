using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public Transform Character;
    
    // On what percentage of screen space should the camera start moving
    public float MoveTholdX;
    public float MoveTholdY;
    
    // Use this for initialization
    void Start()
    {
        CameraManager.Instance.MoveTo(GetCenterCamPos());
    }

    // Update is called once per frame
    void Update()
    {
        // Camera movement only on X axis
        // We want to move the camera only if the character moves close to the edge
        Bounds camBounds = Camera.main.OrthographicBounds();
        float tholdDistanceX = (camBounds.max.x - camBounds.min.x) * MoveTholdX; // How far are the illusionary edges from screen edge?
        float tholdDistanceY = (camBounds.max.y - camBounds.min.y) * MoveTholdY; // How far are the illusionary edges from screen edge?
        // Calculate the actual on screen x and y coordinates
        float minTholdX = camBounds.min.x + tholdDistanceX;
        float maxTholdX = camBounds.max.x - tholdDistanceX;
        float minTholdY = camBounds.min.y + tholdDistanceY;
        float maxTholdY = camBounds.max.y - tholdDistanceY;

        // Move the camera only when too close to the edge
        if (Character.position.x < minTholdX ||
            Character.position.x > maxTholdX ||
            Character.position.y < minTholdY ||
            Character.position.y > maxTholdY)
        {
            CameraManager.Instance.MoveTo(GetCenterCamPos());
        }
    }

    // Returns camera position aligned to player center but camera bounds cannot exceed Bounds
    private Vector3 GetCenterCamPos()
    {
        return Character.position;
    }
}
