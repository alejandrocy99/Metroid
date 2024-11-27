using TMPro;
using UnityEngine;

public class MostrarPuntuacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoPuntuacion; // Referencia al objeto TextMeshPro


    

    private void Start()
    {
        // Encontrar el GestorDePuntuacion y obtener la puntuación
        GestorPuntuacion gestorDePuntuacion = FindObjectOfType<GestorPuntuacion>();
        Debug.Log("que es" + gestorDePuntuacion.PuntuacionTotal);
            float puntuacionTotal = gestorDePuntuacion.PuntuacionTotal * 10f;
        
            textoPuntuacion.text = "Puntuación Total: " + Mathf.Max(0, Mathf.RoundToInt(puntuacionTotal)).ToString();;
        
        
            
        }
    
}
