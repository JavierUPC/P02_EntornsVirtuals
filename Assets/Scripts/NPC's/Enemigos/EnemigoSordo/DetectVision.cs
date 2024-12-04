using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectVision : MonoBehaviour
{
    private Transform thisHead;
    public int amountCircels = 1;
    public int maxAngle;
    public int raysPerCircle = 1;
    public int distancia;
    public float necessaryHideTime;
    private LayerMask layerMask;
    private Vector3 direccion;
    private bool detected = false;
    private bool inView = false;
    private float unseenTime;

    private void Start()
    {
        thisHead = GetComponent<Transform>();
        detected = false;

        if (maxAngle > 65)
            maxAngle = 65;
        else if (maxAngle <= 0)
            maxAngle = 1;

        unseenTime = 0;

        layerMask = LayerMask.GetMask("Player", "Terrain", "Wall", "Floor"); //AÑADIR MAS LAYERS QUE OBSTACULIZAN
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log($"Detected: {detected}, InView: {inView}, UnseenTime: {unseenTime}");
        LightCone();
        if(!inView)
        {
            unseenTime += Time.unscaledDeltaTime;
        }
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

                if(Physics.Raycast(transform.position, direccion, out RaycastHit hit, distancia, layerMask))
                {
                    if (!hit.collider.CompareTag("Player"))
                    {
                        inView = false;
                    }
                    else 
                    {
                        unseenTime = 0;
                        detected = true;
                        inView = true;
                        return;
                    }
                }
                else
                {
                    inView = false;
                }

                if (unseenTime >= necessaryHideTime)
                {
                    detected = false;
                }
            }
        }
    }

    public bool Detected()
    {
        return detected;
    }

    public bool InView()
    {
        return inView;
    }
}
