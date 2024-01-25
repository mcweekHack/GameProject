using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energy_system : Singleton<energy_system>
{
    [SerializeField] Stutas_bar_HUD BarUI;
    [SerializeField] float MaxEnergy;
    [SerializeField] float CurrentEnergy;
    [SerializeField] public float HitBouns;
    [SerializeField] public float KillBouns;
    WaitForSeconds GoDownDelay;
    [SerializeField] public float DownCost;
    bool IsAvaiable = true;

    Coroutine GodownCoro;
    void Start()
    {
        MaxEnergy = 100f;
        CurrentEnergy = 50f;
        HitBouns = 0.01f;
        KillBouns = 0.05f;
        BarUI.Initialize(MaxEnergy, CurrentEnergy);
        GoDownDelay = new WaitForSeconds(0.1f);
        DownCost = 1f;
    }
    void OnEnable()
    {
        PlayerOverdrive.on += OverdirveOn;
        PlayerOverdrive.off += OverdirveOff;
    }
    void OnDisable()
    {
        PlayerOverdrive.on -= OverdirveOn;
        PlayerOverdrive.off -= OverdirveOff;
    }
    public void GetEnergy(float bias)
    {
        if (IsAvaiable&&gameObject.activeSelf)
        {
            CurrentEnergy += MaxEnergy * bias;
            CurrentEnergy = CurrentEnergy > MaxEnergy ? MaxEnergy : CurrentEnergy;
            BarUI.UpdateStatus(MaxEnergy, CurrentEnergy);
        }
    }
    public bool IsEnough(float cost)
    {
        return cost <= CurrentEnergy;
    }
    public bool IsFullEnergy()
    {
        return MaxEnergy == CurrentEnergy;
    }
    public bool UseEnergy(float cost)
    {
        if (!IsEnough(cost)) return false;
        CurrentEnergy -= cost;
        BarUI.UpdateStatus(MaxEnergy, CurrentEnergy);
        if (CurrentEnergy == 0 && !IsAvaiable)
            PlayerOverdrive.off.Invoke();
        return true;
    }




    void OverdirveOn()
    {
        if (GodownCoro != null)
            StopCoroutine(GodownCoro);
        IsAvaiable = false;
        GodownCoro = StartCoroutine(EnergyGoDownCoroutine());
    }
    void OverdirveOff()
    {
        if (GodownCoro != null)
            StopCoroutine(GodownCoro);
        IsAvaiable = true;
    }

    IEnumerator EnergyGoDownCoroutine()
    {
        while (gameObject.activeSelf&& CurrentEnergy>0)
        {
            yield return GoDownDelay;
            UseEnergy(DownCost);
        }
    }

}
