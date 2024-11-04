using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float velocidad;
    public float fuerza;
    private float xLimite = 20f;
    private Rigidbody2D fisica;
    private SpriteRenderer orientacion;
    //public GameObject proyectil;


    void Start(){
        fisica = GetComponent<Rigidbody2D>();
        orientacion = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        // Movimiento horizontal del jugador
        float move = Input.GetAxis("Horizontal");
        //transform.Translate(move, 0f, 0f);
        fisica.velocity = new Vector2(move * velocidad,fisica.velocity.y);
        
        if(Input.GetKeyDown(KeyCode.Space)){
            
            if(TocarSuelo()) fisica.AddForce(Vector2.up * fuerza,ForceMode2D.Impulse); 
        }
    //modifica el flix en funcion a donde mira
        if (fisica.velocity.x < 0f)  orientacion.flipX = true;
        else if(fisica.velocity.x > 0f) orientacion.flipX = false; 
        // Limitar el movimiento izquierda-derecha
        Vector3 posicion = transform.position;
        posicion.x = Mathf.Clamp(posicion.x, -xLimite, xLimite);
        transform.position = posicion;

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(proyectil, new Vector3(posicion.x, posicion.y, posicion.z), Quaternion.identity);            
        }*/
    }


    private bool TocarSuelo(){
        RaycastHit2D siTocA = Physics2D.Raycast(transform.position + new Vector3(0f,-2f,0f),Vector2.down,0.2f);
         return siTocA.collider != null;
    }
}
