using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floored : MonoBehaviour
{
    private bool floored;
    private void Start()
    {
        floored = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Floor")
            floored = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Floor")
            floored = false;
    }

    public bool IsFloored()
    {
        return floored;
    }
}
