using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocidad;
    private Vector3 posicionArriba;
    public Vector3 posicionAbajo;
    private bool subiendo;
    // Puedes definir la distancia que quieres que el enemigo suba desde su posición inicial
    public float distanciaSubida = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Posición inicial
        Vector3 posicionInicial = transform.position;

        // Establecemos la posición de subida en relación a la posición inicial
        posicionArriba = posicionInicial + new Vector3(0, distanciaSubida, 0);

        // La posición de bajada puede ser establecida desde el Inspector o aquí directamente
        if (posicionAbajo == Vector3.zero) // Opcionalmente solo si no está definida en el inspector
        {
            posicionAbajo = posicionInicial;
        }

        // Empezamos moviendo hacia arriba
        subiendo = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Determinamos hacia dónde moverse
        Vector3 posicionDestino = subiendo ? posicionArriba : posicionAbajo;

        // Movemos el objeto hacia la posición destino
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);

        // Cambiamos la dirección cuando se alcanza una de las posiciones límite
        if (transform.position == posicionArriba)
        {
            subiendo = false;
        }
        else if (transform.position == posicionAbajo)
        {
            subiendo = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D colision){
        if(colision.gameObject.CompareTag("jugador")){
            Debug.Log("1 vida menos");
            
            colision.gameObject.GetComponent<PlayerController>().FinDelJuego();
        }
    }
}
