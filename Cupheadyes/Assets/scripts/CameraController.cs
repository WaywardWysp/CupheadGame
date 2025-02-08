using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Follow Settings")]
    public Transform player; // Assign the player in the inspector
    public float followSpeed = 5f;
    public float horizontalOffset = 1f; // Feel free to adjust, it changes where the camera sits

    [Header("Camera Limits")]
    public bool useCameraBounds = true;
    public Vector2 minCameraBounds; // We'll end up using these to mark the start and end of the map
    public Vector2 maxCameraBounds;

    private float targetX;

    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("CameraController: No player assigned!");
            return;
        }

        // Camera follows player's position with offset
        targetX = player.position.x + horizontalOffset;

        // Apply camera boundaries
        if (useCameraBounds)
        {
            targetX = Mathf.Clamp(targetX, minCameraBounds.x, maxCameraBounds.x);
        }

        // Moves the camera, smooth as butter
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(targetX, transform.position.y, transform.position.z),
            followSpeed * Time.deltaTime
        );
    }
}
