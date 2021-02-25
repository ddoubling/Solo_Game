using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RunAway : AIState
{
    //variables
    private readonly FriendlyAI friendlyAI;
    private readonly NavMeshAgent navMeshAgent;
    private readonly EnemyDetection enemyDetection;
    private readonly Animator animator;
    private static readonly int Running = Animator.StringToHash("RunAway");
    private float initialSpeed;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float runDistance = 6f;

    //constructor
    public RunAway(FriendlyAI friendly, NavMeshAgent nav, EnemyDetection detection, Animator anim)
    {
        friendlyAI = friendly;
        navMeshAgent = nav;
        enemyDetection = detection;
        animator = anim;

    }

    public void OnEnter()
    {
        //enable navmesh, set animation to true and set initial speed to return to and then set the run speed
        navMeshAgent.enabled = true;
        animator.SetBool(Running, true);
        initialSpeed = navMeshAgent.speed;

        navMeshAgent.speed = runSpeed;

    }

    public void OnExit()
    {
        //return to initial speed, disbale navmesh and set running bool to false to stop run animation
        navMeshAgent.speed = initialSpeed;
        navMeshAgent.enabled = false;
        animator.SetBool(Running, false);
    }

    public void Motion()
    {
        //used to keep gameobject moving to new random location should it come close to the first
        if (navMeshAgent.remainingDistance < 1f)
        {
            var away = GetRandomPoint();
            navMeshAgent.SetDestination(away);
        }
    }
    private Vector3 GetRandomPoint()
    {
        //Get enemy location from fiendly and normalise
        var enemyDirection = friendlyAI.transform.position - enemyDetection.GetClosestEnemy();
        enemyDirection.Normalize();
        //create Vector 3 to run to from where enemy was detected * runDistance
        var direction = friendlyAI.transform.position + (enemyDirection * runDistance);
        //used to keep gameobject from trying to acces non-accessible locations
        if(NavMesh.SamplePosition(direction, out var hit, 5f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return friendlyAI.transform.position;
    }
}
