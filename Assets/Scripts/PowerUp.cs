using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private GameObject efecto;
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntuacion puntuacion;
    

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            puntuacion.SumarPuntos(cantidadPuntos);
            //Instantiate(efecto,transform.position,Quaternion.identity);
            Destroy(gameObject);
            Time.timeScale = 0f;
        }
    }
}
