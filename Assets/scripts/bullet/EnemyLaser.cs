using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class EnmeyLaser : Laser
{
    [SerializeField] float HurtDamage;
    Transform self;
    [SerializeField]float Duration;
    float MinScale;
    float MaxScale;
    float LastTime;
    float TimeTag;
    Vector3 tmp;
    Vector3 tmp2;
    Coroutine LinerCoro;
    bool flag;
    private void Awake()
    {
        MinScale = -0.8f;
        MaxScale = 7f;
        LastTime = 1f;
        self = GetComponent<Transform>();
        LinerCoro = null;
        flag = false;
    }
    private void OnEnable()
    {
        BulletDamage = HurtDamage;
        Duration = 4f;
        if(LinerCoro!=null) StopCoroutine(LinerCoro);
        LinerCoro = StartCoroutine(LaserWidthChangeCoroutine(MaxScale));
        flag = false;
    }
    private void Update()
    {
        if (!flag)
        {
            if (Duration < 0)
            {
                if (LinerCoro != null) StopCoroutine(LinerCoro);
                LinerCoro = StartCoroutine(LaserWidthChangeCoroutine(MinScale));
                flag = true;
            }
            else Duration -= Time.deltaTime;
        }
    }
    IEnumerator LaserWidthChangeCoroutine(float to_)
    {
        TimeTag = 0f;
        tmp = transform.localScale;
        tmp2 = new Vector3(to_,1f,1f);
        while (TimeTag < LastTime)
        {
            transform.localScale = Vector3.Lerp(tmp, tmp2, TimeTag / LastTime);
            yield return null;
            TimeTag += Time.deltaTime;
        }
        if(to_ == MinScale) self.gameObject.SetActive(false);
    }
}
