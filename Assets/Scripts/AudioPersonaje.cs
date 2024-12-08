using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPersonaje : MonoBehaviour
{
    public AudioClip step1, step2, salto;
    public AudioSource audioSource1; // Cambié el nombre para ser consistente

    public void PlayPaso1() // Desde el animador
    {
        audioSource1.clip = step1;
        audioSource1.volume = 0.2f;
        audioSource1.Play();
        // Debug.Log("Played Step 1");
    }

    public void PlayPaso2() // Desde el animador
    {
        audioSource1.clip = step2;
        audioSource1.volume = 0.2f;
        audioSource1.Play();
        // Debug.Log("Played Step 2");
    }

    public void PlaySalto()
    {
        audioSource1.clip = salto;
        audioSource1.volume = 0.3f;
        audioSource1.Play();

    }
}

