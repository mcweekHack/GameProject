using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExpBullet : bullet
{
    private void Awake()
    {
        MoveSpeed = 5f;
        bulletDamage = 5f;
    }
    public void ChangeDirection(Vector3 dir)
    {
        MoveDirect = dir.normalized;
        return;
    }
}
