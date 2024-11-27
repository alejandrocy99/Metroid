using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulpo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    public int siguientePaso = 0;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // El número aleatorio está entre 0 y el número de puntos que tengamos en el mapa
        //siguientePaso = Random.Range(0, puntosMovimiento.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        // Se mueve desde la posición actual hasta una posición aleatoria
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePaso].position, velocidad * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePaso].position) < distanciaMinima)
        {
           // siguientePaso = Random.Range(0, puntosMovimiento.Length);
           siguientePaso += 1;
           if(siguientePaso >= puntosMovimiento.Length){
                siguientePaso = 0;
           }
            Girar();
        }
    }

    private void Girar()
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
}
