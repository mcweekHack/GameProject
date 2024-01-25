using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "playerinput")]
public class playerinput : ScriptableObject,playercon.IPlayerActions
{
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction stopMove = delegate { };
    public event UnityAction onFire = delegate { };
    public event UnityAction stopFire = delegate { };
    public event UnityAction SkillDoge = delegate { };
    public event UnityAction onOverdirve = delegate { };
    public event UnityAction stopOverdirve = delegate { };


    playercon con;

    void OnEnable()
    {
        con = new playercon();
        con.player.SetCallbacks(this);
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
    }
    void OnDisable()
    {
        DisableCon();
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
}
