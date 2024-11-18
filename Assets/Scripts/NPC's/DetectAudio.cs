using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAudio : MonoBehaviour
{
    public float silentTimeNeeded;
    private bool detected;
    private bool firstDetected;
    private float undetectedTime;

    private void Start()
    {
        detected = false;
        firstDetected = false;
        undetectedTime = 0;
    }
    private void FixedUpdate()
    {
        if (firstDetected)
        {
            detected = firstDetected;
        }
        else if (!firstDetected && detected)
        {
            undetectedTime += Time.unscaledDeltaTime;
        }

        if (undetectedTime >= silentTimeNeeded)
        {
            detected = false;
            undetectedTime = 0;
        }
    }
    public void OnRayHit(bool value)
    {
        firstDetected = value;
    }

    public bool Detected()
    {
        return detected;
    }

}