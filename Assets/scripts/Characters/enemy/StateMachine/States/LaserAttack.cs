using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : EnemyState
{

    Transform Muzzle;
    GameObject lazerBullet;
    GameObject lazerInstance;
    Vector3 bias;
    Vector3 Pos;
    Vector3[] WayPoint;
    float[] DelayTime;
    float tmp, tmp2,tmp3;

    public LaserAttack(BossCon enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        bias = Vector3.zero;
        bias.x = 49.5f;
        bias.y = 0.03f;
        bias.z = 0f;
        WayPoint = new Vector3[2];
        DelayTime = new float[2];
    }

    public override void AnimationTriggeredEvent(BossCon.AnimationTriggerType triggerType)
    {
        base.AnimationTriggeredEvent(triggerType);
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        Muzzle = enemy.GetMuzzle()[1];
        lazerBullet = enemy.GetBullet_()[3];
        lazerInstance = null;
        DelayTime[0] = 1f;
        DelayTime[1] = 2f;
        tmp = 0f;
        tmp2 = 0f;
        tmp3 = 0f;
        WayPoint[0] = enemy.transform.position;
        WayPoint[1] = WayPoint[0];
        Pos = WayPoint[0];
        WayPoint[0].y = 3.3f;
        WayPoint[1].y = -4f;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (tmp < DelayTime[0])
        {
            tmp += Time.deltaTime;
            enemy.transform.position = Vector3.Lerp(Pos, WayPoint[0], tmp / DelayTime[0]);
        }
        else if (tmp2 < DelayTime[1])
        {
            if (lazerInstance == null)
            {
                lazerInstance = Pool_manager.Release(lazerBullet, Muzzle.position + bias);
                AudioMana.instance.playSFXRandomly(enemy.LazerSound);
            }
            tmp2 += Time.deltaTime;
            enemy.transform.position = Vector3.Lerp(WayPoint[0], WayPoint[1], tmp2 / DelayTime[1]);
        }
        else if (tmp3 < DelayTime[1])
        {
            tmp3 += Time.deltaTime;
            enemy.transform.position = Vector3.Lerp(WayPoint[1], WayPoint[0], tmp3 / DelayTime[1]);
        }
        else stateMachine.ChangeState(enemy.States[0]);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(lazerInstance != null)
            lazerInstance.transform.position = Muzzle.transform.position + bias;
    }
    public override void OnExitState()
    {
        base.OnExitState();
    }
}
