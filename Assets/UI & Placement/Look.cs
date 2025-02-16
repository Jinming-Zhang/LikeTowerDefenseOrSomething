using UnityEngine;

public class Look : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity of the mouse movement

    private float xRotation = 0f; // Store the vertical rotation

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    private void Update()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update the vertical rotation and clamp it
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation

        // Apply camera rotation
        transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y + mouseX, 0f);
    }
}
