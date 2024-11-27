using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SordoMovimiento : MonoBehaviour
{
    public float forwardMove;
    public int turnFreq;
    private bool mode = false;
    private float timer = 0;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponentInChildren<DetectVision>().Detected())
            Engaged();
        else
            NotEngaged();

        timer += Time.unscaledDeltaTime;
    }

    private void Engaged()
    {
        Vector3 localSpeed = new Vector3(0, rb.velocity.y, forwardMove);
        rb.velocity = transform.TransformDirection(localSpeed);
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

            Vector3 localSpeed = new Vector3(0, rb.velocity.y, forwardMove / 4);
            rb.velocity = transform.TransformDirection(localSpeed);

        }
        else
        {
            if (timer > turnFreq)
            {
                mode = false;
                timer = 0;
            }

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 1, transform.rotation.eulerAngles.z);
        }
    }
}
