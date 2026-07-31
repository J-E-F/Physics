using UnityEngine;
using UnityEngine.InputSystem;

public class BallMovment : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 2f;

    [SerializeField] private float addMass = 10f;

    [SerializeField] private Transform cameraFollow;

    public float speedofBall = 0;

    Vector3 lastPosition = Vector3.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate()
    {
        ballMove();

        changeMass();

        debugSpeedOfBall();

        applyGravity();
    }

    private void applyGravity()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y - addMass * Time.fixedDeltaTime, rb.linearVelocity.z);
    }

    private void ballMove()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Vector3 cameraForward = cameraFollow.forward;
        Vector3 cameraRight = cameraFollow.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 movementDirection = (cameraForward * moveVertical + cameraRight * moveHorizontal);

        if (movementDirection.magnitude > 0.1f)
        {
            movement = movementDirection;
        }
        else
        {
            movement = Vector3.zero;
        }

        rb.AddForce(movement * speed);

        Debug.Log(moveHorizontal);
        Debug.Log(moveVertical);
        Debug.Log(movement);
    }
    private void changeMass()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            addMass += 0.3f * 9.81f;
        }
        else
        {
            addMass = 10f;
        }
        rb.mass = addMass;
    }

    private void debugSpeedOfBall()
    {
        speedofBall = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
        //Debug.Log(speedofBall);
    }
}
