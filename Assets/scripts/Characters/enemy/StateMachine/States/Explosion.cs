using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Explosion : IdleState
{
    Transform Muzzle;
    float FireDelyTime;
    float MaxDely;
    float MinDely;
    float Angle;
    float AngleGap;
    GameObject ExpBullet;
    Vector3 tmp;
    GameObject check;
    public Explosion(BossCon enemy, StateMachine stateMachine) : base(enemy, stateMachine){ }

    public override void AnimationTriggeredEvent(BossCon.AnimationTriggerType triggerType)
    {
        base.AnimationTriggeredEvent(triggerType);
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        Muzzle = enemy.GetMuzzle()[2];
        ExpBullet = enemy.GetBullet_()[2];
        Angle = 0f;
        AngleGap = 30f;
        MinDely = 0.5f;
        MaxDely = 1f;
        tmp = Vector3.left;
        FireDelyTime = 0f;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (FireDelyTime > 0f)
            FireDelyTime -= Time.deltaTime;
        else 
        {
            for(Angle = 0f; Angle < 360f;Angle += AngleGap)
            {
                tmp = Quaternion.AngleAxis(AngleGap, Vector3.forward)*tmp;
                check = Pool_manager.Release(ExpBullet, Muzzle.position, Quaternion.identity);
                if (check.TryGetComponent(out EnemyExpBullet res))
                    res.ChangeDirection(tmp);
            }
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
