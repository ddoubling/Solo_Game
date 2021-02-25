using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToNewFriendly : AIState
{

    //variables
    private readonly EnemyAI enemyAI;
    private readonly NavMeshAgent NavMeshAgent;
    private readonly Animator Animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Vector3 lastPosition = Vector3.zero;
    public float TimeNonActive;

    //constuctor
    public MoveToNewFriendly(EnemyAI enemy, NavMeshAgent navMeshAgent, Animator animator)
    {
        enemyAI = enemy;
        NavMeshAgent = navMeshAgent;
        Animator = animator;
        
    }

    public void OnEnter()
    {
        //on enter set nonactive time to 0, enable the navMeshAgent, set its destination to a friendlyAI, set animation float to 1f
        TimeNonActive = 0f;
        NavMeshAgent.enabled = true;
        NavMeshAgent.SetDestination(enemyAI.Target.transform.position);
        Animator.SetFloat(Speed, 1f);
    }

    public void OnExit()
    {
        //On exit, disable navMeshAgent and set float to 0;
        NavMeshAgent.enabled = false;
        Animator.SetFloat(Speed, 0f);
    }

    public void Motion()
    {
        //on motuion call set destination, allows tracking of moving targets. If current position is same as last then increase counter. set last position as current
        NavMeshAgent.SetDestination(enemyAI.Target.transform.position);
        if (Vector3.Distance(a: enemyAI.transform.position, b: lastPosition) <= 0f)
            TimeNonActive += Time.deltaTime;
        lastPosition = enemyAI.transform.position;
        
        
    }
}
