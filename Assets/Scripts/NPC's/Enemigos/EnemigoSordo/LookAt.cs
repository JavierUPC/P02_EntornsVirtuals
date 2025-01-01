using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Vector3 lookAt;
    public Transform thisHead; 

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!thisHead.gameObject.GetComponent<DetectVision>().Detected() /*&& !thisHead.gameObject.GetComponent<DetectAudio>().Detected()*/)
            return;

        transform.LookAt(new Vector3(lookAt.x, transform.position.y, lookAt.z));

        //if (thisHead != null)
        //{
        //    Vector3 direccion = lookAt - thisHead.position;

        //    Quaternion targetRotation = Quaternion.LookRotation(direccion, Vector3.up);

        //    Vector3 eulerRotation = targetRotation.eulerAngles;

        //    if (eulerRotation.x > 180) 
        //        eulerRotation.x -= 360;

        //    eulerRotation.x = Mathf.Clamp(eulerRotation.x, -50f, 15f);
        //    targetRotation = Quaternion.Euler(eulerRotation);
        //    thisHead.rotation = targetRotation;
        //}
    }

    public void MoveCoords(Vector3 coords)
    {
        lookAt = coords;
    }
}
