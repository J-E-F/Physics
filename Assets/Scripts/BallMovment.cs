using UnityEngine;

public class BallMovment : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float airSpeed = 100;
    [SerializeField] private float steeringForce = 3f;

    [SerializeField] private float addMass = 10f;

    [SerializeField] private Transform cameraFollow;
    [SerializeField] private float massOfBall;

    public cameraFollow cameraFollowScript;


    public float speedofBall;

    Vector3 lastPosition = Vector3.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate()
    {
        ballMove();

        debugSpeedOfBall();

        applyGravity();
    }
    private void Update()
    {
        changeMass();
        checkIfAssendingOrDecending();
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

        if (cameraFollowScript.isGrounded==true)
        {
            if (movementDirection.magnitude > 0.1f)
            {
                movementDirection.Normalize();

                if (rb.linearVelocity.magnitude > 0.1f)
                {
                    Vector3 currentVelocity = rb.linearVelocity;
                    currentVelocity.y = 0f;

                    Vector3 alignedVelocity = Vector3.Project(currentVelocity, movementDirection);

                    Vector3 sidewaysVelocity = currentVelocity - alignedVelocity;

                    rb.AddForce(-sidewaysVelocity * steeringForce, ForceMode.Acceleration);

                    if (Vector3.Dot(currentVelocity, movementDirection) < 0.1f)
                    {
                        rb.AddForce(-alignedVelocity * steeringForce, ForceMode.Acceleration);
                    }
                }
                rb.AddForce(movementDirection * speed, ForceMode.Acceleration);
            }
            /*else
            {
                Vector3 brake = -rb.linearVelocity;
                brake.y = 0f;
                rb.AddForce(brake * 2f, ForceMode.Acceleration);
            }*/
        }
        else
        {
            rb.AddForce(movement * airSpeed);
        }
    }
    private void changeMass()
    {
        if (Input.GetButton("Fire1")||Input.GetKey(KeyCode.Space))
        {
            addMass = massOfBall;
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
    private void checkIfAssendingOrDecending()
    {
        if (rb.linearVelocity.y > 0)
        {
            Debug.Log("Moving up");
        }
        else if (rb.linearVelocity.y < 0)
        {
            Debug.Log("Moving down");
        }
    }
}
