using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUICon : MonoBehaviour
{
    [SerializeField] Button StartBtn;
    void Awake()
    {
        StartBtn.onClick.AddListener(StartGameBtn);

    }
    void StartGameBtn()
    {
        SceneMana.instance.LoadGameScene();
    }
}
