using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;

    [Header("UI del Reporte")]
    public GameObject panelReporte;
    public TextMeshProUGUI textoReporte;

    private int cajasDerribadas = 0;

    private float repTiempo;
    private Vector3 repPunto;
    private float repVel;
    private float repImpulso;
    private bool reporteActivo = false;

    void Awake()
    {
        Instancia = this;
    }

    public void SumarCajaDerribada()
    {
        cajasDerribadas++;

        if (reporteActivo)
        {
            ActualizarCartel();
        }
    }

    public void MostrarReporte(float tiempo, Vector3 punto, float velRelativa, float impulso)
    {
        repTiempo = tiempo;
        repPunto = punto;
        repVel = velRelativa;
        repImpulso = impulso;

        reporteActivo = true;
        panelReporte.SetActive(true);

        ActualizarCartel();
    }

    private void ActualizarCartel()
    {
        textoReporte.text = $"--- REPORTE DE TIRO ---\n\n" +
                            $"Tiempo de vuelo: {repTiempo:F2} seg\n" +
                            $"Punto de impacto: {repPunto}\n" +
                            $"Vel. Relativa: {repVel:F2} m/s\n" +
                            $"Impulso colisión: {repImpulso:F2} Ns\n" +
                            $"Cajas derribadas: {cajasDerribadas}";
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}