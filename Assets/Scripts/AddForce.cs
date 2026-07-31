using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject ball;

    float spawnOnce = 1f;

    public float force = 10f;

    public Material material;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //ball = GetComponent<GameObject>();
        material.SetColor("_Color", Color.red);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int randomDirectionX = Random.Range(-360, 360);
            int randomDirectionY = Random.Range(-360, 360);
            int randomDirectionZ = Random.Range(-360, 360);

            Vector3 randomDirection = new Vector3(randomDirectionX, randomDirectionY, randomDirectionZ);

            rb.AddForce(randomDirection * force, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.Q) && spawnOnce == 1f)
        {
            Instantiate(ball, new Vector3(0, 1, 0), Quaternion.identity);
            spawnOnce--;
        }
    }

}
