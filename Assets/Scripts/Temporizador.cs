using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Temporizador : MonoBehaviour
{

    private float time = 60f;
    private TextMeshProUGUI textMeshT;
    private float timeFinal;

    private void Start()
    {
        
        textMeshT = GetComponent<TextMeshProUGUI>();
    }


    private void Update()
    {

        Contador();
        Debug.Log(time);
        textMeshT.text = "TIME :" + Mathf.Max(0,Mathf.RoundToInt(time)).ToString();
    }



    public float Contador()
    {
        time -= Time.deltaTime;
        
        return time;
    }
}
