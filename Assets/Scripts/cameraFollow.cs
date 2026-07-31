using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform playerBall;

    private void FixedUpdate()
    {
        transform.position = playerBall.position;
    }
}
