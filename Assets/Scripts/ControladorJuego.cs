using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorJuego : MonoBehaviour
{
    [Header("SONIDO")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [Header("Configuración de música")]
    [SerializeField] private float pitchInicial = 1.0f; // Velocidad normal
    [SerializeField] private float pitchMaximo = 2.0f;  // Velocidad máxima cuando queda poco tiempo

    [SerializeField] private List<GameObject> powerUps; // Lista de power-ups manualmente añadida

    private int puntosPowerUps = 0; // Puntos totales de los power-ups
    private Temporizador temporizador; // Referencia al temporizador

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        // Buscar el objeto Temporizador en la escena
        temporizador = FindObjectOfType<Temporizador>();

        if (temporizador == null)
        {
            Debug.LogError("No se encontró el Temporizador en la escena.");
        }
        else
        {
            Debug.Log("Temporizador encontrado correctamente.");
        }

        Debug.Log("Power-ups registrados manualmente: " + powerUps.Count);
    }

    private void Update()
    {
        // Ajustar el pitch de la música en función del tiempo restante
        if (temporizador != null && audioSource != null)
        {
            float tiempoRestante = temporizador.TiempoRestante();
            float tiempoTotal = 60f;

            if (tiempoTotal > 0)
            {
                //Debug.Log("restante" + tiempoRestante);
                //Debug.Log("total" + tiempoTotal);
                float porcentajeTiempo = tiempoRestante / tiempoTotal;
                //Debug.Log("porcetataje:" + porcentajeTiempo);
                //Debug.Log( "pitch:" + Mathf.Lerp(pitchMaximo, pitchInicial, porcentajeTiempo));
                // Aumenta el pitch cuando el tiempo disminuye
                audioSource.pitch = Mathf.Lerp(pitchMaximo, pitchInicial, porcentajeTiempo);
            }
        }
    }

    public void EliminarPowerUp(GameObject powerUp)
    {
        if (powerUps.Contains(powerUp))
        {
            powerUps.Remove(powerUp);
            puntosPowerUps += powerUp.GetComponent<PowerUp>().cantidadPuntos; // Sumar los puntos del power-up
            Debug.Log("Power-ups restantes: " + powerUps.Count);

            if (powerUps.Count == 0)
            {
                Debug.Log("Todos los power-ups recogidos. Calculando puntuación total...");
                CalcularPuntuacionTotal();
                CambiarDeEscena();
            }
        }
    }

    private void CalcularPuntuacionTotal()
    {
        if (temporizador != null)
        {
            float tiempoRestante = temporizador.TiempoRestante();
            Debug.Log("Tiempo restante para cálculo: " + tiempoRestante);

            float puntuacionTotal = puntosPowerUps + tiempoRestante;

            Debug.Log("Puntuación total calculada: " + puntuacionTotal);

            GestorPuntuacion gestor = FindObjectOfType<GestorPuntuacion>();
            gestor.PuntuacionTotal = puntuacionTotal;
        }
        else
        {
            Debug.LogError("Temporizador es NULL en CalcularPuntuacionTotal.");
        }
    }

    private void CambiarDeEscena()
    {
        SceneManager.LoadScene("PantallaFinal");
    }
}
