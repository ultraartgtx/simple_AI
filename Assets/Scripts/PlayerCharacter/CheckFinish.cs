using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
    public PlayerEventScriptObj  gameEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameEvent.PlayerWin();
        }
    }
    
}
