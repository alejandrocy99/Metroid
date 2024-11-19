using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CorazonesUI : MonoBehaviour
{
    public List<Image> listaCorazones = new List<Image>();
    public GameObject corazonePrefabs;
    public VidaJugador1 vidaJugador;
    public int indexActual;
    public Sprite corazonLleno;
    public Sprite corazonVacio;

    private void Awake()
    {
        if (vidaJugador == null)
        {
            Debug.LogError("El componente VidaJugador1 no está asignado en CorazonesUI.");
            return;
        }

        if (vidaJugador.cambioVida == null)
        {
            vidaJugador.cambioVida = new UnityEvent<int>();
        }

        vidaJugador.cambioVida.AddListener(CambiaCorazones);
    }

    private void CambiaCorazones(int vidaActual)
    {
        if (listaCorazones == null || !listaCorazones.Any())
        {
            CrearCorazones(vidaJugador.vidaMax); // Cambiar según vida máxima.
        }
        else
        {
            CambiarVida(vidaActual);
        }
    }

    private void CambiarVida(int vidaActual)
    {
        for (int i = 0; i < listaCorazones.Count; i++)
        {
            listaCorazones[i].sprite = i < vidaActual ? corazonLleno : corazonVacio;
        }
    }

    private void CrearCorazones(int cantidadMaximaVida)
    {
        for (int i = 0; i < cantidadMaximaVida; i++)
        {
            GameObject corazon = Instantiate(corazonePrefabs, transform);
            Image corazonImage = corazon.GetComponent<Image>();

            if (corazonImage == null)
            {
                Debug.LogError("El prefab de corazón no tiene un componente Image.");
                continue;
            }

            listaCorazones.Add(corazonImage);
        }
        indexActual = cantidadMaximaVida - 1;
    }
}
