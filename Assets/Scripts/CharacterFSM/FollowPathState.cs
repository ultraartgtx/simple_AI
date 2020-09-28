using UnityEngine;
using UnityEngine.AI;
public class FollowPathState : FSMState
{
    private float _retreatRadius;
    public FollowPathState(float retreatR)
    {
        _retreatRadius = retreatR;
        stateID = StateID.FollowingPath;
    }
 
    public override void Reason(Transform player, Transform npc)
    {
        if (Vector3.Distance(player.position, npc.position) <= _retreatRadius)
        {
            player.GetComponent<characterControl>().SetTransition(Transition.SawEnemy);
        }
    }
 
    public override void Act(NavMeshAgent _agent,Vector3 position)
    {
        _agent.SetDestination(position);
    }
 
} // FollowPathState