using UnityEngine;
[RequireComponent(typeof(CharacterParameters))]
public class CharacterTakeDamege : MonoBehaviour
{
    private CharacterParameters _characterParameters;
    public delegate void CharacterDeath(); 
    public event CharacterDeath OnCharacterDeath;
    
    private void Start()
    {
        _characterParameters = GetComponent<CharacterParameters>();
    }

    public void TakeDamege(float damage)
    {
        float damageAfterCalc = damage;
        
        if (_characterParameters.defence > 0)
        {
             damageAfterCalc = damage / 2;
            _characterParameters.defence -= damageAfterCalc;
        }
        
        _characterParameters.hp -= damageAfterCalc;
        if (_characterParameters.hp <= 0)
        {
            if (OnCharacterDeath != null) OnCharacterDeath();
        }
    }
}
