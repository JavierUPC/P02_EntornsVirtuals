using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStarCreation : MonoBehaviour
{
    public StarPoolManager starPoolManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de usar la tag "Player" para tu personaje
        {
            Debug.Log("Collision detected");
            starPoolManager.TriggerStars(transform.position);
        }
    }
}

