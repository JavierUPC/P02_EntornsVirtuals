using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{

    private void Start()
    {
        justEntered = true;
        isComplete = false;
    }

    public bool isComplete { get; set; }
    protected float startTime;
    public float time => Time.time - startTime;
    public bool detected;
    protected bool justEntered;

    public virtual void Enter() { startTime = Time.time; justEntered = false; }
    public virtual void Do() { }
    public virtual void Exit() { isComplete = true; justEntered = true; }
}
