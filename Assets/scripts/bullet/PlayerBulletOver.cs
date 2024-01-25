using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletOver : playerbullet
{
    GameObject target;
    Coroutine TrackCoro;
    Vector2 tmp;
    float angle;
    float biasAngleMax = 30f;
    float biasAngleMin = -30f;
    float tmpbiasAngle;
    protected override void OnEnable()
    {
        base.OnEnable();
        if (TrackCoro != null)
            StopCoroutine(TrackCoro);
        TrackCoro = StartCoroutine(TrackingCoroutine());
    }
    protected override void OnDisable()
    {
        if (TrackCoro != null)
            StopCoroutine(TrackCoro);
        base.OnDisable();
    }
    IEnumerator TrackingCoroutine()
    {
        target = EnemyMana.ReturnAliveEnemy();
        tmpbiasAngle = Random.Range(biasAngleMin, biasAngleMax);
        while (target!=null&&target.activeSelf)
        {
            tmp = target.transform.position - transform.position;
            angle = Mathf.Atan2(tmp.y, tmp.x)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation *= Quaternion.Euler(0f, 0f, tmpbiasAngle);
            MoveDirect = transform.right;
            yield return null;
        }
    }
}
