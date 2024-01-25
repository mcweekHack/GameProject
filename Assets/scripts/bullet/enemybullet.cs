using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : bullet
{
    TrailRenderer trail;
    void Awake()
    {
        MoveSpeed = 10f;
        MoveDirect = new Vector2(-1, 0);
        trail = GetComponentInChildren<TrailRenderer>();
        bulletDamage = 10f;
    }
    void OnDisable()
    {
        trail.Clear();
    }
}
