using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviours : MonoBehaviour
{
    GameObject[] AIHolder;
    public float gap = 1.5f;

    private void Awake()
    {
        AIHolder = GameObject.FindGameObjectsWithTag("AI");
    }
    private void Update()
    {
        AIHolder = GameObject.FindGameObjectsWithTag("AI");
        foreach (GameObject ai in AIHolder)
            if (ai != gameObject)
            {
                float distance = Vector3.Distance(ai.transform.position, this.transform.position);
                if (distance <= gap)
                {
                    Vector3 direction = transform.position - ai.transform.position;
                    transform.Translate(direction * Time.deltaTime);

                }
            }
    }
}
            

