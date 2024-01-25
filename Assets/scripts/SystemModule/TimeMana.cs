using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMana : Singleton<TimeMana>
{
    [SerializeField,Range(0f,1f)] float TimeScale;
    float defaultTime;
    protected override void Awake()
    {
        base.Awake();
        defaultTime = Time.fixedDeltaTime;
    }
    public void SetTimeScale()
    {
        Time.timeScale = TimeScale;
        Time.fixedDeltaTime = defaultTime * Time.timeScale;
    }
}
