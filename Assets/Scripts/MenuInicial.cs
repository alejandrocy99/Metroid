using UnityEngine;
using UnityEngine.SceneManagement; // Para manejar las escenas

public class MenuInicial : MonoBehaviour
{
    // Método para iniciar el juego
    public void IniciarJuego()
    {
          Debug.Log("El juego debería iniciar.");
        // Cargar la escena del juego (asegúrate de configurar el nombre correcto)
        SceneManager.LoadScene("Metroid2.0");
    }

    // Método para salir del juego
    public void SalirJuego()
    {
        // Cierra la aplicación
        Application.Quit();
        Debug.Log("El juego se cerrará (solo funciona en una build).");
    }
}
