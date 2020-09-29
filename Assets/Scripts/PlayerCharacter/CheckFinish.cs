using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
    public PlayerEventScriptObj _gameEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           _gameEvent.PlayerWin();
        }
    }
    
}
