using UnityEngine;

public class CameraShaker
{
    public bool IsShaking { get { return elapsed < duration; } }

    private float duration;
    private float magnitude;
    private float speed;
    private float elapsed;

    /// <summary>
    /// Set parameters for shake. When NextShake is called offsets will be based on these.
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="magnitude"></param>
    /// <param name="speed"></param>
    public void InitNewShake(float duration, float magnitude, float speed)
    {
        this.duration = duration;
        this.magnitude = magnitude;
        this.speed = speed;
        elapsed = 0f;
    }

    public void StopShake()
    {
        duration = 0f;
    }

    /// <summary>
    /// Should be called from Update method.
    /// Returns X, Y offsets for the next shake
    /// </summary>
    /// <returns></returns>
    public Vector3 NextShake()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float xOffset = Mathf.PerlinNoise(Time.time * speed, 0.0f) * 2.0f - 1.0f;
            float yOffset = Mathf.PerlinNoise(Time.time * speed, 0.0f) * 2.0f - 1.0f;

            xOffset *= magnitude * damper;
            yOffset *= magnitude * damper;

            return new Vector3(xOffset, yOffset);
        }

        return Vector3.zero;
    }
}

