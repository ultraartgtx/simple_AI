using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterParameters))]
public class CharacterTakeDamege : MonoBehaviour
{
    private CharacterParameters _characterParameters;
    public delegate void CharacterDeath(); 
    public event CharacterDeath OnCharacterDeath; 

    private void Start()
    {
        _characterParameters = FindObjectOfType<CharacterParameters>();
    }

    public void TakeDamege(float damage)
    {
        _characterParameters.hp -= damage;
        if (_characterParameters.hp <= 0)
        {
            if (OnCharacterDeath != null) OnCharacterDeath();
        }
    }
}
