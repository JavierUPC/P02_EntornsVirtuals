using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public Transform thisHead; 

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (!target.GetComponent<ScriptName>().MethodDetectado())
        //    return;

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        if (thisHead != null)
        {
            Vector3 direccion = target.position - thisHead.position;

            Quaternion targetRotation = Quaternion.LookRotation(direccion, Vector3.up);

            if (targetRotation.x < -70f)
                targetRotation = Quaternion.Euler(-70f, targetRotation.y, targetRotation.z);
            else if (targetRotation.x > 0f)
                targetRotation = Quaternion.Euler(0f, targetRotation.y, targetRotation.z);


            thisHead.rotation = targetRotation;
        }
    }
}