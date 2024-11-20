using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform player;             // Referencia al jugador
    public float mouseSens = 100f;       // Sensibilidad del mouse
    public bool isFirstPerson;           // Determina si la cámara es en primera persona o en tercera

    private float xRotation = 0f;        // Rotación en el eje X (vertical)
    private float yRotation = 0f;        // Rotación en el eje Y (horizontal)
    private Vector3 initialPos;          // Posición inicial de la cámara en tercera persona

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro de la pantalla

        if (!isFirstPerson)
        {
            initialPos = transform.position - player.position; // Calcular la posición inicial de la cámara
        }
    }

    void Update()
    {
        // Capturar movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime; // Movimiento horizontal
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime; // Movimiento vertical

        // Calcular rotaciones
        xRotation -= mouseY; // Reducir la rotación vertical para invertir el eje
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotación vertical entre -90° y 90°
        yRotation += mouseX; // Incrementar la rotación horizontal

        if (isFirstPerson)
        {
            // Rotación de la cámara en primera persona
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotación vertical de la cámara
            player.Rotate(Vector3.up * mouseX); // Rotación horizontal del jugador
        }
        else
        {
            // Rotación de la cámara en tercera persona
            Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f); // Combinar ambas rotaciones
            transform.position = player.position + rotation * initialPos; // Calcular nueva posición de la cámara
            transform.LookAt(player.position + Vector3.up * 1.5f); // Mirar ligeramente por encima del jugador
        }
    }
}

