using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform playerBall;

    public bool isGrounded;
    public LayerMask Ground;

    public Vector3 originOffset;

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        transform.position = playerBall.position;

        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit hit, 1f, Ground))
        {
            isGrounded = true;
            Debug.Log("SphereCast is hitting: " + hit.collider.name);
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 castLocation = transform.position + originOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(castLocation, 0.5f);
    }
}
