using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchForFriendly : AIState
{
    //variable
    private readonly EnemyAI enemyAI;
    //contructor
    public SearchForFriendly(EnemyAI enemy)
    {
        enemyAI = enemy;
    }

    public void Motion()
    {
        //sets a target from number of targets within int
        enemyAI.Target = ChooseNearestFriendly(5);
    }

    private FriendlyAI ChooseNearestFriendly(int chooseNearest)
    {
        //Ineumerable extension used for selection process, creates an order, checks if it has health, then checks int of them by order by range
        Debug.Log("searching");
        return Object.FindObjectsOfType<FriendlyAI>()
            .OrderBy(friendly => Vector3.Distance(enemyAI.transform.position, friendly.transform.position))
            .Where(friendly => friendly.NoHealth == false)
            .Take(chooseNearest)
            .OrderBy(friendly=> Random.Range(0, int.MaxValue))
            .FirstOrDefault();

    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }
}
