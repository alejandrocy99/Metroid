using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool movimientoDerecha;
    private Rigidbody2D rb;


    void Start()
    {
        //coge el rigidbody de nuestro personaje
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position,Vector2.down,distancia);
        rb.velocity = new Vector2(velocidad,rb.velocity.y);

        if(informacionSuelo == false)
        {
            Girar();
        }
    }

    private void Girar(){
        movimientoDerecha = !movimientoDerecha;
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y+180,0);
        velocidad *= -1;
    }

    private void onDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position,controladorSuelo.transform.position + Vector3.down * distancia);
    }


}


