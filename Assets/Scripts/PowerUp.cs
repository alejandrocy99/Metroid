using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private GameObject efecto; // Efecto visual al recoger el power-up (opcional)
    [SerializeField] public int cantidadPuntos; // Puntos que otorga este power-up
    [SerializeField] private Puntuacion puntuacion;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;// Referencia al sistema de puntuación

    private ControladorJuego controladorJuego; // Referencia al controlador del juego
   // private bool recogido = false; // Evita que el power-up se recoja más de una vez

    private void Start()
    {
        // Busca el ControladorJuego en la escena
        controladorJuego = FindObjectOfType<ControladorJuego>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el jugador colisiona y el power-up no ha sido recogido aún
        if (other.CompareTag("Player") )
        {
           
            audioSource.PlayOneShot(audioClip);
            // Agrega los puntos al sistema de puntuación
            puntuacion.SumarPuntos(cantidadPuntos);

            // Genera un efecto visual, si está configurado
            

            // Notifica al ControladorJuego que este power-up ha sido recogido
            controladorJuego.EliminarPowerUp(gameObject);

            // Elimina el objeto del juego completamente
             // Espera a que el audio termine antes de destruir el objeto
            Destroy(gameObject, audioClip.length);
        }
    }
}
