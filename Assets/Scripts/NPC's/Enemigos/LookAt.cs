using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Vector3 lookAt;
    public Transform target;
    public Transform thisHead; 

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!thisHead.gameObject.GetComponent<DetectVision>().Detected() || !thisHead.gameObject.GetComponent<DetectAudio>().Detected())
            return;

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        if (thisHead != null)
        {
            Vector3 direccion = target.position - thisHead.position;

            Quaternion targetRotation = Quaternion.LookRotation(direccion, Vector3.up);

            if (targetRotation.x < -70f)
                targetRotation = Quaternion.Euler(-70f, targetRotation.y, targetRotation.z);
            else if (targetRotation.x > 15f)
                targetRotation = Quaternion.Euler(0f, targetRotation.y, targetRotation.z);


            thisHead.rotation = targetRotation;
        }
    }

    public void MoveCoords(Vector3 coords)
    {
        lookAt = coords;
    }
}
