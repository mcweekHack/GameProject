using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackBullet : bullet
{
    [SerializeField] GameObject target;
    Coroutine AmingCoro;
    Vector2 Goto;
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        MoveDirect = new Vector2(-1, 0);
        MoveSpeed = 6f;
        bulletDamage = 5f;
    }
   protected override void OnEnable()
    {
        AmingCoro = StartCoroutine(AimingCoroutine());
    }
    IEnumerator AimingCoroutine()
    {
        yield return null;
        if (target!=null&&target.activeSelf)
        {
            Goto = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y).normalized;
            transform.right = Goto*-1f;
        }
        Coro = StartCoroutine(BulletMove());
    }
}
