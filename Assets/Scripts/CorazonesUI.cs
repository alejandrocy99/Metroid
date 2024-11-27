using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CorazonesUI : MonoBehaviour
{
    public List<Image> listaCorazones = new List<Image>();
    public GameObject corazonePrefabs;
    public VidaJugador1 vidaJugador;
    public int indexActual;
    public Sprite corazonLleno;
    public Sprite corazonVacio;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        InitializeHearts();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Metroid2.0")
        {
            Debug.Log("Escena Metroid2.0 cargada, inicializando corazones.");
            gameObject.SetActive(true);
            InitializeHearts();
        }
        else
        {
            Debug.Log("Escena no válida para CorazonesUI, desactivando.");
            gameObject.SetActive(false);
        }
    }

    private void InitializeHearts()
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

        // Generar los corazones según la vida actual
        CambiaCorazones(vidaJugador.vidaActual);
    }

    private void CambiaCorazones(int vidaActual)
    {
        Debug.Log("Actualizando corazones. Vida actual: " + vidaActual);

        if (listaCorazones == null || listaCorazones.Count == 0)
        {
            CrearCorazones(vidaJugador.vidaMax);
        }

        CambiarVida(vidaActual);
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
        Debug.Log("Creando corazones: " + cantidadMaximaVida);

        // Limpiar corazones existentes si hay alguno
        foreach (var corazon in listaCorazones)
        {
            Destroy(corazon.gameObject);
        }
        listaCorazones.Clear();

        for (int i = 0; i < cantidadMaximaVida; i++)
        {
            GameObject corazon = Instantiate(corazonePrefabs, transform);

            if (corazon == null)
            {
                Debug.LogError("No se pudo instanciar el prefab del corazón.");
                continue;
            }

            Image corazonImage = corazon.GetComponent<Image>();

            if (corazonImage == null)
            {
                Debug.LogError("El prefab del corazón no tiene un componente Image.");
                continue;
            }

            listaCorazones.Add(corazonImage);
            Debug.Log("Corazón creado: " + (i + 1));
        }

        indexActual = cantidadMaximaVida - 1;
    }
}
