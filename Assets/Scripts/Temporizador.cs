using UnityEngine;
using TMPro;

public class Temporizador : MonoBehaviour
{
    [SerializeField] private float time = 60f; // Tiempo inicial del temporizador
    private TextMeshProUGUI textMeshT; // Referencia al TextMeshPro para mostrar el tiempo

    private void Start()
    {
        textMeshT = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Contador();

        // Debug para mostrar el tiempo restante en cada frame
     

        // Actualiza el texto en pantalla
        textMeshT.text = "TIME: " + Mathf.Max(0, Mathf.RoundToInt(time)).ToString();
    }

    public float Contador()
    {
        // Resta el tiempo transcurrido desde el último frame
        time -= Time.deltaTime;

        // Asegura que no sea menor que 0
        time = Mathf.Max(0, time);

        return time; // Devuelve el tiempo restante
    }

    public float TiempoRestante()
    {
        return Mathf.Max(0, time); // Devuelve el tiempo restante
    }
}
