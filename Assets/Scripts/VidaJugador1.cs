using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VidaJugador1 : MonoBehaviour
{
    public int vidaActual;
    public int vidaMax;

    public UnityEvent<int> cambioVida;

    private bool esInmune = false;          // Bandera para controlar inmunidad
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    [SerializeField] private float tiempoInmunidad = 1f; // Duración de inmunidad

    void Start()
    {
        vidaActual = vidaMax;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer no encontrado en el jugador.");
        }

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
        if (esInmune)
        {
            return; // No tomar daño si es inmune
        }

        int vidaTemporal = vidaActual - cantidadDaño;
        vidaActual = Mathf.Max(vidaTemporal, 0);

        if (cambioVida != null)
        {
            cambioVida.Invoke(vidaActual);
        }

        if (vidaActual > 0)
        {
            StartCoroutine(InmunidadTemporal()); // Activar inmunidad
        }
        else
        {
            // Muerte del jugador
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

    private IEnumerator InmunidadTemporal()
    {
        esInmune = true;

        // Cambiar color a rojo
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }

        // Esperar duración de inmunidad
        yield return new WaitForSeconds(tiempoInmunidad);

        // Restaurar color original
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }

        esInmune = false;
    }
}
