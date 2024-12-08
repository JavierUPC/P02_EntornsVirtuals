using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiegoBola : MonoBehaviour
{

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Kill>().Respawn();
        }
    }
}
