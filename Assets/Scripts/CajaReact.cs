using UnityEngine;

public class CajaReactiva : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (rb != null)
        {
            rb.useGravity = true;
        }
    }
}
