using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stutas_bar : MonoBehaviour
{
    Canvas canvas;
    protected float MaxValue;
    protected float Value;
    [SerializeField] Image BackImage;
    [SerializeField] Image FrontImage;
    Coroutine TransformCoro;
    [SerializeField] float DelayTime;
    float t;
    void Awake()
    {
        if(TryGetComponent<Canvas>(out Canvas canvas))
            canvas.worldCamera = Camera.main;
        TransformCoro = null;
    }
    public virtual void Initialize(float MaxVal,float Val)
    {
        MaxValue = MaxVal;
        Value = Val;
        BackImage.fillAmount = Val / MaxValue;
        FrontImage.fillAmount = Val / MaxValue;
        DelayTime = 2f;
    }
    public void UpdateStatus(float MaxVal, float Val)
    {
        if (TransformCoro != null)
            StopCoroutine(TransformCoro);
        if (Value > Val)
        {
            FrontImage.fillAmount = Val / MaxVal;
            TransformCoro = StartCoroutine(FillTransforming(BackImage,Value / MaxVal, Val / MaxVal, DelayTime* (Value - Val) / MaxVal));
        }
        else
        {
            BackImage.fillAmount = Val / MaxVal;
            TransformCoro = StartCoroutine(FillTransforming(FrontImage, Value / MaxVal, Val / MaxVal, DelayTime* (Val - Value) / MaxVal));
        }
        MaxValue = MaxVal;
        Value = Val;
    }
    protected virtual IEnumerator FillTransforming(Image ima,float From_,float To_,float DelyTime)
    {
        t = 0f;
        while (t < DelyTime)
        {
            t += Time.fixedDeltaTime;
            ima.fillAmount = Mathf.Lerp(From_, To_, t / DelyTime);
            yield return null;
        }
    }
}
