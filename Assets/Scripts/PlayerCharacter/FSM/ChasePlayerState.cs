using UnityEngine;
using UnityEngine.AI;
 
public class ChasePlayerState : FSMState
{
    public float retreatRadius;

    public ChasePlayerState(float retreatR)
    {
        retreatRadius = retreatR;
        stateID = StateID.ChasingEnemy;
    }

    Vector3 toPlayer =Vector3.zero;
  
    
    public override void Reason(Transform player, Transform npc)
    {
        toPlayer = player.position - npc.position;
        toPlayer = toPlayer.normalized * retreatRadius;
        // If the player has gone 30 meters away from the NPC, fire LostPlayer transition
        if (Vector3.Distance(npc.position, player.position) > retreatRadius)
        {
            player.GetComponent<PlayerCharacterControl>().SetTransition(Transition.LostEnemy);
        }
            
    }
    public override void Act(NavMeshAgent _agent,Vector3 position)
    {
        
        _agent.SetDestination(toPlayer);
        
       
    }
 
} // ChasePlayerState