using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad;

    private void Update(){
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy") || other.CompareTag("Pulpo")){
            Destroy(gameObject); // destruye la bala
            Destroy(other.gameObject); // destruye el enemigo 
        }
    }
}
