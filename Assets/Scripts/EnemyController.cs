using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Configuración General")]
    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;

    [Header("Pulpo Configuración")]
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private int siguientePaso = 0;
    private bool movimientoDerecha;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (gameObject.CompareTag("Pulpo") && puntosMovimiento.Length > 0)
        {
            GirarPulpo();
        }
    }

    private void Update()
    {
        if (gameObject.CompareTag("Pulpo") && puntosMovimiento.Length > 0)
        {
            MoverPulpo();
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            MoverEnemigoBase();
        }
    }

    private void MoverPulpo()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePaso].position, velocidad * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePaso].position) < distanciaMinima)
        {
            siguientePaso += 1;
            if (siguientePaso >= puntosMovimiento.Length)
            {
                siguientePaso = 0;
            }
            GirarPulpo();
        }
    }

    private void GirarPulpo()
    {
        if (transform.position.x < puntosMovimiento[siguientePaso].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void MoverEnemigoBase()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);
        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        if (!informacionSuelo)
        {
            GirarEnemigo();
        }
    }

    private void GirarEnemigo()
    {
        movimientoDerecha = !movimientoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        if (gameObject.CompareTag("Enemy"))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
        }
    }
}
