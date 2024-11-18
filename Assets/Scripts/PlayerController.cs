using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float velocidad;
    public float fuerza;
    //private float xLimite = 20f;
    private Rigidbody2D fisica;
    private SpriteRenderer orientacion;
    private Animator animacionJugador;
    public int Nvidas;
    private bool vulnerable; 

    //public GameObject proyectil;


    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        orientacion = GetComponent<SpriteRenderer>();
        animacionJugador = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        // Movimiento horizontal del jugador
        float move = Input.GetAxis("Horizontal");
        //transform.Translate(move, 0f, 0f);
        fisica.velocity = new Vector2(move * velocidad, fisica.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (TocarSuelo()) fisica.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);
        }
        //modifica el flix en funcion a donde mira
        if (fisica.velocity.x < 0f) orientacion.flipX = true;
        else if (fisica.velocity.x > 0f) orientacion.flipX = false;

        AnimarJugador();
        // Limitar el movimiento izquierda-derecha
        //Vector3 posicion = transform.position;
        //posicion.x = Mathf.Clamp(posicion.x, -xLimite, xLimite);
        //transform.position = posicion;

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(proyectil, new Vector3(posicion.x, posicion.y, posicion.z), Quaternion.identity);            
        }*/
    }


    private bool TocarSuelo()
    {
        RaycastHit2D siTocA = Physics2D.Raycast(transform.position + new Vector3(0f, -2f, 0f), Vector2.down, 0.2f);
        return siTocA.collider != null;
    }

    public void FinDelJuego()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Time.timeScale = 0f;
    }

    private void AnimarJugador()
    {
        Debug.Log("entrando animacion");
        if (fisica.velocity.x == 0 && fisica.velocity.y == 0){
            animacionJugador.Play("jugador paradp");}
        else if (fisica.velocity.x != 0 && fisica.velocity.y == 0){
            animacionJugador.Play("jugador-corriendo");}
        else if (!TocarSuelo()){
            animacionJugador.Play("jugador-saltando");}
    }

    public void QuitarVidas(){
        if(vulnerable){
            vulnerable = false;
            Nvidas--;
        if (Nvidas == 0 )
        {
            FinDelJuego();
        }
        Invoke("HacerVunerable" , 1f);
        orientacion.color = Color.red;
        }
        
    }


    private void HacerVunerable(){
        vulnerable = true;
        orientacion.color = Color.white;
    }

}











