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
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        float controllerX = Input.GetAxis("RightStickX") * controllerSensitivity * Time.deltaTime;
        float controllerY = Input.GetAxis("RightStickY") * controllerSensitivity * Time.deltaTime;

        float finalX = mouseX + controllerX;
        float finalY = mouseY + controllerY;

        _rotationY += finalX;
        _rotationX -= finalY;

        _rotationX = Mathf.Clamp(_rotationX, minVerticalAngle, maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
    }
}