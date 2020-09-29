using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject _rocket;
    public GameObject _explosion;

    [HideInInspector]public bool isReady = true;
    public RocketWeaponeParms rocketWeaponeParms;
    private float rocketSpeed ;

    IEnumerator shoot(Transform rocketTransform, Transform enemyTransform,float damage )
    {
        isReady = false;
        Vector3 start_pos = rocketTransform.position;
        while(Vector3.Distance(rocketTransform.position,start_pos)<10f)
        {
            rocketTransform.Translate(Vector3.up*rocketSpeed*Time.deltaTime);
            yield return null;
        }

        Vector3 targetVector = enemyTransform.position - rocketTransform.position;
        
        while(targetVector.magnitude>1)
        {
            rocketTransform.Translate(Vector3.up*rocketSpeed*Time.deltaTime);
            targetVector = enemyTransform.position - rocketTransform.position;
            rocketTransform.up = Vector3.Slerp(rocketTransform.up, targetVector, rocketSpeed * Time.deltaTime);
            yield return null;
        }
        
        Instantiate(_explosion, enemyTransform.position, Quaternion.identity);
        Destroy(rocketTransform.gameObject);
        enemyTransform.GetComponent<CharacterTakeDamege>().TakeDamege(damage);
        
        isReady = true;
    }

    private void Start()
    {
        rocketSpeed = rocketWeaponeParms.rocketSpeed;
    }

    public void MakeShoot(Transform enemy,float damage)
    {
        if (isReady)
        {
            Transform rocket = Instantiate(_rocket,transform.position,Quaternion.identity).transform;
            StartCoroutine(shoot(rocket, enemy,damage));
        }
    }
    
    
}
