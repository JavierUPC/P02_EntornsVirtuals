using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del jugador


    void Update()
    {
        Move();
    }


public void Move()
{
        // Obtener las entradas del teclado (WASD)
        float horizontal = Input.GetAxis("Horizontal"); // A/D o flechas izquierda/derecha
        float vertical = Input.GetAxis("Vertical"); // W/S o flechas arriba/abajo

        // Crear el movimiento
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;

        // Mover al jugador
        transform.Translate(movement);
    }
}

