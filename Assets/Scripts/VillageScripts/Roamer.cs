using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roamer : MonoBehaviour
{
    //Variables
    [SerializeField] private int damaged;
    [SerializeField] GameObject deathPartciles;
    [SerializeField] private int Health = 10;
    private NavMeshAgent navAgent;
    private Vector3 nextLocation;
    private float walkDistance = 15f;
    //constuctor
    public SpawnRoamerAI Spawn { get; set; }

    void Start()
    {
        //set current location as nextlocation to activate Roam if statement. 
        nextLocation = this.transform.position;
        navAgent = this.GetComponent<NavMeshAgent>();
        Roam();
    }

    private void Roam()
    {
        //if close choose next location
        if (navAgent.remainingDistance < 8f)
        {
            //set random location within a sphere of the walkDistance, set the y as 0 so as to remove any unreachable positions
            Vector3 random = Random.insideUnitSphere * walkDistance;
            random.y = 0f;
            nextLocation = this.transform.position + random;
            //used to keep gameObject from trying to head to points not in the NavMesh 
            if (NavMesh.SamplePosition(nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                nextLocation = hit.position;
                navAgent.SetDestination(nextLocation);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {//bullet collision checker.
        var shot = collision.collider.GetComponent<BulletFire>();
        if (shot)
        {
            damaged++;
            float count = Health - damaged;
            if (count <= 0)
            {
                if (deathPartciles != null)
                    Instantiate(deathPartciles, this.transform.position, this.transform.rotation);
                gameObject.SetActive(false);


            }
        }
    }
}