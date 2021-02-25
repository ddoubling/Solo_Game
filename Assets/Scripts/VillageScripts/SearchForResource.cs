using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchForResource : AIState
{
    private readonly FriendlyAI FriendlyAI;

    public SearchForResource(FriendlyAI friendlyAI)
    {
        FriendlyAI = friendlyAI;
    }
    public void Motion()
    {
        FriendlyAI.Target = ChooseNearestResource(3);
    }

    private Foragables ChooseNearestResource(int chooseNearest)
    {
        //looks for nearest 3 trees and chooses nearest.
        Debug.Log("searching");
        return Object.FindObjectsOfType<Foragables>()
            .OrderBy(resource => Vector3.Distance(FriendlyAI.transform.position, resource.transform.position))
            .Where(resource => resource.depletedResource == false)
            .Take(chooseNearest)
            .OrderBy(resource => Random.Range(0, int.MaxValue))
            .FirstOrDefault();

    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }
}

