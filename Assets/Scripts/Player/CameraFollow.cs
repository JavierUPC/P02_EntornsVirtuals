using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // Referencia al jugador
    public Vector3 offset;        // Desplazamiento de la c�mara
    public float smoothSpeed = 0.125f;  // Velocidad de suavizado del movimiento de la c�mara

    void LateUpdate()
    {
        // Calcula la nueva posici�n deseada de la c�mara
        Vector3 desiredPosition = player.position + offset;

        // Suaviza el movimiento de la c�mara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualiza la posici�n de la c�mara
        transform.position = smoothedPosition;

        // La c�mara siempre mira al jugador
        transform.LookAt(player);
    }
}
