using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowards : MonoBehaviour
{
    private Vector3 lookAt;
    public Transform thisHead;
    public Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!thisHead.gameObject.GetComponent<DetectAudio>().Detected())
            return;

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    public void MoveCoords(Vector3 coords)
    {
        lookAt = coords;
    }
}
