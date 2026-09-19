using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector] public Transform objetivo;

    [Header("Configuración Orbital")]
    public float distancia = 10f;
    public float sensibilidad = 3f;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    private float rotX = 0f;
    private float rotY = 15f;
    private Transform objetivoAnterior;

    void Start()
    {
        objetivo = null;

        if (distancia <= 0f)
        {
            distancia = 10f;
        }

        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void LateUpdate()
    {
        if (objetivo != null)
        {
            if (objetivo != objetivoAnterior)
            {
                rotX = objetivo.eulerAngles.y;
                rotY = 15f;
                objetivoAnterior = objetivo;
            }

            if (Input.GetMouseButton(1))
            {
                rotX += Input.GetAxis("Mouse X") * sensibilidad;
                rotY -= Input.GetAxis("Mouse Y") * sensibilidad;
                rotY = Mathf.Clamp(rotY, -10f, 80f);
            }

            Quaternion rotacionDeseada = Quaternion.Euler(rotY, rotX, 0);
            Vector3 posicionDeseada = objetivo.position - (rotacionDeseada * Vector3.forward * distancia);

            transform.position = Vector3.Lerp(transform.position, posicionDeseada, Time.deltaTime * 10f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 10f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, posicionInicial, Time.deltaTime * 2f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionInicial, Time.deltaTime * 2f);
            objetivoAnterior = null;
        }
    }
}
