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
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        timer += Time.unscaledDeltaTime;

        if (timer > attackDelay && GetComponentInChildren<DetectVision>().InView() && Vector3.Distance(transform.position, GetComponent<ShortestPath>().GiveTarget().position) < attackDistance)
        {
            timer = 0;
            animator.SetTrigger("Attack");
            animationState = true;
            GetComponent<Rigidbody>().AddForce(dashForce * transform.forward, ForceMode.Impulse); //Dash hacia delante para atacar
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && animationState)
        {
            Damage(collision.gameObject);
        }
    }

    public void Damage(GameObject Player)
    {
        Player.GetComponent<Kill>().Respawn();
    }

    public void Animation() //Llamar este m�todo en primer y �ltimo frame de la animaci�n
    {
        animationState = false;
    }
}
