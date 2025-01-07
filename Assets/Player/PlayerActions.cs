using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour

    public Animator animator;             // Referencia al Animator

    public float moveSpeed = 0, runSpeed = 0, jumpForce = 0, sensX = 0, sensY = 0;
    public InputActionReference move, jump, rotate;
    public InputActionAsset inputActions;

    private bool grounded;
    private Rigidbody rb;
    private Vector2 rotationDelta;
    private Vector3 moveDirection;
    private float speed;
    private float vertRot, horiRot;

private Vector3 movement;            // Dirección de movimiento
private bool isGrounded;             // Si está en el suelo
private float yRotation = 0f;

float horizontal = Input.GetAxis("Horizontal");
float vertical = Input.GetAxis("Vertical");


bool isWalking = movement.magnitude > 0 && vertical >= 0;
bool isWalkingBack = vertical < 0;
bool IsCrouchingBack = vertical < 0 && isWalkingBack;
bool isCrouching = Input.GetKey(KeyCode.LeftControl);
bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;
bool isCrouchWalking = isWalking && isCrouching;
bool isDancing = Input.GetKey(KeyCode.B);

// Configurar animaciones
        animator.SetBool("IsWalking", isWalking && !isRunning && !isCrouching);
        animator.SetBool("IsWalkingBack", isWalkingBack);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsCrouching", isCrouching);
        animator.SetBool("IsCrouchWalking", isWalking && isCrouching);
        animator.SetBool("IsDancing", isDancing);
        animator.SetBool("IsCrouchingBack", IsCrouchingBack);


        // Verificar si está tocando el suelo
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);

        // SALTO
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("SALTANDO");  // Si esto no se muestra, es que el salto no se está ejecutando
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reiniciar la velocidad vertical
    rb.AddForce(Vector3.up* jumpForce, ForceMode.Impulse);      // Aplicar fuerza de salto
            animator.SetBool("IsJumping", true);                        // Activar animación de salto
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

// Start is called before the first frame update
void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        speed = moveSpeed;
    }


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
        if (grounded)
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }
    //------


    //UPDATE
    void Update()
    {
        Move();

        Rotate();

        if (grounded)
            Run();
    }
    //------


    //FIXEDUPDATE
    public void FixedUpdate()
    { 
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);

        if(!grounded && )
    }
    //------


    //MOVE
    public void Move()
    {
        Vector2 worldMoveDirection = move.action.ReadValue<Vector2>();
        Vector3 worldDirection = new Vector3(worldMoveDirection.x, 0, worldMoveDirection.y);
        moveDirection = transform.TransformDirection(worldDirection);

        //Debug.Log("Move: " + moveDirection);
    }
    //------


    //ROTATE    
    public void Rotate()
    {
        rotationDelta = rotate.action.ReadValue<Vector2>();

        vertRot = rotationDelta.y * sensY * Time.deltaTime;
        horiRot = rotationDelta.x * sensX * Time.deltaTime;

        //Evitar que la cámara rote de más

        transform.Rotate(Vector3.up, horiRot);
    }
    //------

    //RUN
    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;
        else
            speed = moveSpeed;
    }
    //-----


    //GROUNDED
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Terrain"))
            grounded = true;
    }
    

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Terrain"))
            grounded = false;
    }
    //--------
}
