using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))] 
[RequireComponent (typeof (CharacterTakeDamege))] 
[RequireComponent (typeof (CharacterParameters))] 
[RequireComponent(typeof(CharacterCalculateDamage))]
[RequireComponent(typeof(AnimationEvent))]
public class EnemyCharacterControl : MonoBehaviour
{
    //animation
    public Animator _animator;
    private const string animationAttackName = "Attack1Trigger";
    
    //events
    private AnimationEvent _AnimationEvent;
    
    //character parm scripts
    private CharacterTakeDamege _characterTakeDamegeSctipt;
    private CharacterParameters _enemyParms;
    private CharacterCalculateDamage _characterCalculateDamage;
    
    //parms
    private bool isGameOver = false;
    private float attac=0;
    private float attacRange = 0;
    
    //Navigation
    private NavMeshAgent _agent;
    private GameObject _player;
    private Transform _target;
  
   
    //Spawn
    private SpawnEnemys _spawnEnemys;
    
    

    public void GameOver()
    {
        isGameOver = true;
        _agent.Stop();
    }

    private void OnDestroy()
    {
        unregisterReceivers();
    }

    void death()
    {
        _spawnEnemys.enemys.Remove(this.transform);
        Destroy(this.gameObject);
    }

    void GiveDamage()
    {
        _player.GetComponent<CharacterTakeDamege>().TakeDamege(_characterCalculateDamage.calculateDamage(attac));
    }

    
    void setComponents()
    {
        _characterCalculateDamage = GetComponent<CharacterCalculateDamage>();
        _AnimationEvent = GetComponent<AnimationEvent>();
        _enemyParms = GetComponent<CharacterParameters>();
        _characterTakeDamegeSctipt = GetComponent<CharacterTakeDamege>();
        _agent = GetComponent<NavMeshAgent>();
        _spawnEnemys = FindObjectOfType<SpawnEnemys>();
        _player=GameObject.FindGameObjectWithTag("Player");
    }

    void registerReceivers()
    {
        _AnimationEvent.OnHit += GiveDamage;
        _characterTakeDamegeSctipt.OnCharacterDeath += death;
    }
    void unregisterReceivers()
    {
        _AnimationEvent.OnHit -= GiveDamage;
        _characterTakeDamegeSctipt.OnCharacterDeath -= death;
    }

    void setParms()
    {
        _agent.speed = _enemyParms.speed;
        _target = _player.transform;
        attac = _enemyParms.attack;
        attacRange = _enemyParms.attacRange;
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
            _agent.SetDestination(_target.position);
            if (Vector3.Distance(_target.position , transform.position) <= attacRange) {
                _animator.SetTrigger(animationAttackName);
            }
        }
    }
}
