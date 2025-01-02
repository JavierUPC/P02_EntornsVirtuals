using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    public GameObject ataque;
    public float attackInterval;
    public override void Do()
    {
        if (time >= attackInterval || justEntered)
        {
            Instantiate(ataque, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.identity);
            startTime = Time.time;
        }

        if (justEntered)
            Enter();
    }
}
