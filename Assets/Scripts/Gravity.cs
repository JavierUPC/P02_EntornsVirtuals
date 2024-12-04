using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody rb;
    private bool grounded;
    private float speed;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!grounded)
        {
            time += Time.unscaledDeltaTime;
            speed = 9.8f * time;
           
        }
        else
        {
            speed = 2;
        }

        rb.velocity = new Vector3(rb.velocity.x, -speed, rb.velocity.z);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("Terrain") || collision.collider.CompareTag("Ground"))
        {
            grounded = true;
            time = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Terrain") || collision.collider.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}
