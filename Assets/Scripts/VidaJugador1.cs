using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VidaJugador1 : MonoBehaviour
{
    public int vidaActual;
    public int vidaMax;

    public UnityEvent<int> cambioVida;

    void Start()
    {
        vidaActual = vidaMax;

        if (cambioVida != null)
        {
            cambioVida.Invoke(vidaActual);
        }
        else
        {
            Debug.LogError("cambioVida no está inicializado.");
        }
    }

    public void TomarDaño(int cantidadDaño)
    {
        int vidaTemporal = vidaActual - cantidadDaño;

        vidaActual = Mathf.Max(vidaTemporal, 0);

        if (cambioVida != null)
        {
            cambioVida.Invoke(vidaActual);
        }

        if (vidaActual <= 0)
        {
            Destroy(gameObject);
            Time.timeScale = 0f;
        }
    }

    public void CurarVida(int curacion)
    {
        int vidaTemporal = vidaActual + curacion;

        vidaActual = Mathf.Min(vidaTemporal, vidaMax);

        if (cambioVida != null)
        {
            cambioVida.Invoke(vidaActual);
        }
    }
}
