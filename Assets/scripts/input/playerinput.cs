using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "playerinput")]
public class playerinput : ScriptableObject,playercon.IPlayerActions,playercon.IPauseMenuActions
{
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction stopMove = delegate { };
    public event UnityAction onFire = delegate { };
    public event UnityAction stopFire = delegate { };
    public event UnityAction SkillDoge = delegate { };
    public event UnityAction onOverdirve = delegate { };
    public event UnityAction stopOverdirve = delegate { };
    public event UnityAction onPause = delegate { };
    public event UnityAction stopPause = delegate { };
    public event UnityAction onContiune = delegate { };
    public event UnityAction stopContiune = delegate { };


    playercon con;
    void OnEnable()
    {
        con = new playercon();
        con.player.SetCallbacks(this);
        con.PauseMenu.SetCallbacks(this);
        EnableCon();
    } 
   public void EnableCon()
    {
        con.player.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void DisableCon()
    {
        con.player.Disable();
        con.PauseMenu.Disable();
    }
    void OnDisable()
    {
        DisableCon();
    }



    void SwitchActionMap(InputActionMap map,bool IsUIInput)
    {
        con.Disable();
        map.Enable();
        if (IsUIInput)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = IsUIInput;
    }
    public void EnablePlayerMap()
    {
        SwitchActionMap(con.player,false);
    }
    public void EnableMenuMap()
    {
        SwitchActionMap(con.PauseMenu, true);
    }




    public void OnPlayermove(InputAction.CallbackContext context)
    {
        if (context.performed)
            onMove.Invoke(context.ReadValue<Vector2>());
        if (context.canceled)
            stopMove.Invoke();
    }
    public void OnPlayerFire(InputAction.CallbackContext context)
    {
        if (context.performed)
            onFire.Invoke();
        if (context.canceled)
            stopFire.Invoke();
    }
    public void OnDoge(InputAction.CallbackContext context)
    {
        if (context.performed)
            SkillDoge.Invoke();
    }
    public void OnOverdrive(InputAction.CallbackContext context)
    {
        if (context.performed)
            onOverdirve.Invoke();
        if (context.canceled)
            stopOverdirve.Invoke();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
            onPause.Invoke();
        if (context.canceled)
            stopPause.Invoke();
    }
   public void OnContiune(InputAction.CallbackContext context)
    {
        if (context.performed)
            onContiune.Invoke();
        if (context.canceled)
            stopContiune.Invoke();
    }
}
