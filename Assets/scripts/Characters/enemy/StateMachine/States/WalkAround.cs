using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class WalkAround : IdleState
{
    float TimeTag;
    float TimeDelay;
    int mak;
    public WalkAround(BossCon enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {}
    public override void OnEnterState()
    {
        base.OnEnterState();
        TimeTag = 0f;
        TimeDelay = Random.Range(0f,2f);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (TimeTag < TimeDelay) TimeTag += Time.deltaTime;
        else
        {
            mak = StateIndexBack(enemy.States);
            stateMachine.ChangeState(enemy.States[mak]);
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


    int StateIndexBack(List<EnemyState> tmp)
    {
        int len = tmp.Count;
        return Random.Range(1, len);
    }

}
