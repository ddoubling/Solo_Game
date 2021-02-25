using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInPit : AIState
{
    //variable
    private readonly FriendlyAI friendlyAI;
    //constuctor
    public ResourceInPit(FriendlyAI friendly)
    {
        friendlyAI = friendly;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void Motion()
    {
        //if bool returns true then call add from storage pit
        if (friendlyAI.Take())
        friendlyAI.StoragePit.Add();
    }

}
