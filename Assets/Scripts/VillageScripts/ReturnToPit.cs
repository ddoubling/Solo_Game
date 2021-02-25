using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnToPit : AIState
{
    //variables
    private readonly FriendlyAI friendlyAI;
    private readonly NavMeshAgent navMeshAgent;
    private readonly Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    //constructor
    public ReturnToPit(FriendlyAI friendly, NavMeshAgent nav, Animator anim)
    {
        friendlyAI = friendly;
        navMeshAgent = nav;
        animator = anim;
    }

    public void OnEnter()
    {
        //on enter state, find an object matching a storage pit and move to it. activating navMeshAgent to do so and animator with speed for animation
            friendlyAI.StoragePit = Object.FindObjectOfType<StoragePit>();
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(friendlyAI.StoragePit.transform.position);
            animator.SetFloat(Speed, 1f);
        
    }


    public void OnExit()
    {
        //on exit, disable navmeshAgent and set speed to 0 to stop animation
        navMeshAgent.enabled = false;
        animator.SetFloat(Speed, 0f);
    }

    public void Motion()
    {

    }
}
