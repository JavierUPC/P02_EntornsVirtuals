using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCam : MonoBehaviour
{
    public Camera playerCamera; 
    public float zoomedFOV = 30f; //fov del zoom
    public float normalFOV = 60f; //fov normal
    public float zoomSpeed = 2f; //velocidad zoom

    private bool isZoomed = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse2)) 
        {
            isZoomed = !isZoomed; //activar i desactivar
        }

        //Fov camara
        if (isZoomed)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomedFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, normalFOV, Time.deltaTime * zoomSpeed);
        }
    }
}
