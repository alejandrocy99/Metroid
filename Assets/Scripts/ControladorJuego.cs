using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControladorJuego : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo; // Tiempo máximo para el temporizador
    private float tiempoActual; // Tiempo actual en el temporizador
    private bool tiempoActivado = false; // Indica si el temporizador está activado o no
    private TextMeshProUGUI textMeshT; // Referencia al componente TextMeshProUGUI para mostrar el tiempo

    private void Start()
    {
        Debug.Log("hola"); // Mensaje para confirmar que el script se ha iniciado
        textMeshT = GetComponent<TextMeshProUGUI>(); // Obtiene el componente TextMeshProUGUI
        ACtivarTemporizador(); // Activa el temporizador al inicio (opcional, dependiendo del juego)
    }

    private void Update()
    {
        if (tiempoActivado)
        {
            CambiarContador(); // Reduce el tiempo si el temporizador está activo
            textMeshT.text = "TIME: " + Mathf.Max(0, Mathf.RoundToInt(tiempoActual)).ToString(); // Actualiza el texto del temporizador
        }
    }

    // Resta el tiempo actual y gestiona el estado del temporizador
    private void CambiarContador()
    {
        tiempoActual -= Time.deltaTime; // Reduce el tiempo según el tiempo transcurrido

        if (tiempoActual <= 0) // Si el tiempo llega a 0, detiene el temporizador
        {
            tiempoActual = 0; // Asegura que no haya valores negativos
            tiempoActivado = false; // Detiene el temporizador
            Debug.Log("derrota"); // Mensaje de derrota (puedes agregar lógica adicional aquí)
        }
    }

    // Activa el temporizador e inicializa el tiempo actual
    private void ACtivarTemporizador()
    {
        tiempoActual = tiempoMaximo; // Inicializa el tiempo actual al máximo
        tiempoActivado = true; // Activa el temporizador
    }

    // Desactiva el temporizador
    private void DesactivarTemporizador()
    {
        tiempoActivado = false; // Cambia el estado del temporizador a desactivado
    }
}
