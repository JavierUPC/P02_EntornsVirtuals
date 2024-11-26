using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortestPath : MonoBehaviour
{
    public Transform target;
    public GameObject thisHead;
    public float offset;

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

    private void CheckObstacles ()
    {
        Vector3 targetLine1 = new Vector3(target.position.x, target.position.y, target.position.y) - transform.position;

        RaycastHit hitObsticle;

        if (Physics.Raycast(transform.position, targetLine1, out hitObsticle, Vector3.Distance(target.position, transform.position), detectMasks) && GetComponentInChildren<DetectVision>().Detected())
        {
            Bounds bounds = hitObsticle.collider.bounds;

            Vector3[] corners = new Vector3[8];
            corners[0] = bounds.min;
            corners[1] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
            corners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
            corners[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
            corners[4] = bounds.max;
            corners[5] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
            corners[6] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
            corners[7] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);

            Vector3 observerPosition = transform.position;
            float mostLeft = float.MaxValue;
            float mostRight = float.MinValue;
            Vector3 leftPoint = Vector3.zero;
            Vector3 rightPoint = Vector3.zero;

            foreach (Vector3 corner in corners)
            {
                Vector3 directionToCorner = corner - observerPosition;
                float angle = Vector3.SignedAngle(transform.forward, directionToCorner, Vector3.up);

                if (angle < mostLeft)
                {
                    mostLeft = angle;
                    leftPoint = corner;
                }

                if (angle > mostRight)
                {
                    mostRight = angle;
                    rightPoint = corner;
                }
            }

            Debug.DrawLine(observerPosition, leftPoint, Color.green, 1f);
            Debug.DrawLine(observerPosition, rightPoint, Color.blue, 1f);

            Vector3 obstacleCenter = hitObsticle.collider.bounds.center;

            Vector3 directionFromCenterToLeft = (leftPoint - obstacleCenter).normalized;
            Vector3 directionFromCenterToRight = (rightPoint - obstacleCenter).normalized;

            Vector3 leftOffset = leftPoint + directionFromCenterToLeft * offset;
            Vector3 rightOffset = rightPoint + directionFromCenterToRight * offset;


            Debug.DrawLine(observerPosition, leftOffset, Color.yellow, 1f);
            Debug.DrawLine(observerPosition, rightOffset, Color.cyan, 1f);

            if (Vector3.Distance(new Vector3(leftOffset.x, transform.position.y, leftOffset.z), transform.position) +
                Vector3.Distance(new Vector3(leftOffset.x, transform.position.y, leftOffset.z), target.position) >
                Vector3.Distance(new Vector3(rightOffset.x, transform.position.y, rightOffset.z), transform.position) +
                Vector3.Distance(new Vector3(rightOffset.x, transform.position.y, rightOffset.z), target.position))
            {
                GivePath(new Vector3(rightOffset.x, transform.position.y, rightOffset.z));
            }
            else
            {
                GivePath(new Vector3(leftOffset.x, transform.position.y, leftOffset.z));
            }
        }
        else if (thisHead.GetComponent<DetectVision>().Detected())
        {
            GivePath(target.position);
        }
    }

    public void GivePath(Vector3 givenPath)
    {
        GetComponent<LookAt>().MoveCoords(givenPath);
    }
}
