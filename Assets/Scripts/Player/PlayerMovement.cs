using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject FloorChecker;
    public Animator ArmsAnimaton; // Aseg�rate de que este es el nombre correcto


    private Rigidbody rb;
    private Collider coll;
    private bool floored;

    public float moveSpeed = 6f;
    public float jumpForce = 10f;

    public float mouseSens = 100f;
    private float rotationX;

    private float mouseX, moveX, moveZ;
    private float timer;
    public float runSpeed = 12f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        floored = false;
        Cursor.lockState = CursorLockMode.Locked;
        rotationX = 0f;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        rb.velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, move.z * moveSpeed);


        if (moveX != 0 || moveZ != 0) // Si el personaje se está moviendo
        {
            if (Input.GetKey(KeyCode.LeftShift) && floored && FloorChecker.GetComponent<Floored>().IsFloored()) // Si está corriendo
            {
                ArmsAnimaton.SetBool("isRunning", true); // Activar la animación de correr
                ArmsAnimaton.SetBool("isWalking", false); // Desactivar la animación de caminar
            }
            else // Si está caminando
            {
                ArmsAnimaton.SetBool("isWalking", true); // Activar la animación de caminar
                ArmsAnimaton.SetBool("isRunning", false); // Desactivar la animación de correr
            }
        }
        else // Si el personaje está quieto
        {
            ArmsAnimaton.SetBool("isWalking", false); // Activar la animación de idle
            ArmsAnimaton.SetBool("isRunning", false); // Desactivar la animación de correr
        }

        if (!floored && !FloorChecker.GetComponent<Floored>().IsFloored()) // Si el personaje está en el aire
        {
            ArmsAnimaton.SetBool("isJumping", true); // Activar la animación de saltar
        }
        else // Si el personaje está en el suelo
        {
            ArmsAnimaton.SetBool("isJumping", false); // Desactivar la animación de saltar
        }




        //Salto - el "airtime" depende del tiempo que se mantenga el bot�n presionado
        if (floored && Input.GetKeyDown(KeyCode.Space) && FloorChecker.GetComponent<Floored>().IsFloored())
        {
            timer += Time.deltaTime;
            Debug.Log("Jump1");
        }
        else if (Input.GetKey(KeyCode.Space) && timer > 0 && timer <= 0.5)
        {
            Debug.Log("Jump2");
            timer += Time.deltaTime;
            Jump();
        }
        else if (Input.GetKeyUp(KeyCode.Space) || timer > 0.5)
        {
            Debug.Log("Jump3");
            timer = 0;
        }

        //codigo correr
        if (Input.GetKey(KeyCode.LeftShift) && floored && FloorChecker.GetComponent<Floored>().IsFloored())
        {
            Debug.Log("Corre");
            Correr(move);
        }

        //Rotacion horizontal del personaje segun el movimiento del raton
        mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        rotationX += mouseX;
        transform.rotation = Quaternion.Euler(0f, rotationX, 0f);
    }

    private void Correr(Vector3 movimiento)
    {

        rb.velocity = new Vector3(movimiento.x * runSpeed, rb.velocity.y, movimiento.z * runSpeed);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce / (1 + Mathf.Pow(timer, 2)), rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            floored = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            floored = false;
    }

}
