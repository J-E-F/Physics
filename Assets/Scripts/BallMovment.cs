using UnityEngine;

public class BallMovment : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 2f;

    [SerializeField] private float addMass = 10f;

    public float speedofBall = 0;

    Vector3 lastPosition = Vector3.zero;

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

        rb.AddForce(movement * speed);

        Debug.Log(moveHorizontal);
        Debug.Log(moveVertical);
        Debug.Log(movement);
    }
    private void changeMass()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            addMass += 1f * 9.81f;
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
