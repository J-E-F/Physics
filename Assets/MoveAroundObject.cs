using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveAroundObject : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 3.0f;
    [SerializeField]
    private float _stickSensitivity = 100f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference _lookAction;

    private float _rotationY;
    private float _rotationX;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _distanceFromTarget = 3.0f;
    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;
    [SerializeField]
    private float _smoothTime = 0.2f;
    [SerializeField]
    private Vector2 _rotationXMinMax = new Vector2(-40, 40);

    private void OnEnable()
    {
        _lookAction.action.Enable();
    }

    private void OnDisable()
    {
        _lookAction.action.Disable();
    }

    void Update()
    {
        Vector2 look = _lookAction.action.ReadValue<Vector2>();

        bool isGamepad = Gamepad.current != null && Gamepad.current.rightStick.IsActuated();

        float lookX = isGamepad ? look.x * _stickSensitivity * Time.deltaTime : look.x * _mouseSensitivity;
        float lookY = isGamepad ? look.y * _stickSensitivity * Time.deltaTime : look.y * _mouseSensitivity;

        _rotationY += lookX;
        _rotationX += lookY;

        // Apply clamping for x rotation 
        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);
        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);
        // Apply damping between rotation changes
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;
        // Subtract forward vector of the GameObject to point its forward vector to the target
        transform.position = _target.position - transform.forward * _distanceFromTarget;
    }
}