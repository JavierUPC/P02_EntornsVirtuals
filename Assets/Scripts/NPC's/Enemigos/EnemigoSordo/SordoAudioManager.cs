using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SordoAudioManager : MonoBehaviour
{
    public AudioClip step1, step2, idle, roar;
    public AudioSource audioSource1, audioSource2;

    private void FixedUpdate()
    {
        PlayIdle();
    }

    public void PlayStep1() //Desde el animador
    {
        audioSource2.clip = step1;
        audioSource2.Play();
        //Debug.Log("Played Step 1");
    }

    public void PlayStep2() //Desde el animador
    {
        audioSource2.clip = step2;
        audioSource2.Play();
        //Debug.Log("Played Step 2");
    }

    public void PlayRoar() 
    {
        audioSource1.clip = roar;
        audioSource1.Play();
        //Debug.Log("Played Roar");
    }

    private void PlayIdle()
    {
        if (!audioSource1.isPlaying)
        {
            audioSource1.clip = idle;
            audioSource1.Play();
            //Debug.Log("Played Idle");
        }
    }
}
