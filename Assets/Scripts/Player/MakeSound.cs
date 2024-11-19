using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSound : MonoBehaviour
{
    public float distanciaWalk, distanciaRun, distanciaCrouch;
    public LayerMask layerMask;
    public int amountCircles;
    public int raysPerCircle;

    private bool noContact;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Player", "Terrain", "Wall", "Floor", "Enemy");
        noContact = true;
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
            //if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distanciaRun, layerMask))
            //{
            //    if (hit.collider.tag != "Enemy")
            //    {
            //        noContact = true;
            //    }
            //    else if (hit.collider.tag == "Enemy" && GetComponent<PlayerMovement>().MovementType() == 1 && Vector3.Distance(hit.collider.transform.position, transform.position) < distanciaRun)
            //    {
            //        hit.collider.GetComponent<DetectAudio>().OnRayHit(true);
            //        noContact = false;
            //        return;
            //    }
            //    else if (hit.collider.tag == "Enemy" && GetComponent<PlayerMovement>().MovementType() == 0 && Vector3.Distance(hit.collider.transform.position, transform.position) < distanciaWalk)
            //    {
            //        hit.collider.GetComponent<DetectAudio>().OnRayHit(true);
            //        noContact = false;
            //        return;
            //    }
            //    else if (hit.collider.tag == "Enemy" && GetComponent<PlayerMovement>().MovementType() == 2 && Vector3.Distance(hit.collider.transform.position, transform.position) < distanciaCrouch)
            //    {
            //        hit.collider.GetComponent<DetectAudio>().OnRayHit(true);
            //        noContact = false;
            //        return;
            //    }
            //}
        }
    }
}
