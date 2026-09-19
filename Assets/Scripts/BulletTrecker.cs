using UnityEngine;
using TMPro;

public class BulletTracker : MonoBehaviour
{
    private GameObject prefabImpacto;
    private Vector3 posicionInicial;
    private Rigidbody rb;
    private TextMeshProUGUI cartelEstadisticas;
    private bool yaImpacto = false;

    private float tiempoInicio;

    public void Inicializar(TextMeshProUGUI cartel, GameObject prefabExplosion)
    {
        cartelEstadisticas = cartel;
        prefabImpacto = prefabExplosion;
        posicionInicial = transform.position;
        rb = GetComponent<Rigidbody>();
        tiempoInicio = Time.time; 

        Destroy(gameObject, 10f);
    }

    void Update()
    {
        if (rb != null && cartelEstadisticas != null && !yaImpacto)
        {
            float distancia = Vector3.Distance(posicionInicial, transform.position);
            float velocidad = rb.linearVelocity.magnitude;
            cartelEstadisticas.text = $"Vuelo...\nDist: {distancia:F2} m\nVel: {velocidad:F2} m/s";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (yaImpacto) return;
        yaImpacto = true;

        float tiempoVuelo = Time.time - tiempoInicio;
        Vector3 puntoImpacto = collision.contacts[0].point;
        float velRelativa = collision.relativeVelocity.magnitude;
        float impulso = collision.impulse.magnitude;

        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.MostrarReporte(tiempoVuelo, puntoImpacto, velRelativa, impulso);
        }

        AudioSource audioClip = GetComponent<AudioSource>();
        if (audioClip != null) audioClip.Play();

        if (prefabImpacto != null)
        {
            GameObject efecto = Instantiate(prefabImpacto, puntoImpacto, Quaternion.LookRotation(collision.contacts[0].normal));
            Destroy(efecto, 2f);
        }
    }
}
