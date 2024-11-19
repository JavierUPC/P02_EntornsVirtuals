using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // Referencia al jugador
    public Vector3 offset;        // Desplazamiento de la cámara
    public float smoothSpeed = 0.125f;  // Velocidad de suavizado del movimiento de la cámara

    void LateUpdate()
    {
        // Calcula la nueva posición deseada de la cámara
        Vector3 desiredPosition = player.position + offset;

        // Suaviza el movimiento de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualiza la posición de la cámara
        transform.position = smoothedPosition;

        // La cámara siempre mira al jugador
        transform.LookAt(player);
    }
}
