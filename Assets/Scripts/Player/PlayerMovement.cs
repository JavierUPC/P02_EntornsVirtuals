using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float rotationSpeed = 720f; // Velocidad de rotación (en grados por segundo)

    private Rigidbody rb;

    void Start()
    {
        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Leer las entradas del teclado
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D o flechas izquierda/derecha
        float moveVertical = Input.GetAxis("Vertical"); // W/S o flechas arriba/abajo

        // Crear un vector de movimiento
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        if (movement.magnitude > 0.1f) // Si hay movimiento
        {
            // Normalizar el vector para una velocidad constante
            movement.Normalize();

            // Mover al Rigidbody
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

            // Rotar al objeto hacia la dirección del movimiento
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}


