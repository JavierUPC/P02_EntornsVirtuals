using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortestPath : MonoBehaviour
{
   public Transform target;
    public GameObject thisHead;
    public float offset;
    public float collisionPushForce;
    public float sphereDiameter = 1f; // Diameter of the sphere used for detecting obstacles
    public float rotationSpeed = 5f;  // Speed of rotation when avoiding obstacles

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
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        RaycastHit hitObstacle;

        // Visualize the SphereCast direction (Green Line)
        Debug.DrawRay(transform.position, transform.forward * 3f, Color.green); // 3 units forward

        // Visualize the radius of the SphereCast (Yellow Lines)
        float sphereRadius = sphereDiameter / 2;
        Debug.DrawRay(transform.position, transform.right * sphereRadius, Color.yellow); // Right side of the sphere
        Debug.DrawRay(transform.position, -transform.right * sphereRadius, Color.yellow); // Left side of the sphere

        if (GetComponentInChildren<DetectVision>().InView())
        {
            // If no obstacle is detected, continue moving towards the target
            GivePath(target.position);
        }
        else if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hitObstacle, 3f, detectMasks) && GetComponentInChildren<DetectVision>().Detected())
        {
            //CODIGP
        }
        else if (!GetComponentInChildren<DetectVision>().Detected())
        {
            // If target is not detected, stop or continue searching
            GivePath(new Vector3(transform.position.x, transform.position.y, target.position.z+1));
        }
    }

    public void GivePath(Vector3 givenPath)
    {
        GetComponent<LookAt>().MoveCoords(givenPath);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
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
