using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTree : MonoBehaviour
{
    //Variables
    [SerializeField] private float radius = 5;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject[] startTree;
    [SerializeField] private GameObject tree;
    [SerializeField] private float spawnRate;
    [SerializeField] private int treeCount;
    [SerializeField] private int replenish;

    void Awake()
    {
        //for loop to create trees at beginning of level. 
        for (int i = 0; i < startTree.Length; i++)
        {
            Vector3 spawn = spawnPoint.transform.position;
            spawn.x += Random.Range(-radius, radius);
            spawn.z += Random.Range(-radius, radius);
            Instantiate(tree, spawn, Quaternion.identity);
            startTree[i] = tree;
            treeCount = startTree.Length;


        }


    }
    public IEnumerator treeDrop()
    {
        //checks if count is below set number, while it is, spawn an tree whithin a random radius of a set location, wait for a set time and increase count
        Debug.Log("adding a tree");
        while (treeCount < startTree.Length)
        {
            Vector3 spawn = spawnPoint.transform.position;
            spawn.x += Random.Range(-radius, radius);
            spawn.z += Random.Range(-radius, radius);
            Instantiate(tree, spawn, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
            treeCount += 1;
        }
    }
    private void Update()
    {
        //checks how many trees, if it is below then reset count and start coroutine.
        if (GameObject.FindGameObjectsWithTag("Tree").Length <= replenish)
        {
            treeCount = GameObject.FindGameObjectsWithTag("Tree").Length;
            StartCoroutine(treeDrop());
        }
    }

}




