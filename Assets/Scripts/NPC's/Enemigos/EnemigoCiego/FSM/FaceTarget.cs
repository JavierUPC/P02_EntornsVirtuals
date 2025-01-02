using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : EnemyState
{
    public Transform target;
    public override void Do()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }
}
