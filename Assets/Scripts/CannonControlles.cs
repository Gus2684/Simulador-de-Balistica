using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonController : MonoBehaviour
{
    [Header("Partes del Cañón")]
    public Transform baseCanon;
    public Transform barrilCanon;
    public Transform firePoint;

    [Header("Balística y Efectos")]
    public GameObject prefabBala;
    public GameObject prefabExplosion;

    [Header("UI - Controles")]
    public Slider sliderHorizontal;
    public Slider sliderVertical;
    public Slider sliderFuerza;
    public TextMeshProUGUI textoEstadisticas;

    void Start()
    {
        sliderHorizontal.minValue = -60f;
        sliderHorizontal.maxValue = 60f;

        sliderVertical.minValue = -10f;
        sliderVertical.maxValue = 45f;

        sliderFuerza.minValue = 10f;
        sliderFuerza.maxValue = 100f;
    }

    void Update()
    {
        baseCanon.localRotation = Quaternion.Euler(0, sliderHorizontal.value, 0);
        barrilCanon.localRotation = Quaternion.Euler(-sliderVertical.value, 0, 0);
    }

    public void Disparar()
    {
        GameObject bala = Instantiate(prefabBala, firePoint.position, firePoint.rotation);

        Rigidbody rbBala = bala.GetComponent<Rigidbody>();
        rbBala.AddForce(firePoint.forward * sliderFuerza.value, ForceMode.Impulse);

        BulletTracker tracker = bala.AddComponent<BulletTracker>();
        tracker.Inicializar(textoEstadisticas, prefabExplosion);

        if (Camera.main.GetComponent<CameraFollow>() != null)
        {
            Camera.main.GetComponent<CameraFollow>().objetivo = bala.transform;
        }
    }
}

