using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharecterParmData  : ScriptableObject
{
    [Min(1.0f)] public float hp;

    [Min(0.0f)] public float defence;

    [Min(0.0f)] public float attack;

    [Min(0.0f)] public float speed;

    [Min(0.0f)] public float attacRange;

    [Min(0.0f)] public float retreatRadius;

    [Min(0.0f)] public float _parameterMultiplier;

    [Range(0, 1)] public float _critChanse;
    
    [Min(1.0f)]public float _critMultiplier;
}
