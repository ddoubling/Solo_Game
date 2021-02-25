using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackPlayer : AIState
{
    //Variables
    private readonly PlayerAttacker attackPlayer;
    private readonly Animator Animator;
    private float betweenAttacks = 3f;
    private float attackTime;

    //constructor
    public AttackPlayer(PlayerAttacker player, Animator animator)
    {
        attackPlayer = player;
        Animator = animator;
    }
    public void OnEnter()
    {
        //set animation on enter
        Animator.SetBool("Attack 0", true);
    }

    public void OnExit()
    {
        //set animation on exit
        Animator.SetBool("Attack 0", false);
    }

    public void Motion()
    {
        //check if there is a target then if time has passed, hit.
        if (attackPlayer.Target != null)
        {
            Debug.Log("got friendly target");
            //Debug.Log("about to attack");
            if (attackTime <= Time.time)
            {
                attackTime = Time.time + (1f / betweenAttacks);
                attackPlayer.Target.Hit();
                //Animator.SetTrigger(Attack); 

            }
        }
    }
}
