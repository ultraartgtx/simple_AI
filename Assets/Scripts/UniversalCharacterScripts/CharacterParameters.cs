using UnityEngine;


public class CharacterParameters : MonoBehaviour
{
    public CharecterParmData  _CharacterParametersData;
    [HideInInspector] public float hp;
    [HideInInspector] public float defence;
    [HideInInspector] public float attack;
    [HideInInspector] public float speed;
    [HideInInspector] public float attacRange;
    [HideInInspector] public float retreatRadius;
    [HideInInspector] public float critChanse;
    [HideInInspector] public float critMultiplier;


    float parmMultiplier(float parm)
    {
        return parm + (_CharacterParametersData._parameterMultiplier * LevelData.generation * parm);
    }

    void setCharacterParms()
    {
        hp = parmMultiplier(_CharacterParametersData.hp);
        defence = parmMultiplier(_CharacterParametersData.defence);
        attack = parmMultiplier(_CharacterParametersData.attack);
        critChanse = parmMultiplier(_CharacterParametersData._critChanse);
        critMultiplier = parmMultiplier(_CharacterParametersData._critMultiplier);
        
        speed = _CharacterParametersData.speed;
        attacRange = _CharacterParametersData.attacRange;
        retreatRadius = _CharacterParametersData.retreatRadius;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        setCharacterParms();
    }

   
}
