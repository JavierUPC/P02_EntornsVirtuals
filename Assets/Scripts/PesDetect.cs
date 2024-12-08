using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PesDetect : MonoBehaviour
{
    public GameObject puerta;
    public float velocidad = 2f;
    public float distanciaBajada = 5f;

    private Vector3 posicionInicialPuerta;
    private Vector3 posicionFinalPuerta;
    private bool objetoEncima = false;

    void Start()
    {
        if (puerta != null)
        {
            posicionInicialPuerta = puerta.transform.position;
            posicionFinalPuerta = posicionInicialPuerta - new Vector3(0, distanciaBajada, 0);
        }
    }

    void Update()
    {
        if (puerta != null)
        {
            Vector3 objetivo = objetoEncima ? posicionFinalPuerta : posicionInicialPuerta;
            puerta.transform.position = Vector3.MoveTowards(
                puerta.transform.position,
                objetivo,
                velocidad * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            objetoEncima = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Desactiva la apertura si el objeto válido sale del trigger
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            objetoEncima = false;
        }
    }
}