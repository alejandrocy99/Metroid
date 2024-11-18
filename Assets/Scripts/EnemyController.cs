using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //private float xLimite = 20f;

    private SpriteRenderer orientacion;
    private Animator animacionEnemy;
    [SerializeField] private float speed = 5f;  // Velocidad del enemigo
    [SerializeField] private bool moveOnX = true;  // Controla si se mueve en el eje X o Y, editable desde el Inspector
    private float direction = 1f;  // Dirección inicial (1 para adelante, -1 para atrás)
    [SerializeField] private float boundaryX = 10f;  // Límite en el eje X
    [SerializeField] private float boundaryY = 5f;  
    // Límite en el eje Y
 // Límite en el eje Y


    void Start()
    {
        
        orientacion = GetComponent<SpriteRenderer>();
        animacionEnemy = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        AnimarEnemigo();
    }

    // Método para mover el enemigo en el eje seleccionado
    private void Move()
    {
        // Movimiento en el eje X
        if (moveOnX)
        {
            transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);

            // Cambia de dirección al alcanzar el límite en X
            if (transform.position.x >= boundaryX || transform.position.x <= -boundaryX)
            {
                orientacion.flipX = true;
                direction *= -1;
                
            }
        }
        // Movimiento en el eje Y
        else
        {
            transform.position += new Vector3(0, direction * speed * Time.deltaTime, 0);

            // Cambia de dirección al alcanzar el límite en Y
            if (transform.position.y >= boundaryY || transform.position.y <= -boundaryY)
            {
                direction *= -1;
            }
        }
    }

    private void AnimarEnemigo()
    {
        Debug.Log("entrando animacion");

        if (moveOnX)
        {
            animacionEnemy.Play("cagrejo-andando");
        }
    }
}


