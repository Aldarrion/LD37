using UnityEngine;

public static class CameraExtensions
{
    // Viewport corners
    private static readonly Vector3 BOT_LEFT = new Vector3(0.0f, 0.0f);
    //private static readonly Vector3 TOP_LEFT = new Vector3(0.0f, 1.0f);
    //private static readonly Vector3 BOT_RIGHT = new Vector3(1.0f, 0.0f);
    //private static readonly Vector3 TOP_RIGHT = new Vector3(1.0f, 1.0f);

    /// <summary>
    /// Returns the smallest bounding rectangle around the camera that includes it
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static Bounds OrthographicBounds(this Camera camera)
    {
        float maxX = camera.ViewportPointToRay(BOT_LEFT).origin.x;
        float maxY = camera.ViewportPointToRay(BOT_LEFT).origin.y;
        for (float x = 0f; x < 2f; x++)
        {
            for (float y = 0f; y < 2f; y++)
            {
                Ray ray = camera.ViewportPointToRay(new Vector3(x, y));
                if (ray.origin.x > maxX)
                    maxX = ray.origin.x;
                if (ray.origin.y > maxY)
                    maxY = ray.origin.y;
            }
        }

        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(
                (maxX - camera.transform.position.x) * 2,
                (maxY - camera.transform.position.y) * 2
                )
            );

        return bounds;
    }
}