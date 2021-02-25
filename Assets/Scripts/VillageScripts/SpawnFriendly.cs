using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFriendly : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject friendly;
    [SerializeField] private int friendlyCount;
    private float radius = 5;


    void Awake()
    {

        for (int i = 0; i < friendlyCount; i++)
        {
            Vector3 spawn = spawnPoint.transform.position;
            spawn.x += Random.Range(-radius, radius);
            spawn.z += Random.Range(-radius, radius);
            Instantiate(friendly, spawn, Quaternion.identity);
        }

    }
}
