using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiegoMovimiento : MonoBehaviour
{
    public float forwardMove;
    public int turnFreq;
    private bool mode = false;
    private float timer = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponentInChildren<DetectAudio>().Detected())
            Engaged();
        else
            NotEngaged();

        timer += Time.unscaledDeltaTime;
    }

    private void Engaged()
    {
        Vector3 localSpeed = new Vector3(0, 0, forwardMove); 
        transform.Translate(localSpeed * Time.deltaTime);
        //Debug.Log(rb.velocity.y);
    }

    private void NotEngaged()
    {
        if (!mode)
        {
            if (timer > turnFreq)
            {
                mode = true;
                timer = 0;
            }

            Vector3 localSpeed = new Vector3(0, 0, forwardMove/2); 
            transform.Translate(localSpeed * Time.deltaTime);

        }
        else
        {
            if (timer > turnFreq)
            {
                mode = false;
                timer = 0;
            }

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 0.2f, transform.rotation.eulerAngles.z);
        }
        //Debug.Log(rb.velocity.y);
    }
}
