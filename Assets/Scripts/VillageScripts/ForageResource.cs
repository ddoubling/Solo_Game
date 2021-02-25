using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForageResource : AIState
{
    //variables
    private readonly FriendlyAI FriendlyAI;
    private readonly Animator Animator;
    private float resourceTime = 3f;
    private float resourceCollectTime;

    //constructor
    public ForageResource(FriendlyAI friendly, Animator animator)
    {
        FriendlyAI = friendly;
        Animator = animator;
    }
    public void OnEnter()
    {

    }
    
    public void OnExit()
    {

    }

    public void Motion()
    {
        // check that there is target and then collects if time has passed
        //animator was set in motion as was a trigger and it gave a better fluidity of the animations
        if (FriendlyAI.Target != null)
        {
            Debug.Log("foraging");
                if (resourceCollectTime <= Time.time)
                {
                    resourceCollectTime = Time.time + (1f/resourceTime);
                    FriendlyAI.TakeFromTarget();
                    Animator.SetTrigger("Forage");
                }
            }
        }
       
    }
     


