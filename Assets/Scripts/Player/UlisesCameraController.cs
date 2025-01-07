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

    private bool isChangingWorld = false;     // Bandera para evitar múltiples activaciones

    // Variables públicas para personalizar la voltereta
    public float rollRotationAmount = 720f;   // Cantidad de rotación durante la voltereta
    public float rollForwardDistance = 3f;    // Distancia hacia adelante durante la voltereta
    public float rollDuration = 1f;           // Duración de la voltereta

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
        if (!isChangingWorld) // Solo mover la cámara normalmente si no estamos cambiando de mundo
        {
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

        // Detectar cambio de mundo solo si el jugador está quieto
        if (Input.GetKeyDown(KeyCode.E) && !isChangingWorld && IsPlayerIdle())
        {
            StartCoroutine(FollowCharacterRoll());
        }
    }

    // Función que verifica si el jugador está quieto
    bool IsPlayerIdle()
    {
        // Si no hay entrada de movimiento (horizontal o vertical), el jugador está quieto
        return Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0;
    }

    // Corrutina para animar la cámara durante la voltereta
    System.Collections.IEnumerator FollowCharacterRoll()
    {
        isChangingWorld = true;

        // Configurar los valores iniciales y finales
        Quaternion initialRotation = transform.rotation; // Rotación inicial de la cámara
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, rollRotationAmount, 0f); // Voltereta completa en Y
        Vector3 initialPosition = transform.position; // Posición inicial
        Vector3 targetPosition = initialPosition + transform.forward * rollForwardDistance; // Desplazarse hacia adelante

        float elapsedTime = 0f;

        // Interpolar rotación y posición
        while (elapsedTime < rollDuration)
        {
            float t = elapsedTime / rollDuration;

            // Rotar la cámara
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

            // Mover la cámara hacia adelante
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar que termina exactamente en la posición/rotación final
        transform.rotation = targetRotation;
        transform.position = targetPosition;

        isChangingWorld = false;
    }
}
