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
            GivePath(target.position);
        }
        else if (GetComponentInChildren<DetectVision>().Detected() &&
                 Physics.Raycast(transform.position, (target.position - transform.position).normalized, out RaycastHit hitObstacle, Vector3.Distance(transform.position, target.position), detectMasks))
        {
            //Debug.Log(target.position);
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Vector3 bestDirection = Vector3.zero;
            float smallestAngle = float.MaxValue;

            int rayCount = 360; 
            float rayDistance = 50f; 
            float rayHeight = transform.position.y+1; 

            for (int i = 0; i < rayCount; i++)
            {
                
                float angle = i * Mathf.Deg2Rad; 
                Vector3 rayDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)); 
                Vector3 rayOrigin = new Vector3(transform.position.x, rayHeight, transform.position.z); 

                if (!Physics.Raycast(rayOrigin, rayDirection, rayDistance, detectMasks))
                {
                    float angleToTarget = Vector3.Angle(directionToTarget, rayDirection);

                    if (angleToTarget < smallestAngle)
                    {
                        smallestAngle = angleToTarget;
                        bestDirection = rayDirection;
                    }
                }
                //Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
            }

            if (bestDirection != Vector3.zero)
            {
                Vector3 newTargetPosition = transform.position + bestDirection;
                GivePath(newTargetPosition);
            }
        }
        else if (GetComponentInChildren<DetectVision>().Detected() &&
            !Physics.Raycast(transform.position, (target.position - transform.position).normalized, out RaycastHit nohitObstacle, Vector3.Distance(transform.position, target.position), detectMasks))
        {
            //Debug.Log("No tiene obstaculo");
            //Debug.Log(GetComponentInChildren<DetectVision>().Detected());
            GivePath(target.position);
        }
        else if (!GetComponentInChildren<DetectVision>().Detected())
            GivePath(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1));
    }

    public void GivePath(Vector3 givenPath)
    {
        //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), givenPath, Color.green);
        GetComponent<LookAt>().MoveCoords(givenPath);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") && GetComponentInChildren<DetectVision>().Detected())
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 collisionPoint = contact.point;

            Vector3 enemyCollisionPoint = transform.InverseTransformPoint(collisionPoint);

            Vector3 push = Vector3.zero;

            if (enemyCollisionPoint.x < 0)
            {
                push = transform.right;
            }
            else
            {
                push = -transform.right;
            }

            GetComponent<Rigidbody>().AddForce(push * collisionPushForce, ForceMode.Impulse);
            //Debug.Log("Pushed");
        }
    }

    public Transform GiveTarget()
    {
        return target;
    }
}
