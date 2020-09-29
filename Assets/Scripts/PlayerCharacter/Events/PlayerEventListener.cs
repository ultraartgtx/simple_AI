using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventListener : MonoBehaviour
{
    public PlayerEventScriptObj Event;
    public UnityEvent PlayerWinUniEvent;
    public UnityEvent PlayerDeathUniEvent;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventPlayerWin()
    {
        if (PlayerWinUniEvent != null)
        {
            PlayerWinUniEvent.Invoke(); 
        }
    }
    public void OnEventPlayerDeath()
    {
        if (PlayerDeathUniEvent != null)
        {
            PlayerDeathUniEvent.Invoke();
        }
    }
}
