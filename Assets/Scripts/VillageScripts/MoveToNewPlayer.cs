using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToNewPlayer : AIState
{
    //same scritp used as MoveToNewFirendly, alter to follow player

    private readonly PlayerAttacker playerAttacker;
    private readonly NavMeshAgent NavMeshAgent;
    private readonly Animator Animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Vector3 lastPosition = Vector3.zero;


    public float TimeNonActive;

    public MoveToNewPlayer(PlayerAttacker player, NavMeshAgent navMeshAgent, Animator animator)
    {
        playerAttacker = player;
        NavMeshAgent = navMeshAgent;
        Animator = animator;

    }

    public void OnEnter()
    {
        TimeNonActive = 0f;
        NavMeshAgent.enabled = true;
        NavMeshAgent.SetDestination(playerAttacker.Target.transform.position);
        Animator.SetFloat(Speed, 1f);
    }

    public void OnExit()
    {
        NavMeshAgent.enabled = false;
        Animator.SetFloat(Speed, 0f);
    }

    public void Motion()
    {
        NavMeshAgent.SetDestination(playerAttacker.Target.transform.position);
        if (Vector3.Distance(a: playerAttacker.transform.position, b: lastPosition) <= 0f)
            TimeNonActive += Time.deltaTime;
        lastPosition = playerAttacker.transform.position;
      


    }
}
