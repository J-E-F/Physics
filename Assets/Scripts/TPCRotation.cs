using UnityEngine;

public class ThirdPersonCameraTarget : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f;

    [Header("Controller Settings")]
    public float controllerSensitivity = 100f;

    [Header("Rotation Limits")]
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 60f;

    private float _rotationX = 0f;
    private float _rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. Get Mouse Input (Frame-rate independent by default)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 2. Get Controller Right Stick Input (Multiplied by Time.deltaTime for smooth tracking)
        // Note: "RightStickX" and "RightStickY" must match your Input Manager names precisely
        float controllerX = Input.GetAxis("RightStickX") * controllerSensitivity * Time.deltaTime;
        float controllerY = Input.GetAxis("RightStickY") * controllerSensitivity * Time.deltaTime;

        // 3. Combine Inputs
        float finalX = mouseX + controllerX;
        float finalY = mouseY + controllerY;

        // 4. Calculate Pitch (Up/Down) and Yaw (Left/Right)
        _rotationY += finalX;
        _rotationX -= finalY; // Inverted so pushing up looks up

        // Clamp vertical look to stop camera flipping upside down
        _rotationX = Mathf.Clamp(_rotationX, minVerticalAngle, maxVerticalAngle);

        // 5. Apply rotation directly to the camera target object
        transform.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
    }
}