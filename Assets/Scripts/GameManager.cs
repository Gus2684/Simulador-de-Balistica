using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;

    [Header("UI del Reporte (Al impactar)")]
    public GameObject panelReporte;
    public TextMeshProUGUI textoReporte;

    [Header("UI del Historial en la Nube (Siempre visible)")]
    public TextMeshProUGUI textoListaHistorial;

    private int cajasDerribadas = 0;
    private float repTiempo;
    private Vector3 repPunto;
    private float repVel;
    private float repImpulso;
    private bool reporteActivo = false;

    private float curAngH, curAngV, curFuerza, curMasa;
    private Vector3 puntoOrigen;

    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        ActualizarTextoHistorial();
    }


    public void SumarCajaDerribada()
    {
        cajasDerribadas++;
        if (reporteActivo) ActualizarCartel();
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
                            $"Cajas impactadas: {cajasDerribadas}";
    }


    public void ConfigurarDisparoActual(float h, float v, float f, float m, Vector3 origen)
    {
        curAngH = h; curAngV = v; curFuerza = f; curMasa = m; puntoOrigen = origen;
    }

    public void FinalizarYGuardarEnNube()
    {
        DatosDisparo datos = new DatosDisparo();
        datos.anguloH = curAngH;
        datos.anguloV = curAngV;
        datos.fuerza = curFuerza;
        datos.masa = curMasa;
        datos.acierto = cajasDerribadas > 0;
        datos.distancia = Vector3.Distance(puntoOrigen, repPunto);
        datos.cajasAfectadas = cajasDerribadas;

        if (UGSManager.Instancia != null)
        {
            UGSManager.Instancia.GuardarResultado(datos);
        }

        ActualizarTextoHistorial();
        Invoke("ReiniciarNivel", 2f);
    }

    public void ActualizarTextoHistorial()
    {
        if (textoListaHistorial == null) return;

        textoListaHistorial.text = "--- TIROS DE ESTA SESIÓN ---\n\n";

        if (UGSManager.Instancia != null && UGSManager.Instancia.disparosSesion.Count > 0)
        {
            var lista = UGSManager.Instancia.disparosSesion;
            for (int i = 0; i < lista.Count; i++)
            {
                var d = lista[i];
                string aciertoTxt = d.acierto ? "<color=green>Acierto</color>" : "<color=red>Fallo</color>";

                textoListaHistorial.text += $"> {aciertoTxt} | {d.distancia:F1}m | Cajas: {d.cajasAfectadas}\n";
            }
        }
        else
        {
            textoListaHistorial.text += "Esperando el primer disparo...";
        }
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}