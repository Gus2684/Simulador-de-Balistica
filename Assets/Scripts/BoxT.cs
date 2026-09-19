using UnityEngine;

public class BoxTarget : MonoBehaviour
{
    private bool yaContada = false; 

    void OnCollisionEnter(Collision collision)
    {
        if (!yaContada)
        {
            if (collision.gameObject.GetComponent<BulletTracker>() != null)
            {
                yaContada = true;

                if (GameManager.Instancia != null)
                {
                    GameManager.Instancia.SumarCajaDerribada();
                }
            }
        }
    }
}
