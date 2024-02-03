using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUICon : MonoBehaviour
{
    [SerializeField] playerinput input_;
    [SerializeField] Canvas PauseMenu;
    [SerializeField] Canvas HUD_;

    [SerializeField] Button btn_resume;
    [SerializeField] Button btn_opt;
    [SerializeField] Button btn_main;
    void Start()
    {
        PauseMenu.enabled = false;
        HUD_.enabled = true;
    }
    void OnEnable()
    {
        input_.onPause += OnPause;
        input_.onContiune += OnContinue;

        BtnPressed.BtnFuncTable.Add(btn_resume.gameObject.name, ResumeBtnFunc);
        BtnPressed.BtnFuncTable.Add(btn_opt.gameObject.name, OptBtnFunc);
        BtnPressed.BtnFuncTable.Add(btn_main.gameObject.name, MainMenuBtnFunc);
    }
    void OnDisable()
    {
        input_.onPause -= OnPause;
        input_.onContiune -= OnContinue;
    }
    void OnPause()
    {
        input_.EnableMenuMap();
        TimeMana.instance.PauseGame();
        PauseMenu.enabled = true;
        HUD_.enabled = false;
        UIController.instance.SelectUI(btn_resume);
    }
   void OnContinue()
    {
        input_.EnablePlayerMap();
        TimeMana.instance.ContinueGame();
        PauseMenu.enabled = false;
        HUD_.enabled = true;
    }
    void OnOptBtn()
    {
        UIController.instance.SelectUI(btn_opt);
        input_.EnableMenuMap();

    }
    void OnMainMenuBtn()
    {
        UIController.instance.SelectUI(btn_main);
        input_.EnableMenuMap();
        SceneMana.instance.BackToStartMenu();
    }



    void ResumeBtnFunc()
    {
        OnContinue();
    }
    void MainMenuBtnFunc()
    {
        OnMainMenuBtn();
    }
    void OptBtnFunc()
    {
        OnOptBtn();
    }
}
