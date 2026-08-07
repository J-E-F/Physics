using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallMovment : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 2f;

    [SerializeField] private float addMass = 10f;

    [SerializeField] private Transform cameraFollow;
    [SerializeField] private float massOfBall;

    private Vector3 currentTransfrom;
    private Vector3 preveiousTransform; 

    public float speedofBall = 0;

    Vector3 lastPosition = Vector3.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentTransfrom = transform.position;
        preveiousTransform = currentTransfrom;
    }
    private void FixedUpdate()
    {
        ballMove();

        //debugSpeedOfBall();

        applyGravity();
    }
    private void Update()
    {
        changeMass();
        checkIfAssendingOrDecending();
        //StartCoroutine(waitALilBit());
        currentTransfrom = transform.position;
    }
    private void LateUpdate()
    {
        preveiousTransform = currentTransfrom;
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

        //Debug.Log(moveHorizontal);
        //Debug.Log(moveVertical);
        //Debug.Log(movement);
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

    private IEnumerator waitALilBit()
    {
        currentTransfrom = transform.position;
        yield return new WaitForSeconds(0.009f);
        preveiousTransform = currentTransfrom;
    }

    private void debugSpeedOfBall()
    {
        speedofBall = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
        //Debug.Log(speedofBall);
    }
    private void checkIfAssendingOrDecending()
    {
        if(currentTransfrom.y > preveiousTransform.y)
        {
            Debug.Log("Assending");
        }
        else if(currentTransfrom.y < preveiousTransform.y)
        {
            Debug.Log("Decending");
        }
        else
        {
            Debug.Log("Same Height");
        }
    }
}
