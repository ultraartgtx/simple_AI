using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))] 
[RequireComponent (typeof (CharacterTakeDamege))] 
[RequireComponent (typeof (CharacterParameters))] 
public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private SpawnEnemys _spawnEnemys;
    private NavMeshAgent _agent;
    private GameObject _player;
    private Transform _target;
    public Animator _animator;
    private AnimationEvent _AnimationEvent;
    private CharacterTakeDamege _characterTakeDamegeSctipt;
    private CharacterParameters _enemyParms;
    
    private void OnDestroy()
    {
        _AnimationEvent.OnHit -= GiveDamage;
        _characterTakeDamegeSctipt.OnCharacterDeath -= death;
    }

    void death()
    {
        _spawnEnemys.enemys.Remove(this.transform);
        Destroy(this.gameObject);
    }

    void GiveDamage()
    {
        _player.GetComponent<CharacterTakeDamege>().TakeDamege(_enemyParms.attack);
        print("GiveDamage.enemy");
    }

    void Start()
    {
        _AnimationEvent = GetComponent<AnimationEvent>();
        _enemyParms = GetComponent<CharacterParameters>();
        _AnimationEvent.OnHit += GiveDamage;
        _spawnEnemys = FindObjectOfType<SpawnEnemys>();
        _characterTakeDamegeSctipt = GetComponent<CharacterTakeDamege>();
        _characterTakeDamegeSctipt.OnCharacterDeath += death;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _enemyParms.speed;
        _player=GameObject.FindGameObjectWithTag("Player");
        _target = _player.transform;
    }

    
    void FixedUpdate()
    {
        
        _agent.SetDestination(_target.position);
        if (Vector3.Distance(_target.position , transform.position) < 3) {
            _animator.SetTrigger("Attack1Trigger");
        }

    }
}
