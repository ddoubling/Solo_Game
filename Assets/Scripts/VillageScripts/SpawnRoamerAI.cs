using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoamerAI : MonoBehaviour
{

    // Repeated code for ease of usability and separation
    public GameObject spawnPoint;

    public GameObject enemy;
    public int enemyCount;
    public int min;
    public int spawnRate;
    public int spawnNumber;
    private float radius = 5;

    public IEnumerator enemyDrop()
    {
        Debug.Log("adding a tree");
        while (enemyCount < spawnNumber)
        {
            Vector3 spawn = spawnPoint.transform.position;
            spawn.x += Random.Range(-radius, radius);
            spawn.z += Random.Range(-radius, radius);
            Instantiate(enemy, spawn, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
            enemyCount += 1;
        }
    }

    void Awake()
    {
        StartCoroutine(enemyDrop());
    }
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length <= min)
        {
            enemyCount = 0;
            StartCoroutine(enemyDrop());
        }
    }
}
