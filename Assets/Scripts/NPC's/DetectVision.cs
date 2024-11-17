using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectVision : MonoBehaviour
{
    public Transform thisHead;
    public int amountCircels = 1;
    public int maxAngle;
    public int raysPerCircle = 1;
    public int distancia;
    public LayerMask layerMask;
    private Vector3 direccion;
    private bool detected;

    private void Start()
    {
        detected = false;

        if (maxAngle > 65)
            maxAngle = 65;
        else if (maxAngle <= 0)
            maxAngle = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LightCone();
    }

    public void LightCone()
    {
        for (int i = 0; i < amountCircels; i++)
        {
            float anguloPolar = (i + 1) * maxAngle / amountCircels;

            for(int j = 0; j < raysPerCircle; j++){

                float anguloCircular = j * 360f / raysPerCircle;

                direccion = new Vector3(
                Mathf.Sin(anguloPolar * Mathf.Deg2Rad) * Mathf.Cos(anguloCircular * Mathf.Deg2Rad),
                Mathf.Sin(anguloPolar * Mathf.Deg2Rad) * Mathf.Sin(anguloCircular * Mathf.Deg2Rad),
                Mathf.Cos(anguloPolar * Mathf.Deg2Rad)
                );
                direccion = thisHead.TransformDirection(direccion);

                Debug.DrawRay(transform.position, direccion * distancia, Color.red);
                if (Physics.Raycast(transform.position, direccion, out RaycastHit hit, distancia, layerMask))
                {
                    detected = true;
                    Debug.Log(detected);
                }

            }
        }
    }

    public bool Detected()
    {
        return detected;
    }
}
