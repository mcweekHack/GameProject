using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    Vector3 bias;
    Vector3 NextPos;
    Vector3 NowPos;
    float MoveDur;
    float MoveBias;
    float TimeTag;
    public IdleState(BossCon enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        bias = new Vector3(3.07f, 0.8f);
    }
    public override void AnimationTriggeredEvent(BossCon.AnimationTriggerType triggerType)
    {
        base.AnimationTriggeredEvent(triggerType);
    }
    public override void OnEnterState()
    {
        NextPos = viewport.instance.BackEnemyTarget(bias);
        NowPos = enemy.transform.position;
        TimeTag = 0;
        MoveBias = 1f;
        MoveDur = Random.Range(enemy.MoveDur - MoveBias, enemy.MoveDur + MoveBias);
    }
    public override void FrameUpdate()
    {
        if (TimeTag <= MoveDur)
        {
            TimeTag += Time.deltaTime;
            enemy.transform.position = Vector3.Lerp(NowPos, NextPos, TimeTag / MoveDur);
        }
        else
        {
            NextPos = viewport.instance.BackEnemyTarget(bias);
            NowPos = enemy.transform.position;
            TimeTag = 0;
        }
        
    }
    public override void FixedUpdate()
    {

    }
    public override void OnExitState()
    {

    }
    
}
