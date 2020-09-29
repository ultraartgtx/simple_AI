using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterTakeDamege))]
[RequireComponent(typeof(CharacterParameters))]
[RequireComponent(typeof(CharacterCalculateDamage))]
public class PlayerCharacterControl : MonoBehaviour
{
    //events
    public PlayerEventScriptObj PlayerEventListener;
    private CharacterTakeDamege playerCharacterTakeDamege;
    
    //character parm scripts
    private CharacterParameters playerCharacterParameters;
    private CharacterCalculateDamage playerCharacterCalculateDamage;
    
    //parms
    private float playerAttackRange;
    private float rangeToCheckRaycast = 200f;
    private bool isGameOver = false;
    
    //FSM
    public Transform eyesTransform;
    private FSMSystem fsm;
    private NavMeshAgent playerAgent;
    private Transform finishTransform;
    private Transform playerTransform;
    //Weapon
    public Weapon weapon;
    //Spawn
    private SpawnEnemys spawnEnemys;



    void  GameOver()
    {
        isGameOver = true;
        playerAgent.Stop();
    }

    void characterDeath()
    {
        PlayerEventListener.PlayerDeath();
        GameOver();
    }

    private void OnDestroy()
    {
        unregisterReceivers();
    }

    void setComponents()
    {
        playerTransform = this.transform;
        playerCharacterTakeDamege = GetComponent<CharacterTakeDamege>();
        playerCharacterParameters = GetComponent<CharacterParameters>();
        playerCharacterCalculateDamage = GetComponent<CharacterCalculateDamage>(); 
        playerAgent = GetComponent<NavMeshAgent>();
        spawnEnemys = FindObjectOfType<SpawnEnemys>();
        finishTransform=GameObject.FindGameObjectWithTag("Finish").transform;
    }

    void registerReceivers()
    {
        playerCharacterTakeDamege.OnCharacterDeath += characterDeath; 
    }
    void unregisterReceivers()
    {
        playerCharacterTakeDamege.OnCharacterDeath -= characterDeath; 
    }

    void setParms()
    {
        playerAgent.speed = playerCharacterParameters.speed;
        playerAttackRange = playerCharacterParameters.attackRange;
        this.gameObject.tag = "Player";
    }

    public void Start()
    {
        setComponents();
        setParms();
        registerReceivers();
        MakeFSM();
    }

    bool IsVisible(Transform target)
    {
            RaycastHit hit;
            Vector3 direction = (( target.position+Vector3.up)-(eyesTransform.position+Vector3.up)).normalized;
            
            if (Physics.Raycast(eyesTransform.position+Vector3.up, direction, out hit, rangeToCheckRaycast))
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
        if (!isGameOver)
        {
            if (spawnEnemys.enemys.Count > 0)
            {
                Transform target= spawnEnemys.enemys[0];
                float distance = Vector3.Distance(playerTransform.position, spawnEnemys.enemys[0].position);
            
                for (int count = 1; count < spawnEnemys.enemys.Count; count++)
                {
                    bool isVisible = IsVisible(spawnEnemys.enemys[count]);
                    if (Vector3.Distance(playerTransform.position, spawnEnemys.enemys[count].position) < distance && isVisible)
                    {
                        target = spawnEnemys.enemys[count];
                        distance = Vector3.Distance(playerTransform.position, spawnEnemys.enemys[count].position);
                    }
                }
            
                if (weapon.isReady&&IsVisible(target)&&distance<=playerAttackRange)
                {
                    weapon.MakeShoot(target,playerCharacterCalculateDamage.calculateDamage(playerCharacterParameters.attack));
                }
                fsm.CurrentState.Reason(playerTransform, target); 
            
            }
            fsm.CurrentState.Act(playerAgent, finishTransform.position);
        }

    }
 
	// The NPC has two states: FollowPath and ChasePlayer
	// If it's on the first state and SawPlayer transition is fired, it changes to ChasePlayer
	// If it's on ChasePlayerState and LostPlayer transition is fired, it returns to FollowPath
    
    public void SetTransition(Transition t) { fsm.PerformTransition(t); }
    private void MakeFSM()
    {
        FollowPathState follow = new FollowPathState(playerCharacterParameters.retreatRadius);
        follow.AddTransition(Transition.SawEnemy, StateID.ChasingEnemy);
 
        ChasePlayerState chase = new ChasePlayerState(playerCharacterParameters.retreatRadius);
        chase.AddTransition(Transition.LostEnemy, StateID.FollowingPath);
 
        fsm = new FSMSystem();
        fsm.AddState(follow);
        fsm.AddState(chase);
    }
}

