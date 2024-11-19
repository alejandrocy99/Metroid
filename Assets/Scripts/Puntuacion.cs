using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntuacion : MonoBehaviour
{
    private float puntos;
    private TextMeshProUGUI textMeshP;
    private TextMeshProUGUI textMeshT;
    private Temporizador tempo;

    private void Start()
    {
        textMeshP = GetComponent<TextMeshProUGUI>();
    }


    private void Update()
    {

        textMeshP.text = puntos.ToString("0");

    }

    public void SumarPuntos(float puntosEntrada)
    {
        puntos += puntosEntrada;
        if(Time.timeScale == 0f){
            puntos = puntos + tempo.Contador();
        }
    }

    
}
