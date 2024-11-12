using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class efectoParalax : MonoBehaviour
{
    public float speed;
    private new Transform camera;
    private Vector3 ultimaPosicionCamera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        ultimaPosicionCamera = camera.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movimientoFondo = camera.position - ultimaPosicionCamera;
        transform.position += new Vector3(movimientoFondo.x * speed,movimientoFondo.y,0);
        ultimaPosicionCamera = camera.position;
    }
}
