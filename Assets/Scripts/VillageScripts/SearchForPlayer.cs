using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchForPlayer : AIState
{
    private readonly PlayerAttacker playerAttacker;

    public SearchForPlayer(PlayerAttacker player)
    {
        playerAttacker = player;
    }
    public void Motion()
    {
        playerAttacker.Target = ChooseNearestPlayer(10);
    }

    private Player ChooseNearestPlayer(int chooseNearest)
    {
        //Same process as search for friendly. Kept as considered it may allow for multiplayer integration
        Debug.Log("searching");
        return Object.FindObjectsOfType<Player>()
            .OrderBy(player => Vector3.Distance(playerAttacker.transform.position, player.transform.position))
            .Where(player => player.NoHealth == false)
            .Take(chooseNearest)
            .OrderBy(player => Random.Range(0, int.MaxValue))
            .FirstOrDefault();

    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }
}
