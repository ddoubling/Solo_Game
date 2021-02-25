using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{

    //Same script as EnenmyAIDetection with added function to get the transfrom position of detectedEnemy if not null.
    //Used to set direction fo which to run away from

    public bool EnemyDetected => detectedEnemy != null;

    private Roamer detectedEnemy;

     void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Roamer>())
        {
            detectedEnemy = other.GetComponent<Roamer>();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Roamer>())
        {
            StartCoroutine(routine: EnemyNoLongerSighted());
        }
    }

    private IEnumerator EnemyNoLongerSighted()
    {
        yield return new WaitForSeconds(3f);
        detectedEnemy = null;
    }

    public Vector3 GetClosestEnemy()
    {
        return detectedEnemy?.transform.position ?? Vector3.zero;
    }
}
