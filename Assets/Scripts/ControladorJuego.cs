using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorJuego : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerUps; // Lista de power-ups manualmente añadida


    private int puntosPowerUps = 0; // Puntos totales de los power-ups
    private Temporizador temporizador; // Referencia al temporizador

    private void Start()
    {
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
            // Obtener el tiempo restante del temporizador
            float tiempoRestante = temporizador.TiempoRestante();

            Debug.Log("Tiempo restante para cálculo: " + tiempoRestante);

            // Calcular puntos totales
            float puntuacionTotal = puntosPowerUps + tiempoRestante;

            Debug.Log("Puntuación total calculada: " + puntuacionTotal);
            
            GestorPuntuacion gestor = FindObjectOfType<GestorPuntuacion>();
            gestor.PuntuacionTotal = puntuacionTotal;
            // Puedes pasar esta puntuación a la siguiente escena
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
