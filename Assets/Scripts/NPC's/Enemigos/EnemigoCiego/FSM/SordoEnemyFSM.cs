using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SordoEnemyFSM : MonoBehaviour
{
    EnemyState state;

    public EnemyState followState;
    public EnemyState walkState;
    public EnemyState turnState;

    public DetectionStatus detectionStatus;
    public AttackState attack;
    public FaceTarget faceTarget;

    private void Start()
    {
        state = walkState;
    }
    private void FixedUpdate()
    {
        SelectState();
        state.Do();
        detectionStatus.Do();
    }

    private void SelectState()
    {
        if (detectionStatus.detected)
        {
            state = followState;
            UpdateAttack();
        }
        else
        {
            UpdatePatrol();
        }
    }

    private void UpdatePatrol()
    {
        //Debug.Log(state.isComplete);
        if (state.isComplete)
        {
            if (state == walkState)
                state = turnState;
            else if (state == turnState)
                state = walkState;

            state.isComplete = false;
        }
    }
    private void UpdateAttack()
    {
        attack.Do();
        faceTarget.Do();
    }

}
