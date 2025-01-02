using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : EnemyState
{
    public GameObject target;
    public override void Do()
    {
        Debug.Log(target.transform.position);
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }
}
