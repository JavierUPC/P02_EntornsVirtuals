using UnityEngine;

public class UlisesCameraController : MonoBehaviour
{
    public Transform player;           // Referencia al jugador
    public Transform cameraPivot;      // Punto alrededor del cual gira la c�mara
    public Transform mainCamera;       // Referencia a la c�mara principal

    public float mouseSensitivity = 3f; // Sensibilidad del rat�n
    public float rotationSmoothTime = 0.1f; // Tiempo de suavizado al rotar
    public float verticalClamp = 85f;  // L�mite vertical para el movimiento de la c�mara

    public float defaultCameraDistance = 5f;  // Distancia normal de la c�mara
    public float runningCameraDistance = 7f;  // Distancia cuando el jugador corre
    public float crouchingCameraDistance = 3f; // Distancia cuando el jugador se agacha
    public float zoomSpeed = 5f;              // Velocidad para ajustar la distancia de la c�mara

    private Vector2 currentRotation;          // Rotaci�n actual de la c�mara
    private Vector2 rotationSmoothVelocity;   // Velocidad de suavizado para la rotaci�n
    private float currentCameraDistance;      // Distancia actual de la c�mara

    void Start()
    {
        // Ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Configurar la distancia inicial de la c�mara
        currentCameraDistance = defaultCameraDistance;
    }

    void Update()
    {
        // Entrada del rat�n
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Ajustar la rotaci�n en funci�n de la entrada
        currentRotation.x += mouseX;
        currentRotation.y -= mouseY;

        // Limitar el movimiento vertical
        currentRotation.y = Mathf.Clamp(currentRotation.y, -verticalClamp, verticalClamp);

        // Suavizar la rotaci�n
        Vector2 targetRotation = Vector2.SmoothDamp(transform.eulerAngles, currentRotation, ref rotationSmoothVelocity, rotationSmoothTime);

        // Aplicar la rotaci�n al punto de pivote
        cameraPivot.localRotation = Quaternion.Euler(currentRotation.y, 0f, 0f); // Controla la rotaci�n vertical
        transform.rotation = Quaternion.Euler(0f, currentRotation.x, 0f); // Controla la rotaci�n horizontal

        // Determinar la distancia de la c�mara seg�n la acci�n
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

        // Ajustar la posici�n de la c�mara en funci�n de la distancia
        mainCamera.localPosition = new Vector3(0f, 0f, -currentCameraDistance);

        // Alinear el jugador con la c�mara cuando se mueve
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            Quaternion targetPlayerRotation = Quaternion.Euler(0f, currentRotation.x, 0f);
            player.rotation = Quaternion.Lerp(player.rotation, targetPlayerRotation, Time.deltaTime * 5f);
        }
    }
}
