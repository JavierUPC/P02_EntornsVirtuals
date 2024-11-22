using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortestPath : MonoBehaviour
{
    public Transform target;
    private Vector3 targetPath;

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
        Vector3 targetLine = target.position - transform.position;

        RaycastHit hitObsticle;
        if (Physics.Raycast(transform.position, targetLine, out hitObsticle, Vector3.Distance(target.position, transform.position), detectMasks))
        {
            Vector3 objectSize = hitObsticle.transform.lossyScale;
            Vector3 objectRotation = hitObsticle.transform.rotation.eulerAngles;
            Vector3 objectPosition = hitObsticle.transform.position;

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

            Debug.DrawLine(observerPosition, leftPoint, Color.red, 1f);
            Debug.DrawLine(observerPosition, rightPoint, Color.blue, 1f);

            if(Vector3.Distance(leftPoint,transform.position) > Vector3.Distance(rightPoint, transform.position))
            {
                SetPath(rightPoint);
            }
            else
            {
                SetPath(leftPoint);
            }
        }
        else
        {
            SetPath(target.position);
        }
    }

    private void SetPath(Vector3 targetPos)
    {
        GivePath(new Vector3(targetPos.x, transform.position.y, targetPos.x));
    }

    public void GivePath(Vector3 givenPath)
    {
        GetComponent<LookAt>().MoveCoords(givenPath);
    }
}
