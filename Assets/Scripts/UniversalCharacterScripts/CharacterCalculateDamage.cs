using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterParameters))]
public class CharacterCalculateDamage : MonoBehaviour
{
    private CharacterParameters _characterParameters;
    // Start is called before the first frame update
    void Start()
    {
       _characterParameters=GetComponent<CharacterParameters>();
    }

    public float calculateDamage(float damage)
    {
        if (_characterParameters.critChanse > 0)
        {
            if (_characterParameters.critChanse >= Random.Range(0, 1f))
            {
                return damage * _characterParameters.critMultiplier;
            }
        }

        return damage;
    }


}
