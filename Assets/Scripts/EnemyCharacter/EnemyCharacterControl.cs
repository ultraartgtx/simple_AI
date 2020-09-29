using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))] 
[RequireComponent (typeof (CharacterTakeDamege))] 
[RequireComponent (typeof (CharacterParameters))] 
[RequireComponent(typeof(CharacterCalculateDamage))]
[RequireComponent(typeof(AnimationEvent))]
[RequireComponent(typeof(Animator))]
public class EnemyCharacterControl : MonoBehaviour
{
    //animation
    private Animator animator;
    private const string animationAttackName = "Attack1Trigger";
    
    //events
    private AnimationEvent  AnimationEvent;
    
    //character parm scripts
    private CharacterTakeDamege  characterTakeDamegeSctipt;
    private CharacterParameters  enemyParms;
    private CharacterCalculateDamage  characterCalculateDamage;
    
    //parms
    private bool isGameOver = false;
    private float attack;
    private float attackRange;
    
    //Navigation
    private NavMeshAgent  agent;
    private GameObject  player;
    private Transform  target;
    private Transform enemyTransform;
  
   
    //Spawn
    private SpawnEnemys  spawnEnemys;
    
    

    public void GameOver()
    {
        isGameOver = true;
         agent.Stop();
    }

    private void OnDestroy()
    {
        unregisterReceivers();
    }

    void death()
    {
         spawnEnemys.enemys.Remove(this.transform);
        Destroy(this.gameObject);
    }

    void GiveDamage()
    {
         player.GetComponent<CharacterTakeDamege>().TakeDamege( characterCalculateDamage.calculateDamage(attack));
    }

    
    void setComponents()
    {
         characterCalculateDamage = GetComponent<CharacterCalculateDamage>();
         AnimationEvent = GetComponent<AnimationEvent>();
         animator = GetComponent<Animator>();
         enemyParms = GetComponent<CharacterParameters>();
         characterTakeDamegeSctipt = GetComponent<CharacterTakeDamege>();
         agent = GetComponent<NavMeshAgent>();
         spawnEnemys = FindObjectOfType<SpawnEnemys>();
         player=GameObject.FindGameObjectWithTag("Player");
    }

    void registerReceivers()
    {
         AnimationEvent.OnHit += GiveDamage;
         characterTakeDamegeSctipt.OnCharacterDeath += death;
    }
    void unregisterReceivers()
    {
         AnimationEvent.OnHit -= GiveDamage;
         characterTakeDamegeSctipt.OnCharacterDeath -= death;
    }

    void setParms()
    {
         agent.speed =  enemyParms.speed;
         target =  player.transform;
         attack =  enemyParms.attack;
         attackRange =  1.7f;
         enemyTransform = this.transform;
    }
    void Start()
    {
        setComponents();
        registerReceivers();
        setParms();
    }



    void FixedUpdate()
    {
        if (!isGameOver)
        {
            
            agent.SetDestination( target.position);
            if (Vector3.Distance( target.position , enemyTransform.position) <= attackRange) {
                animator.SetTrigger(animationAttackName);
            }
        }
    }
}
