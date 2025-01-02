using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionStatus : EnemyState
{
    public float silentTimeNeeded;
    private bool firstDetected;

    private void Start()
    {
        detected = false;
        firstDetected = false;
        startTime = Time.time;
    }

    public override void Do()
    {
        if(firstDetected && !detected)
        {
            Debug.Log("In");
            startTime = Time.time;
            detected = true;
        }
        if(time >= silentTimeNeeded && !firstDetected && detected)
        {
            detected = false;
        }
    }

    public void OnRayHit(bool value)
    {
        firstDetected = value;
        //Debug.Log(value);
    }
}
