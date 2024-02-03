using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public EnemyState CurrentEnemyState { get; set; }
    public void Initilize(EnemyState StartState)
    {
        CurrentEnemyState = StartState;
        CurrentEnemyState.OnEnterState();
    }
    public void ChangeState(EnemyState enemyState)
    {
        CurrentEnemyState.OnExitState();
        CurrentEnemyState = enemyState;
        CurrentEnemyState.OnEnterState();
    }
}
