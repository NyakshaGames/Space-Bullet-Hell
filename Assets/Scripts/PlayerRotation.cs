using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 10f; // Controls smooth rotation
    // If true, default to mouse aiming; if false, always use keyboard aiming.
    public bool useMouseAiming = true;

    void Update()
    {
        // Toggle the default aiming mode with the T key.
        if (Input.GetKeyDown(KeyCode.T))
        {
            useMouseAiming = !useMouseAiming;
            Debug.Log("Default Aiming Mode: " + (useMouseAiming ? "Mouse Aiming" : "Keyboard Aiming"));
        }

        // When using mouse aiming as default:
        if (useMouseAiming)
        {
            // Determine if keyboard input should override the mouse aim.
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 keyboardInput = new Vector3(horizontal, 0, vertical);
            bool keyboardOverride = false;

            // Use a small threshold to detect input.
            if (keyboardInput.sqrMagnitude > 0.1f)
            {
                // Check if the input is not exclusively "W" (forward only).
                // (Forward only: negligible horizontal input and vertical near 1)
                if (!(Mathf.Abs(horizontal) < 0.1f && vertical > 0.9f))
                {
                    keyboardOverride = true;
                }
            }

            if (keyboardOverride)
            {
                // Rotate toward the keyboard input direction.
                Quaternion targetRotation = Quaternion.LookRotation(keyboardInput);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                // No keyboard override—rotate toward the mouse pointer.
                RotateTowardsMouse();
            }
        }
        else
        {
            // If not using mouse aiming as default, always use keyboard aiming.
            RotateTowardsKeyboard();
        }
    }

    void RotateTowardsMouse()
    {
        // Cast a ray from the camera through the mouse pointer.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Use a plane at the player's current y position.
        Plane plane = new Plane(Vector3.up, transform.position);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            Vector3 direction = hitPoint - transform.position;
            direction.y = 0f; // Ensure rotation stays horizontal.
            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void RotateTowardsKeyboard()
    {
        // Use keyboard input to rotate.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
