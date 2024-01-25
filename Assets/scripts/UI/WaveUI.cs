using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    Text waveRound;
    void Awake()
    {
        if (TryGetComponent<Canvas>(out Canvas canvas))
            canvas.worldCamera = Camera.main;
        waveRound = GetComponentInChildren<Text>();
    }
    void OnEnable()
    {
        waveRound.text = "-Wave " + EnemyMana.instance.WaveNum + "-";
    }
}
