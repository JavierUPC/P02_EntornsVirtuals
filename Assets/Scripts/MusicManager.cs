using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip inversa, normal;
    private AudioSource musicPlayer;
    public GameObject player;

    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.clip = normal;
        musicPlayer.Play();
    }

    private void FixedUpdate()
    {
        if (player.GetComponent<DimensionChange>().Dystopian() && musicPlayer.clip == normal)
        {
            InverseMusic();
        }
        else if (!player.GetComponent<DimensionChange>().Dystopian() && musicPlayer.clip == inversa)
        {
            NormalMusic();
        }
    }

    public void NormalMusic()
    {
        musicPlayer.clip = normal;
        musicPlayer.Play();
    }

    public void InverseMusic()
    {
        musicPlayer.clip = inversa;
        musicPlayer.Play();
    }
}
