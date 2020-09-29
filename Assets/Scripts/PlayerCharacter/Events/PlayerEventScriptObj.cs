using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerEventScriptObj : ScriptableObject
{
    private List<PlayerEventListener> listeners = 
        new List<PlayerEventListener>();

    public void PlayerDeath()
    {
        for(int i = listeners.Count -1; i >= 0; i--)
            listeners[i].OnEventPlayerDeath();
    }
    public void PlayerWin()
    {
        
        for(int i = listeners.Count -1; i >= 0; i--)
            listeners[i].OnEventPlayerWin();
    }

    public void RegisterListener(PlayerEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(PlayerEventListener listener)
    {
        listeners.Remove(listener);
    }
}