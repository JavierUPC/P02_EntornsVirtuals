using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : EnemyState
{
    public int turnTime;
    public override void Do()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 0.2f, transform.rotation.eulerAngles.z);

        if (justEntered)
            Enter();

        if (time >= turnTime)
            Exit();
    }
}
