using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool movimientoDerecha;
    private Rigidbody2D rb;

    void Start()
    {
        // Coge el Rigidbody de nuestro enemigo
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);
        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        if (informacionSuelo == false)
        {
            Girar();
        }
    }

    private void Girar()
    {
        movimientoDerecha = !movimientoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta al jugador y quita vida
        if (collision.CompareTag("Player"))
        {
            VidaJugador1 vidaJugador = collision.GetComponent<VidaJugador1>();
            if (vidaJugador != null)
            {
                vidaJugador.TomarDaño(1); // Reduce 1 punto de vida al jugador
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }
}
