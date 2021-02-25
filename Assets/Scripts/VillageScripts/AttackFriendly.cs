using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackFriendly : AIState
{
    
    //Variables
    private readonly EnemyAI enemyAI;
    private readonly Animator Animator;
    private float betweenAttacks = 3f;

    private float attackTime;

//constructor
public AttackFriendly( EnemyAI enemy, Animator animator)
{
        enemyAI = enemy;
        Animator = animator;
}
public void OnEnter()
{
        //sets animation
        Animator.SetBool("Attack 0", true);
}

public void OnExit()
{
        //stops animation
        Animator.SetBool("Attack 0", false);
    }

public void Motion()
{
        // if there is a target and attack time has passed then hit
    if (enemyAI.Target != null)
    {
            Debug.Log("got friendly target");
        if (attackTime <= Time.time)
        {
            attackTime = Time.time + (1f / betweenAttacks);
                enemyAI.Target.Hit();

               
        }
    }
}
       
    }
     
