using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttack : IdleState
{
    Transform Muzzle;
    float FireDelyTime;
    float MaxDely;
    float MinDely;
    GameObject NormalBullet;

    public MoveAttack(BossCon enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        
    }
    public override void AnimationTriggeredEvent(BossCon.AnimationTriggerType triggerType)
    {
        base.AnimationTriggeredEvent(triggerType);
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        Muzzle = enemy.GetMuzzle()[1];
        NormalBullet = enemy.GetBullet_()[0];
        MaxDely = 0.5f;
        MinDely = 0.1f;
        FireDelyTime = Random.Range(MinDely, MaxDely);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(FireDelyTime > 0f)
            FireDelyTime -= Time.deltaTime;
        else
        {
            Pool_manager.Release(NormalBullet, Muzzle.position, Quaternion.identity);
            FireDelyTime = Random.Range(MinDely, MaxDely);
        }

        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnExitState()
    {
        base.OnExitState();
    }
}
