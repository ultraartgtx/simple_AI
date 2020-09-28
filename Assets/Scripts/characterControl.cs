using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterTakeDamege))]
[RequireComponent(typeof(CharacterParameters))]
public class characterControl : MonoBehaviour
{
    public Transform _eyes;
    private FSMSystem fsm;
    private NavMeshAgent _agent;
    private GameObject _finish;
    public Weapon _Weapon;
    private SpawnEnemys _spawnEnemys;
    private CharacterTakeDamege _characterTakeDamege;
    private CharacterParameters _characterParameters;
    public GameEvent _GameEvent;
    private float _attacRange;
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
        _characterTakeDamege = GetComponent<CharacterTakeDamege>();
        _characterParameters = GetComponent<CharacterParameters>();
        _spawnEnemys = FindObjectOfType<SpawnEnemys>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _characterParameters.speed;
        _finish=GameObject.FindGameObjectWithTag("Finish");
        _attacRange = _characterParameters.attacRange;
        _characterTakeDamege.OnCharacterDeath += characterDeath;
        this.gameObject.tag = "Player";
        MakeFSM();
    }

    bool IsVisible(Transform target)
    {
        
            RaycastHit hit;
            Vector3 direction = (( target.position+Vector3.up)-(_eyes.position+Vector3.up)).normalized;
                
            //just for see logic of choosing targets
            Debug.DrawRay(_eyes.position+Vector3.up,direction*500,Color.red);
            
            if (Physics.Raycast(_eyes.position+Vector3.up, direction, out hit, 200f))
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
            float distance = Vector3.Distance(transform.position, _spawnEnemys.enemys[0].position);
            foreach (Transform enemy in _spawnEnemys.enemys)
            {
                bool isVisible = IsVisible(target);
                if (Vector3.Distance(transform.position, enemy.position) < distance && isVisible)
                {
                    target = enemy;
                    distance = Vector3.Distance(transform.position, enemy.position);
                }
                
            }
            if (_Weapon.isReady&&IsVisible(target))
            {
                _Weapon.MakeShoot(target,_characterParameters.attack);
            }
            fsm.CurrentState.Reason(transform, target); 
        }
        fsm.CurrentState.Act(_agent, _finish.transform.position);
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

