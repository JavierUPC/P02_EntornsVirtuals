using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStarCreation : MonoBehaviour
{
    public StarPoolManager starPoolManager;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player")) // Aseg�rate de usar la tag "Player" para tu personaje
        {
            Debug.Log("Collision detected");
            starPoolManager.TriggerStars(transform.position);
        }
    }
}

