using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAudio : MonoBehaviour
{
    private bool detected;
    private void FixedUpdate()
    {
        if (OnRayHit())
        {

        }
    }
    public void OnRayHit(bool value)
    {
        detected = value;
    }

    public bool Detected()
    {
        return detected;
    }

}