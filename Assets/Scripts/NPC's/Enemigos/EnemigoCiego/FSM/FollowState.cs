using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : EnemyState
{
    public float followSpeed;
    public override void Do()
    {
        Vector3 localSpeed = new Vector3(0, 0, followSpeed);
        transform.Translate(localSpeed * Time.deltaTime);
    }
}
