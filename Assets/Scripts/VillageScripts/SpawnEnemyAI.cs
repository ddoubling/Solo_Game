using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyAI : MonoBehaviour
{
    //variables
    public GameObject spawnPoint;
    public GameObject enemy;
    public int enemyCount;
    public int min;
    public int spawnRate;
    public int spawnNumber;
    private float radius = 5;

    public IEnumerator enemyDrop()
    {
        //checks if count is below set number, while it is, spawn an enemy whithin a random radius of a set location, wait for a set time and increase count
        while (enemyCount < spawnNumber)
        {
            Vector3 spawn = spawnPoint.transform.position;
            spawn.x += Random.Range(-radius, radius);
            spawn.z += Random.Range(-radius, radius);
            Instantiate(enemy, spawn, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
            enemyCount++;
        }
    }

    void Awake()
    {
        //starts Coroutine at wake of game;
        StartCoroutine(enemyDrop());
    }
    private void Update()
    {
        //checks how many enemies are alive, if it is below then reset count and start coroutine.
        if (GameObject.FindGameObjectsWithTag("enemy").Length <= min)
        {
            enemyCount = 0;
            StartCoroutine(enemyDrop());
        }
    }

}
