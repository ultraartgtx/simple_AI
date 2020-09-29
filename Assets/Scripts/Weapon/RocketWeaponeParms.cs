using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class RocketWeaponeParms : ScriptableObject
{
   [Min(0.0f)] public float rocketSpeed;
}
