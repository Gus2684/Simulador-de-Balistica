using UnityEngine;

public class BoxTarget : MonoBehaviour
{
    void OnJointBreak(float breakForce)
    {
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.SumarCajaDerribada();
        }
    }
}
