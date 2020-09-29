using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterTakeDamege))]
[RequireComponent(typeof(CharacterParameters))]
[RequireComponent(typeof(CharacterCalculateDamage))]
public class characterControl : MonoBehaviour
{
    public Transform _eyes;
    private FSMSystem fsm;
    private NavMeshAgent _agent;
    private Transform _finish;
    private Transform _playerTransform;
    public Weapon _Weapon;
    private SpawnEnemys _spawnEnemys;
    private CharacterTakeDamege _characterTakeDamege;
    private CharacterParameters _characterParameters;
    public GameEvent _GameEvent;
    private float _attacRange;
    private float rangeToCheckRaycast = 200f;
    private CharacterCalculateDamage _characterCalculateDamage;
    public void SetTransition(Transition t) { fsm.PerformTransition(t); }

    void characterDeath()
    {
        _GameEvent.PlayerDeath();
        print("characterDeath");
    }

    private void OnDestroy()
    {
        _characterTakeDamege.OnCharacterDeath -= characterDeath;
    }

    public void Start()
    {
        _playerTransform = this.transform;
        _characterTakeDamege = GetComponent<CharacterTakeDamege>();
        _characterParameters = GetComponent<CharacterParameters>();
        _characterCalculateDamage = GetComponent<CharacterCalculateDamage>();
        _spawnEnemys = FindObjectOfType<SpawnEnemys>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _characterParameters.speed;
        _finish=GameObject.FindGameObjectWithTag("Finish").transform;
        _attacRange = _characterParameters.attacRange;
        _characterTakeDamege.OnCharacterDeath += characterDeath;
        this.gameObject.tag = "Player";
        MakeFSM();
    }

    bool IsVisible(Transform target)
    {
            RaycastHit hit;
            Vector3 direction = (( target.position+Vector3.up)-(_eyes.position+Vector3.up)).normalized;
            
            if (Physics.Raycast(_eyes.position+Vector3.up, direction, out hit, rangeToCheckRaycast))
            {

                if (hit.collider.transform == target)
                {
                    return true;
                }
            }
        
            return false;
    }

    public void FixedUpdate()
    {
        
        if (_spawnEnemys.enemys.Count > 0)
        {
            Transform target= _spawnEnemys.enemys[0];
            float distance = Vector3.Distance(_playerTransform.position, _spawnEnemys.enemys[0].position);
            
            for (int count = 1; count < _spawnEnemys.enemys.Count; count++)
            {
                    bool isVisible = IsVisible(_spawnEnemys.enemys[count]);
                if (Vector3.Distance(_playerTransform.position, _spawnEnemys.enemys[count].position) < distance && isVisible)
                {
                    target = _spawnEnemys.enemys[count];
                    distance = Vector3.Distance(_playerTransform.position, _spawnEnemys.enemys[count].position);
                }
            }
            
            if (_Weapon.isReady&&IsVisible(target))
            {
                _Weapon.MakeShoot(target,_characterCalculateDamage.calculateDamage(_characterParameters.attack));
            }
            fsm.CurrentState.Reason(_playerTransform, target); 
            
        }
        fsm.CurrentState.Act(_agent, _finish.position);
    }
 
	// The NPC has two states: FollowPath and ChasePlayer
	// If it's on the first state and SawPlayer transition is fired, it changes to ChasePlayer
	// If it's on ChasePlayerState and LostPlayer transition is fired, it returns to FollowPath
    private void MakeFSM()
    {
        FollowPathState follow = new FollowPathState(_characterParameters.retreatRadius);
        follow.AddTransition(Transition.SawEnemy, StateID.ChasingEnemy);
 
        ChasePlayerState chase = new ChasePlayerState(_characterParameters.retreatRadius);
        chase.AddTransition(Transition.LostEnemy, StateID.FollowingPath);
 
        fsm = new FSMSystem();
        fsm.AddState(follow);
        fsm.AddState(chase);
    }
}

