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

    private bool isChangingWorld = false;     // Bandera para evitar m�ltiples activaciones

    // Variables p�blicas para personalizar la voltereta
    public float rollRotationAmount = 720f;   // Cantidad de rotaci�n durante la voltereta
    public float rollForwardDistance = 3f;    // Distancia hacia adelante durante la voltereta
    public float rollDuration = 1f;           // Duraci�n de la voltereta

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
        if (!isChangingWorld) // Solo mover la c�mara normalmente si no estamos cambiando de mundo
        {
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

        // Detectar cambio de mundo solo si el jugador est� quieto
        if (Input.GetKeyDown(KeyCode.E) && !isChangingWorld && IsPlayerIdle())
        {
            StartCoroutine(FollowCharacterRoll());
        }
    }

    // Funci�n que verifica si el jugador est� quieto
    bool IsPlayerIdle()
    {
        // Si no hay entrada de movimiento (horizontal o vertical), el jugador est� quieto
        return Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0;
    }

    // Corrutina para animar la c�mara durante la voltereta
    System.Collections.IEnumerator FollowCharacterRoll()
    {
        isChangingWorld = true;

        // Configurar los valores iniciales y finales
        Quaternion initialRotation = transform.rotation; // Rotaci�n inicial de la c�mara
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, rollRotationAmount, 0f); // Voltereta completa en Y
        Vector3 initialPosition = transform.position; // Posici�n inicial
        Vector3 targetPosition = initialPosition + transform.forward * rollForwardDistance; // Desplazarse hacia adelante

        float elapsedTime = 0f;

        // Interpolar rotaci�n y posici�n
        while (elapsedTime < rollDuration)
        {
            float t = elapsedTime / rollDuration;

            // Rotar la c�mara
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

            // Mover la c�mara hacia adelante
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar que termina exactamente en la posici�n/rotaci�n final
        transform.rotation = targetRotation;
        transform.position = targetPosition;

        isChangingWorld = false;
    }
}
