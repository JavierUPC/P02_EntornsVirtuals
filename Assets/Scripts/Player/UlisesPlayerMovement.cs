using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class UlisesPlayerMovement : MonoBehaviour
{
    public Animator animator;             // Referencia al Animator
    public Transform cameraRef;          // Referencia a la cámara
    public float speed = 5f;             // Velocidad al caminar
    public float runSpeed = 0, sensX = 0, sensY = 0;          // Velocidad al correr
    public float crouchSpeed = 2.5f;     // Velocidad al agacharse
    public float jumpForce = 5f;         // Fuerza del salto
    public Rigidbody rb;                 // Rigidbody del jugador
    public Transform groundCheck;        // Punto para verificar si el jugador está tocando el suelo
    public float groundDistance = 0.1f;  // Distancia para la verificación de suelo
    public LayerMask groundMask;         // Máscara de la capa del suelo

    private float currentSpeed;

    public InputActionReference move, jump, rotate;
    private Vector2 rotationDelta;
    private Vector3 moveDirection;
    private float vertRot, horiRot;

    private Vector3 movement;            // Dirección de movimiento
    private bool isGrounded;             // Si está en el suelo
    private float yRotation = 0f;        // Rotación de la cámara en el eje Y (para rotar el jugador)

    bool _wasGrounded;

    //ACTION EVENTLISTENERS
    private void OnEnable()
    {
        jump.action.started += Jump;
    }

    private void OnDisable()
    {
        jump.action.started -= Jump;
    }
    //------


    //JUMP
    private void Jump(InputAction.CallbackContext obj)
    {

        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
    }

    private void Move()
    {
        Vector2 worldMoveDirection = move.action.ReadValue<Vector2>();
        Vector3 worldDirection = new Vector3(worldMoveDirection.x, 0, worldMoveDirection.y);
        moveDirection = transform.TransformDirection(worldDirection);

        //Debug.Log("Move: " + moveDirection);
    }

    private void Rotate()
    {
        rotationDelta = rotate.action.ReadValue<Vector2>();

        vertRot = rotationDelta.y * sensY * Time.deltaTime;
        horiRot = rotationDelta.x * sensX * Time.deltaTime;
        transform.Rotate(Vector3.up, horiRot);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
    }

    void Update()
    {
        

        Move();

        Rotate();

        // Verificar si está tocando el suelo
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);



        // Detectar entrada del teclado
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcular dirección de movimiento
        Vector3 forward = cameraRef.forward;
        Vector3 right = cameraRef.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        movement = (forward * vertical + right * horizontal).normalized;

        // Ajustar velocidad base
        

        // Estados de animación
        bool isWalking = movement.magnitude > 0 && vertical >= 0;
        bool isWalkingBack = vertical < 0;
        bool IsCrouchingBack = vertical < 0 && isWalkingBack;
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;
        bool isCrouchWalking = isWalking && isCrouching;
        bool isDancing = Input.GetKey(KeyCode.B);


        

        // Ajustar velocidad según el estado
        if (isRunning)
        {
            currentSpeed = runSpeed;

        }
        else if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

        //Debug.Log(isRunning);
        // Aplicar movimiento al jugador
        //Vector3 velocity = movement * currentSpeed;
        //velocity.y = rb.velocity.y; // Mantener la velocidad vertical
        //rb.velocity = velocity;

        // Rotar al jugador hacia la cámara
        //float mouseX = Input.GetAxis("Mouse X");
        //yRotation += mouseX;
        //transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // Configurar animaciones
        animator.SetBool("IsWalking", isWalking && !isRunning && !isCrouching);
        animator.SetBool("IsWalkingBack", isWalkingBack);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsCrouching", isCrouching);
        animator.SetBool("IsCrouchWalking", isWalking && isCrouching);
        animator.SetBool("IsDancing", isDancing);
        animator.SetBool("IsCrouchingBack", IsCrouchingBack);
        animator.SetBool("IsLanding", true);



        // Verificar si está tocando el suelo
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);

        // SALTO
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //    Debug.Log("SALTANDO");  // Si esto no se muestra, es que el salto no se está ejecutando
        //    rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reiniciar la velocidad vertical
        //    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);      // Aplicar fuerza de salto
        //    animator.SetBool("IsJumping", true);                        // Activar animación de salto

        //}
        // DETECTAR ATERRIZAJE
        if (!_wasGrounded && isGrounded) // Si estaba en el aire y ahora está en el suelo
        {
            animator.SetBool("IsJumping", false); // Finalizar animación de salto
            animator.SetBool("IsFalling", false); // Finalizar animación de caída
            animator.SetBool("IsLanding", true);  // Activar animación de aterrizaje
            animator.SetTrigger("Landed");        // Activar trigger de aterrizaje
            Debug.Log("Aterrizado");
        }

        // Desactivar "IsLanding" después de un tiempo
        if (animator.GetBool("IsLanding") && !_wasGrounded) // Si el bool sigue activado pero ya pasó el aterrizaje
        {
            animator.SetBool("IsLanding", false);
        }


        // ACTIVAR CAÍDA
        if (!isGrounded && rb.velocity.y < 0) // Si no está en el suelo y está cayendo
        {
            animator.SetBool("IsFalling", true); // Activar la animación de caída
        }

        // DETECTAR ATERRIZAJE
        if (!_wasGrounded && isGrounded) // Si estaba en el aire y ahora está en el suelo
        {
            animator.SetBool("IsJumping", false); // Finalizar animación de salto
            animator.SetBool("IsFalling", false); // Finalizar animación de caída
            animator.SetTrigger("Landed");    // Activar animación de aterrizaje
            Debug.Log("Aterrizado");
        }

        // Actualizar estado previo del suelo
        _wasGrounded = isGrounded;



    }
}
