using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objetivo;
    private Quaternion rotacionInicial;

    void Start()
    {
        rotacionInicial = transform.rotation;
    }

    void Update()
    {
        if (objetivo != null)
        {
            transform.LookAt(objetivo);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionInicial, Time.deltaTime * 2f);
        }
    }
}
