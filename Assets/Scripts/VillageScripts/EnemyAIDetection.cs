using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIDetection : MonoBehaviour
{
    //variable bool if enemy was detected
    public bool EnemyDetected => detectedEnemy != null;
    private EnemyAI detectedEnemy;

    
    void OnTriggerEnter(Collider other)
    {
        //on trigger enter and component was an EnemyAI then set as detectedEnemy
        Debug.Log("enemy");
        if (other.GetComponent<EnemyAI>())
        {
            detectedEnemy = other.GetComponent<EnemyAI>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //on trigger exit and component was an EnemyAI then call Coroutine to set detectedEnemy to null
        if (other.GetComponent<EnemyAI>())
        {
            StartCoroutine(routine: EnemyNoLongerSighted());
        }
    }

    private IEnumerator EnemyNoLongerSighted()
    {
        //after called wait 3 seconds then set to null
        yield return new WaitForSeconds(3f);
        detectedEnemy = null;
    }
}
