using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del jugador
    public float rotationSpeed = 700f; // Velocidad de rotación (si deseas rotar al jugador)

    private Vector3 moveDirection;

    void Update()
    {
        // Capturar la entrada del teclado (WASD)
        float horizontal = Input.GetAxis("Horizontal"); // A y D
        float vertical = Input.GetAxis("Vertical");     // W y S

        // Calculamos la dirección de movimiento
        moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Mover al jugador
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Opcional: Rotar al jugador para que mire hacia donde se mueve
        if (moveDirection.magnitude > 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
