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
    float durat;
    float TimeTaa;
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
            AudioMana.instance.playSFXRandomly(enemy.ShootSound1);
            for (var i = 1;i<= BulletTime; i++)
            {
                tmp.x = Random.Range(-1*Spread,Spread);
                tmp.y = Random.Range(-1*Spread,Spread);
                Pool_manager.Release(TrackBullet, Muzzle.position + tmp, Quaternion.identity);
            }
        }
        if (TimeTaa < durat) TimeTaa += Time.deltaTime;
        else stateMachine.ChangeState(enemy.States[0]);
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        Muzzle = enemy.GetMuzzle()[0];
        TrackBullet = enemy.GetBullet_()[1];
        MinDely = 0.1f;
        MaxDely = 0.3f;
        BulletTime = 7;
        Spread = 1f;
        TimeTag = Random.Range(MinDely, MaxDely);
        durat = 1f;
        TimeTaa = 0f;
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }
}
