using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    public Transform Target;
    private void Update()
    {
        transform.RotateAround(Target.position, Vector3.up, 8 * Time.deltaTime);
    }
}
