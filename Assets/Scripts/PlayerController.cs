using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("movimiento")]
    private float movimientoHorizontal;
    [SerializeField] private float velocidadMovimiento;
    [Range(0,0.3f)][SerializeField] private float suavizadorDeMovimiento;
    private Vector3 velocidad  =Vector3.zero;
    private bool mirandoDerecha;


    [Header("salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimesionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;








    private Animator animacionJugador;
    public int Nvidas;
    private bool vulnerable;

    //public GameObject proyectil;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animacionJugador = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        movimientoHorizontal = Input.GetAxis("Horizontal") * velocidadMovimiento;
        animacionJugador.SetFloat("Horizontal",Mathf.Abs(movimientoHorizontal));
        if(Input.GetButtonDown("Jump")){
            salto =  true;
        }
        
    }

    private void FixedUpdate(){
        // es suelo mientra la caja que hemos creado toque el suelo
        enSuelo  =Physics2D.OverlapBox(controladorSuelo.position,dimesionesCaja,0f,queEsSuelo);
        //le decimos a animador que es en suelo
        animacionJugador.SetBool("enSuelo", enSuelo);
        //Mover
        Mover(movimientoHorizontal * Time.fixedDeltaTime,salto);

        salto = false;
    }

    private void Mover(float mover,bool saltar){
        //esto es para que cuando salte o caiga tenga la misma velocidad
        Vector2 velocidadObjeto = new Vector2(mover,rb2D.velocity.y);
        //suavizar el moviento a la hora de acelerar o frenar
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity,velocidadObjeto,ref velocidad,suavizadorDeMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }else if(mover < 0 && mirandoDerecha)
        {
            Girar();
        }
        // si estamos en el suelo y presionamos saltar que salte
        if(enSuelo && saltar){
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f,fuerzaSalto));
            animacionJugador.Play("jugador-saltando");
        }
    }

    private void Girar(){
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }



    public void FinDelJuego()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Time.timeScale = 0f;
    }

   

    public void QuitarVidas()
    {
        if (vulnerable)
        {
            vulnerable = false;
            Nvidas--;
            if (Nvidas == 0)
            {
                FinDelJuego();
            }
            Invoke("HacerVunerable", 1f);
        //    rb2D. = Color.red;
        }

    }


    private void HacerVunerable()
    {
        vulnerable = true;
      //  orientacion.color = Color.white;
    }


    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position,dimesionesCaja);
    }

}











