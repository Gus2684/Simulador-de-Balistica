using UnityEngine;
using TMPro;

public class BulletTracker : MonoBehaviour
{
    private GameObject prefabImpacto;
    private Vector3 posicionInicial;
    private Rigidbody rb;
    private TextMeshProUGUI cartelEstadisticas;
    private bool yaImpacto = false;

    public void Inicializar(TextMeshProUGUI cartel, GameObject prefabExplosion)
    {
        cartelEstadisticas = cartel;
        prefabImpacto = prefabExplosion;
        posicionInicial = transform.position;
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, 10f);
    }

    void Update()
    {
        if (rb != null && cartelEstadisticas != null && !yaImpacto)
        {
            float distancia = Vector3.Distance(posicionInicial, transform.position);
            float velocidad = rb.linearVelocity.magnitude;
            cartelEstadisticas.text = $"En vuelo...\nDistancia: {distancia:F2} m\nVelocidad: {velocidad:F2} m/s";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (yaImpacto) return;
        yaImpacto = true;

        AudioSource audioClip = GetComponent<AudioSource>();
        if (audioClip != null) audioClip.Play();

        if (prefabImpacto != null)
        {
            ContactPoint contacto = collision.contacts[0];
            GameObject efecto = Instantiate(prefabImpacto, contacto.point, Quaternion.LookRotation(contacto.normal));
            Destroy(efecto, 2f);
        }
    }
}
