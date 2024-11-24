using UnityEngine;

public class UlisesCameraController : MonoBehaviour
{
    public Transform player;           // Referencia al jugador
    public Transform cameraPivot;      // Punto alrededor del cual gira la cámara
    public Transform mainCamera;       // Referencia a la cámara principal

    public float mouseSensitivity = 3f; // Sensibilidad del ratón
    public float rotationSmoothTime = 0.1f; // Tiempo de suavizado al rotar
    public float verticalClamp = 85f;  // Límite vertical para el movimiento de la cámara

    public float defaultCameraDistance = 5f;  // Distancia normal de la cámara
    public float runningCameraDistance = 7f;  // Distancia cuando el jugador corre
    public float crouchingCameraDistance = 3f; // Distancia cuando el jugador se agacha
    public float zoomSpeed = 5f;              // Velocidad para ajustar la distancia de la cámara

    private Vector2 currentRotation;          // Rotación actual de la cámara
    private Vector2 rotationSmoothVelocity;   // Velocidad de suavizado para la rotación
    private float currentCameraDistance;      // Distancia actual de la cámara

    void Start()
    {
        // Ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Configurar la distancia inicial de la cámara
        currentCameraDistance = defaultCameraDistance;
    }

    void Update()
    {
        // Entrada del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Ajustar la rotación en función de la entrada
        currentRotation.x += mouseX;
        currentRotation.y -= mouseY;

        // Limitar el movimiento vertical
        currentRotation.y = Mathf.Clamp(currentRotation.y, -verticalClamp, verticalClamp);

        // Suavizar la rotación
        Vector2 targetRotation = Vector2.SmoothDamp(transform.eulerAngles, currentRotation, ref rotationSmoothVelocity, rotationSmoothTime);

        // Aplicar la rotación al punto de pivote
        cameraPivot.localRotation = Quaternion.Euler(currentRotation.y, 0f, 0f); // Controla la rotación vertical
        transform.rotation = Quaternion.Euler(0f, currentRotation.x, 0f); // Controla la rotación horizontal

        // Determinar la distancia de la cámara según la acción
        if (Input.GetKey(KeyCode.LeftShift)) // Corriendo
        {
            currentCameraDistance = Mathf.Lerp(currentCameraDistance, runningCameraDistance, Time.deltaTime * zoomSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftControl)) // Agachado
        {
            currentCameraDistance = Mathf.Lerp(currentCameraDistance, crouchingCameraDistance, Time.deltaTime * zoomSpeed);
        }
        else // Estado normal
        {
            currentCameraDistance = Mathf.Lerp(currentCameraDistance, defaultCameraDistance, Time.deltaTime * zoomSpeed);
        }

        // Ajustar la posición de la cámara en función de la distancia
        mainCamera.localPosition = new Vector3(0f, 0f, -currentCameraDistance);

        // Alinear el jugador con la cámara cuando se mueve
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            Quaternion targetPlayerRotation = Quaternion.Euler(0f, currentRotation.x, 0f);
            player.rotation = Quaternion.Lerp(player.rotation, targetPlayerRotation, Time.deltaTime * 5f);
        }
    }
}
