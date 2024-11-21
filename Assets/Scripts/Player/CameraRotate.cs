using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Jugador
    public Vector3 offset;   // Distancia entre cámara y jugador
    public float rotationSpeed = 5f; // Velocidad de rotación

    void LateUpdate()
    {
        // Rotar cámara alrededor del jugador
        float horizontalInput = Input.GetAxis("Mouse X");
        Quaternion rotation = Quaternion.Euler(0, horizontalInput * rotationSpeed, 0);

        offset = rotation * offset;

        transform.position = player.position + offset;
        transform.LookAt(player.position);

        // Rotar al jugador hacia donde mira la cámara
        player.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }
}
