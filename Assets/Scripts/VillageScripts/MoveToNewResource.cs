using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToNewResource : AIState
{
    // Same script as MoveToNewFriendly, if statement added to allow moving resources
    private readonly FriendlyAI FriendlyAI;
    private readonly NavMeshAgent NavMeshAgent;
    private readonly Animator Animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Vector3 lastPosition = Vector3.zero;
    public float TimeNonActive;

    public MoveToNewResource(FriendlyAI friendlyAI, NavMeshAgent navMeshAgent, Animator animator)
    {
        FriendlyAI = friendlyAI;
        NavMeshAgent = navMeshAgent;
        Animator = animator;

    }

    public void OnEnter()
    {
        TimeNonActive = 0f;
        NavMeshAgent.enabled = true;
        NavMeshAgent.SetDestination(FriendlyAI.Target.transform.position);
        Animator.SetFloat(Speed, 1f);
    }

    public void OnExit()
    {
        NavMeshAgent.enabled = false;
        Animator.SetFloat(Speed, 0f);
    }

    public void Motion()
    {
        if (Vector3.Distance(a: FriendlyAI.transform.position, b: lastPosition) <= 0f)
            TimeNonActive += Time.deltaTime;
        lastPosition = FriendlyAI.transform.position;
        //precaution in case of moving resources
        if (NavMeshAgent.destination != FriendlyAI.Target.transform.position)
            NavMeshAgent.SetDestination(FriendlyAI.Target.transform.position);
    }
}
