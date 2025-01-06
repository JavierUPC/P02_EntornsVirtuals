using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : EnemyState
{
    public float forwardMove;
    public int walkTime;

    public override void Do()
    {
        Vector3 localSpeed = new Vector3(0, 0, forwardMove);
        transform.Translate(localSpeed * Time.deltaTime);

        if (justEntered)
            Enter();

        if (time >= walkTime)
            Exit();
    }
}
