using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerOverdrive : MonoBehaviour
{
    public static UnityAction on = delegate { };
    public static UnityAction off = delegate { };
    [SerializeField] GameObject OverdirveVFX;
    [SerializeField] GameObject NormalVFX;
    [SerializeField] AudioData OverdriveSFX;
    [SerializeField] TrailRenderer trail;

    void OnEnable()
    {
        on += On;
        off += Off;
        OverdirveVFX.SetActive(false);
    }
    void OnDisable()
    {
        on -= On;
        off -= Off;
    }
    void On()
    {
        AudioMana.instance.playSFXRandomly(OverdriveSFX);
        NormalVFX.SetActive(false);
        OverdirveVFX.SetActive(true);
    }
    void Off()
    {
        NormalVFX.SetActive(true);
        trail.Clear();
        OverdirveVFX.SetActive(false);
    }
}
