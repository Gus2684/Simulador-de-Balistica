using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System.Collections.Generic;
using System.Threading.Tasks;

[System.Serializable]
public class DatosDisparo
{
    public float anguloH;
    public float anguloV;
    public float fuerza;
    public float masa;
    public bool acierto;
    public float distancia;
    public int cajasAfectadas;
}

[System.Serializable]
public class HistorialUGS
{
    public List<DatosDisparo> disparos = new List<DatosDisparo>();
}

public class UGSManager : MonoBehaviour
{
    public static UGSManager Instancia;

    public HistorialUGS historial = new HistorialUGS();

    [HideInInspector] public List<DatosDisparo> disparosSesion = new List<DatosDisparo>();

    async void Awake()
    {
        if (Instancia != null)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);

        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await CargarDatosNube();
        }
    }

    public async void GuardarResultado(DatosDisparo datos)
    {
        historial.disparos.Add(datos);

        disparosSesion.Add(datos);

        if (disparosSesion.Count > 4)
        {
            disparosSesion.RemoveAt(0);
        }

        string json = JsonUtility.ToJson(historial);
        var data = new Dictionary<string, object> { { "historial_balistica", json } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    }

    public async Task CargarDatosNube()
    {
        var loadedData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "historial_balistica" });

        if (loadedData.TryGetValue("historial_balistica", out var item))
        {
            string json = item.Value.GetAs<string>();
            historial = JsonUtility.FromJson<HistorialUGS>(json);
        }

        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.ActualizarTextoHistorial();
        }
    }
}
