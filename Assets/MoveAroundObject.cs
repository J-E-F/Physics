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

    public LayerMask collisionMask;
    public LayerMask playerLayer;

    [Header("Collision Smoothing")]
    [SerializeField] private float _sphereRadius = 0.3f;
    [SerializeField] private float _safetyOffset = 0.2f;
    [SerializeField] private float _minDistanceFromTarget = 0.8f;
    [SerializeField] private float _collisionSmoothSpeed = 15f;
    private float _currentSmoothDistance;

    private void OnEnable()
    {
        _lookAction.action.Enable();
    }

    private void OnDisable()
    {
        _lookAction.action.Disable();
    }

    void Start()
    {
        _currentSmoothDistance = _distanceFromTarget;
    }

    void Update()
    {
        Vector2 look = _lookAction.action.ReadValue<Vector2>();

        bool isGamepad = Gamepad.current != null && Gamepad.current.rightStick.IsActuated();

        float lookX = isGamepad ? look.x * _stickSensitivity * Time.deltaTime : look.x * _mouseSensitivity;
        float lookY = isGamepad ? look.y * _stickSensitivity * Time.deltaTime : look.y * _mouseSensitivity;

        _rotationY += lookX;
        _rotationX -= lookY;

        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);
        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetIdealPosition = _target.position - (transform.forward * _distanceFromTarget);
        Vector3 rayDirection = (targetIdealPosition - _target.position).normalized;
        float desiredDistance = _distanceFromTarget;

        if (Physics.SphereCast(_target.position, _sphereRadius, rayDirection, out RaycastHit hitEnvironment, _distanceFromTarget, collisionMask))
        {
            desiredDistance = Mathf.Clamp(hitEnvironment.distance - _safetyOffset, _minDistanceFromTarget, _distanceFromTarget);
        }
        else
        {
            desiredDistance = Mathf.Max(_minDistanceFromTarget, _distanceFromTarget);
        }

        _currentSmoothDistance = Mathf.Lerp(_currentSmoothDistance, desiredDistance, Time.deltaTime * _collisionSmoothSpeed);
        transform.position = _target.position - (transform.forward * _currentSmoothDistance);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _sphereRadius);
        Gizmos.DrawLine(transform.position, _target.transform.position);
    }
}