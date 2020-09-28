using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class CharacterParameters : MonoBehaviour
{
    public CharecterParmData  _CharacterParametersData;
    
     public float hp;
    [HideInInspector] public float defence;
    [HideInInspector] public float attack;
    [HideInInspector] public float speed;
    [HideInInspector]public float attacRange;
    [HideInInspector]public float retreatRadius;
    
    
    void setCharacterParms()
    {
        hp = _CharacterParametersData.hp;
        defence = _CharacterParametersData.defence;
        attack = _CharacterParametersData.attack;
        speed = _CharacterParametersData.speed;
        attacRange = _CharacterParametersData.attacRange;
        retreatRadius = _CharacterParametersData.retreatRadius;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        setCharacterParms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
