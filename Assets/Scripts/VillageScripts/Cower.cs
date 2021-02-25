using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cower : AIState
{
    //Variables
    private readonly Animator Animator;
    private static readonly int cower = Animator.StringToHash("Attack");

    //Constructor
    public Cower( Animator animator)
    {
        //friendlyAI = friendly;
        Animator = animator;
    }

    
    public void OnEnter()
    {
        //sets animation on enter
    Animator.SetBool(cower, true);
    }

    public void OnExit()
    {
        //sets animation on exit
        Animator.SetBool(cower, false);
    }

    public void Motion()
    {
    
    }
    

}
