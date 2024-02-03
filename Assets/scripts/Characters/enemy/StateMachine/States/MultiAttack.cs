using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttack : IdleState
{
    Transform Muzzle;
    float FireDelyTime;
    float MaxDely;
    float MinDely;
    float TimeTag;
    float Spread;
    Vector3 tmp;
    GameObject TrackBullet;
    int BulletTime;
    public MultiAttack(BossCon enemy, StateMachine stateMachine) : base(enemy, stateMachine){ }

    public override void AnimationTriggeredEvent(BossCon.AnimationTriggerType triggerType)
    {
        base.AnimationTriggeredEvent(triggerType);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(TimeTag > 0f)
            TimeTag -= Time.deltaTime;
        else
        {
            tmp = Vector3.zero;
            TimeTag = Random.Range(MinDely, MaxDely);
            for(var i = 1;i<= BulletTime; i++)
            {
                tmp.x = Random.Range(-1*Spread,Spread);
                tmp.y = Random.Range(-1*Spread,Spread);
                Pool_manager.Release(TrackBullet, Muzzle.position + tmp, Quaternion.identity);
            }
        }
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        Muzzle = enemy.GetMuzzle()[0];
        TrackBullet = enemy.GetBullet_()[1];
        MinDely = 1f;
        MaxDely = 3f;
        BulletTime = 7;
        Spread = 1f;
        TimeTag = Random.Range(MinDely, MaxDely);
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }
}
