using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortestPath : MonoBehaviour
{
   public Transform target;
    public GameObject thisHead;
    public float offset;
    public float collisionPushForce;

    [SerializeField]
    public LayerMask detectMasks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckObstacles();
    }

    private void CheckObstacles()
    {
        if (GetComponentInChildren<DetectVision>().InView())
        {
            // If the target is in view, move directly towards the target
            GivePath(target.position);
        }
        else if (GetComponentInChildren<DetectVision>().Detected() &&
                 Physics.Raycast(transform.position, (target.position - transform.position).normalized, out RaycastHit hitObstacle, Vector3.Distance(transform.position, target.position), detectMasks))
        {
            // 360 Rays to detect the clearest path
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Vector3 bestDirection = Vector3.zero;
            float smallestAngle = float.MaxValue;

            int rayCount = 360; // Number of rays to cast (1 ray per degree)
            float rayDistance = 10f; // Distance of each ray
            float rayHeight = transform.position.y; // Fixed height

            for (int i = 0; i < rayCount; i++)
            {
                // Calculate the direction of the ray in world space
                float angle = i * Mathf.Deg2Rad; // Convert degree to radians
                Vector3 rayDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)); // Direction of the ray
                Vector3 rayOrigin = new Vector3(transform.position.x, rayHeight, transform.position.z); // Start at current height

                // Perform the raycast
                if (!Physics.Raycast(rayOrigin, rayDirection, rayDistance, detectMasks))
                {
                    // If the ray does not hit an obstacle, calculate the angle to the target
                    float angleToTarget = Vector3.Angle(directionToTarget, rayDirection);

                    // Check if this angle is the smallest so far
                    if (angleToTarget < smallestAngle)
                    {
                        smallestAngle = angleToTarget;
                        bestDirection = rayDirection;
                    }
                }

                // Visualize the rays for debugging
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
            }

            if (bestDirection != Vector3.zero)
            {
                // If a valid direction is found, move toward it
                Vector3 newTargetPosition = transform.position + bestDirection * rayDistance;
                GivePath(newTargetPosition);
            }
        }
        else if (!GetComponentInChildren<DetectVision>().Detected())
        {
            // If the target is not detected, stop or continue searching
            GivePath(new Vector3(transform.position.x, transform.position.y, target.position.z + 1));
        }
    }

    public void GivePath(Vector3 givenPath)
    {
        GetComponent<LookAt>().MoveCoords(givenPath);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") && GetComponentInChildren<DetectVision>().Detected())
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 collisionPoint = contact.point;

            Vector3 localCollisionPoint = transform.InverseTransformPoint(collisionPoint);

            Vector3 push = Vector3.zero;

            if (localCollisionPoint.x < 0)
            {
                push = transform.right;
            }
            else
            {
                push = -transform.right;
            }

            GetComponent<Rigidbody>().AddForce(push * collisionPushForce, ForceMode.Impulse);
            Debug.Log("Pushed");
        }
    }

    public Transform GiveTarget()
    {
        return target;
    }
}
