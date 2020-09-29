using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))] 
[RequireComponent (typeof (CharacterTakeDamege))] 
[RequireComponent (typeof (CharacterParameters))] 
[RequireComponent(typeof(CharacterCalculateDamage))]
public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isGameOver = false;
    private SpawnEnemys _spawnEnemys;
    private NavMeshAgent _agent;
    private GameObject _player;
    private Transform _target;
    public Animator _animator;
    private AnimationEvent _AnimationEvent;
    private CharacterTakeDamege _characterTakeDamegeSctipt;
    private CharacterParameters _enemyParms;
    private CharacterCalculateDamage _characterCalculateDamage;


    public void GameOver()
    {
        isGameOver = true;
        _agent.Stop();
    }

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
        _player.GetComponent<CharacterTakeDamege>().TakeDamege(_characterCalculateDamage.calculateDamage(_enemyParms.attack));
        print("GiveDamage.enemy");
    }

    void Start()
    {
        _characterCalculateDamage = GetComponent<CharacterCalculateDamage>();
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
        if (!isGameOver)
        {
            _agent.SetDestination(_target.position);
            if (Vector3.Distance(_target.position , transform.position) <= _enemyParms.attacRange) {
                _animator.SetTrigger("Attack1Trigger");
            }
        }
    }
}
