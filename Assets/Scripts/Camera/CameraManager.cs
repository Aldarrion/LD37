using System;
using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    //-- Editor fields --//
    public BoxCollider2D Bounds;
    public float MoveSpeed;
    public float RotationSpeed;
    public bool IsOn;

    //-- Public --//
    // Singleton instance
    public static CameraManager Instance { get; private set; }

    public Vector3 TargetPosition { get; private set; }
    public Quaternion TargetRotation { get; private set; }


    //-- Private --//
    private CameraShaker shaker;
    private Camera MainCam { get { return Camera.main; } }
    private Vector3 prevShake;


    void Awake()
    {
        // This is the first awake, create singleton
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;

            if (IsOn)
            {
                // Center to player and save as target - we don't want to move
                MainCam.transform.position = ClampCamToBounds(MainCam.transform.position);
                TargetPosition = MainCam.transform.position;
                TargetRotation = MainCam.transform.rotation;
            }
            shaker = new CameraShaker();
        }
        else // Instance already exist - second manager is not allowed
        {
            Destroy(gameObject);
        }
    }


    //-- Interface --//
    public void TurnOn()
    {
        Instance.IsOn = true;
        TargetPosition = MainCam.transform.position;
        TargetRotation = MainCam.transform.rotation;
    }

    public void TurnOff()
    {
        Instance.IsOn = false;
    }

    /// <summary>
    /// Returns camera position clamped to bounds from given desired position.
    /// Does not move the camera
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector3 ClampCamToBounds(Vector3 position)
    {
        // Center the camera to the character position
        Vector3 adjustedPos = position;

        // No reason to move the camera on the z axis
        adjustedPos.z = Camera.main.transform.position.z;

        Bounds cameraBounds = Camera.main.OrthographicBounds();
        float xDist = adjustedPos.x - Camera.main.transform.position.x;
        float yDist = adjustedPos.y - Camera.main.transform.position.y;

        // Adjust the bounds to see how they would be if we moved the camera to character position
        cameraBounds.min = new Vector3(cameraBounds.min.x + xDist, cameraBounds.min.y + yDist);
        cameraBounds.max = new Vector3(cameraBounds.max.x + xDist, cameraBounds.max.y + yDist);

        // Left right cropping
        if (cameraBounds.min.x < Bounds.bounds.min.x)
            adjustedPos.x += Bounds.bounds.min.x - cameraBounds.min.x;
        else if (cameraBounds.max.x > Bounds.bounds.max.x)
            adjustedPos.x -= cameraBounds.max.x - Bounds.bounds.max.x;

        // Top bottom cropping
        if (cameraBounds.min.y < Bounds.bounds.min.y)
            adjustedPos.y += Bounds.bounds.min.y - cameraBounds.min.y;
        else if (cameraBounds.max.y > Bounds.bounds.max.y)
            adjustedPos.y -= cameraBounds.max.y - Bounds.bounds.max.y;

        return adjustedPos;
    }

    /// <summary>
    /// Call if you want the screen to shake
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="magnitude"></param>
    /// <param name="speed"></param>
    public void Shake(float duration = 1f, float magnitude = 0.15f, float speed = 25f)
    {
        shaker.InitNewShake(duration, magnitude, speed);
    }

    /// <summary>
    /// Smoothly moves the camera to given position
    /// </summary>
    /// <param name="target"></param>
    public void MoveTo(Vector3 target)
    {
        TargetPosition = ClampCamToBounds(target);
    }

    /// <summary>
    /// Smoothly rotates the camera around the Z axis by given amount
    /// </summary>
    /// <param name="amount"></param>
    public void Rotate(float amount)
    {
        TargetRotation = Quaternion.Euler(0, 0, amount);
        TargetPosition = ClampCamToBounds(TargetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOn)
        {
            if (prevShake != Vector3.zero)
                RevertShake();

            if (TargetRotation.eulerAngles.z != MainCam.transform.rotation.eulerAngles.z ||
                Vector3.Distance(MainCam.transform.position, TargetPosition) > 0.05f)
            {
                // Rotation may cause movement not the other way around - rotation first
                ApplyRotation();
                ApplyMovement();
            }

            MainCam.transform.position = ClampCamToBounds(MainCam.transform.position);

            if (shaker.IsShaking)
            {
                ApplyShake();
            }
            else
            {
                prevShake = Vector2.zero;
            }
        }
    }


    #region PrivateMethods
    //-- Private methods --//
    private void RevertShake()
    {
        MainCam.transform.position -= prevShake;
    }
    private void ApplyShake()
    {
        prevShake = shaker.NextShake();
        MainCam.transform.position += prevShake;
    }

    private void ApplyRotation()
    {
        MainCam.transform.rotation = Quaternion.RotateTowards(MainCam.transform.rotation, TargetRotation, RotationSpeed * Time.deltaTime);
    }
    private void ApplyMovement()
    {
        MainCam.transform.position = Vector3.MoveTowards(MainCam.transform.position, TargetPosition, MoveSpeed * Time.deltaTime);
    }
    #endregion

}
