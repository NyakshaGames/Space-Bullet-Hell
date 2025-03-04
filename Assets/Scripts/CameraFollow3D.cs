using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Assign the player's transform in the Inspector

    [Header("Camera Settings")]
    // Adjust the offset to position the camera above and behind the player
    public Vector3 offset = new Vector3(0f, 20f, -10f);
    public float smoothSpeed = 0.125f; // Lower values result in smoother, slower movement

    void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the desired position based on the player's position plus an offset.
        Vector3 desiredPosition = target.position + offset;
        // Smoothly transition to the desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optionally, if you want to always look at the player:
        transform.LookAt(target);
    }
}
