using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPersonaje : MonoBehaviour
{
    public AudioClip step1, step2;
    public AudioSource audioSource1; // Cambié el nombre para ser consistente

    public void PlayPaso1() // Desde el animador
    {
        audioSource1.clip = step1; // Usamos audioSource1 correctamente
        audioSource1.Play();
        // Debug.Log("Played Step 1");
    }

    public void PlayPaso2() // Desde el animador
    {
        audioSource1.clip = step2; // Usamos audioSource1 correctamente
        audioSource1.Play();
        // Debug.Log("Played Step 2");
    }
}

