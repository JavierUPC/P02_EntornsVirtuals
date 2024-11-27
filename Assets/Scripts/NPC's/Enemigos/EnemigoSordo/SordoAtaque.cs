using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SordoAtaque : MonoBehaviour
{
    public float attackDelay = 0;
    public float attackDistance = 0;
    public float dashForce = 0;
    private float timer = 0;
    private bool animationState = false;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        timer += Time.unscaledDeltaTime;

        if (timer > attackDelay && GetComponentInChildren<DetectVision>().InView() && Vector3.Distance(transform.position, GetComponent<ShortestPath>().GiveTarget().position) < attackDistance)
        {
            timer = 0;
            //animación de ataque
            GetComponent<Rigidbody>().AddForce(dashForce * transform.forward, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && animationState)
        {
            Damage();
        }
    }

    public void Damage()
    {
        //Hacer daño
    }

    public void Animation() //Llamar este método en primer y último frame de la animación
    {
        if (animationState)
            animationState = false;
        else
            animationState = true;
    }
}
