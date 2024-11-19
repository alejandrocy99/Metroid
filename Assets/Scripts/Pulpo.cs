using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulpo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    public int numeroAleatorio;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        //EL NUMERO ALEATORIO ESTA ENTRE 0 Y EL NUMERO DE PUNTOS QUE TENGAMOS EN EL MAPA PARA QUE NO SE PASE
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();

    }

    private void Update()
    {
        //Se mueve desde la posicion en la que esta hasta  posicion aleatoria
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velocidad * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
        {
            numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
            Girar();
        }
    }
    private void Girar()
    {
        if (transform.position.x < puntosMovimiento[numeroAleatorio].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

}
