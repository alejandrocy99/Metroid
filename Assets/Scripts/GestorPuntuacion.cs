using UnityEngine;

public class GestorPuntuacion : MonoBehaviour
{
    private float puntuacionTotal ; // Variable privada para la puntuación total

    // Propiedad para acceder a la puntuación total
    public float PuntuacionTotal
    {
        get { return puntuacionTotal; }
        set { puntuacionTotal = value; }
    }

    private void Awake()
    {
        // Asegurar que el objeto no se destruya al cambiar de escena
        DontDestroyOnLoad(gameObject);
    }
}
