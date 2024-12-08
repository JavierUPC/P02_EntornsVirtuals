using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiegoAtaque : MonoBehaviour
{
    public GameObject ataque;
    private float timer;
    public float attackInterval;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponentInChildren<DetectAudio>().Detected() && timer > attackInterval)
        {
            Instantiate(ataque, new Vector3(transform.position.x, transform.position.y, transform.position.z+5), Quaternion.identity);
            timer = 0;
        }

        timer += Time.unscaledDeltaTime;
    }
}
