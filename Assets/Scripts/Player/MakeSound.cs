using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSound : MonoBehaviour
{
    public float distanciaWalk, distanciaRun, distanciaCrouch;
    private LayerMask layerMask;
    public int amountCircles;
    public int raysPerCircle;
    private Animator moveState;

    private bool noContact;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Player", "Terrain", "Wall", "Floor", "Enemy");
        noContact = true;
        moveState = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AudioSphere();
    }

    public void AudioSphere()
    {
        int totalRays = amountCircles * raysPerCircle; // Total number of rays to cast

        for (int i = 0; i < totalRays; i++)
        {
            float theta = Mathf.PI * (1 + Mathf.Sqrt(5)) * i;
            float z = 1 - 2 * (i / (float)totalRays);
            float radius = Mathf.Sqrt(1 - z * z);
            Vector3 direction = new Vector3(
                radius * Mathf.Cos(theta),
                radius * Mathf.Sin(theta),
                z
            );

            Debug.DrawRay(transform.position, direction * distanciaRun, Color.red);
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distanciaRun, layerMask))
            {
              
                if (hit.collider.tag == "Ciego" && (moveState.GetBool("IsJumping") || moveState.GetBool("IsRunning")) && Vector3.Distance(hit.collider.transform.position, transform.position) <= distanciaRun)
                {
                    hit.collider.GetComponent<DetectionStatus>().OnRayHit(true);
                    noContact = false;
                }
                else if (hit.collider.tag == "Ciego" && moveState.GetBool("IsWalking") && Vector3.Distance(hit.collider.transform.position, transform.position) <= distanciaWalk)
                {
                    hit.collider.GetComponent<DetectionStatus>().OnRayHit(true);
                    noContact = false;
                }
                else if (hit.collider.tag == "Ciego" && moveState.GetBool("IsCrouchWalking") && Vector3.Distance(hit.collider.transform.position, transform.position) <= distanciaCrouch)
                {
                    hit.collider.GetComponent<DetectionStatus>().OnRayHit(true);
                    noContact = false;
                }
                else if (hit.collider.tag != "Ciego")
                {
                    
                }
                else
                {
                    hit.collider.GetComponent<DetectionStatus>().OnRayHit(false);
                    noContact = true;
                }
            }
        }
    }
}
