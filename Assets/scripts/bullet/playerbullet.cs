using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerbullet : bullet
{
    TrailRenderer trail;
    void Awake()
    {
        MoveSpeed = 20f;
        MoveDirect = new Vector2(1, 0);
        trail = GetComponentInChildren<TrailRenderer>();
        bulletDamage = 10f;
    }
    protected virtual void OnDisable()
    {
        trail.Clear();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        energy_system.instance.GetEnergy(energy_system.instance.HitBouns);
    }
}
