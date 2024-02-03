using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyState
{
    protected BossCon enemy;
    protected StateMachine stateMachine;
    public EnemyState(BossCon enemy, StateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }
    public virtual void OnEnterState() { }
    public virtual void FrameUpdate() { }
    public virtual void FixedUpdate() { }
    public virtual void OnExitState() { }
    public virtual void AnimationTriggeredEvent(BossCon.AnimationTriggerType triggerType) { }

}
