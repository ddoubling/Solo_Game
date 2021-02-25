using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private Transform[] waypoints;
    private NavMeshAgent nav;
    private int Locals = 0;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if( nav.remainingDistance < 1f)
        {
            var nextLocal = GetNextLocal();
            nav.SetDestination(nextLocal);
        }
    }

    private Vector3 GetNextLocal()
    {
        Locals++;
        if (Locals >= waypoints.Length)
            Locals = 0;
        return waypoints[Locals].position;
    }
}
