using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SpawnParmsData : ScriptableObject
{
    [Min(0.0f)] public float timeToSpawnEnemy;
}
