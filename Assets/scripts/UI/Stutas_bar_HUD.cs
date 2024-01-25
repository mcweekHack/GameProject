using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stutas_bar_HUD : Stutas_bar
{
    [SerializeField]Text percent;
    void SetPercent(float per)
    {
        percent.text = Mathf.RoundToInt(per*100f)+"%";
    }
    public override void Initialize(float MaxVal, float Val)
    {
        base.Initialize(MaxVal,Val);
        SetPercent(Val/MaxVal);
    }
    protected override IEnumerator FillTransforming(Image ima, float From_, float To_, float DelyTime)
    {
       SetPercent(To_);
       return base.FillTransforming(ima,From_,To_,DelyTime);
    }
}
