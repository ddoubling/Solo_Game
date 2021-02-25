using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public float onTime;
    public float offTime;
    private MeshRenderer laser;
    private Light mylight;
    private BoxCollider alarm;
    private int Number = 1;
    // Use this for initialization
    void Start()
    {
        alarm = GetComponent<BoxCollider>();
        laser = GetComponent<MeshRenderer>();
        mylight = GetComponent<Light>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Number == 1)
        {
            alarm.enabled = true;
            laser.enabled = true;
            mylight.enabled = true;
            StartCoroutine(waitForOn());
        }
        if (Number == 2)
        {
            alarm.enabled = false;
            laser.enabled = false;
            mylight.enabled = false;
            StartCoroutine(waitForOff());
        }
    }
    IEnumerator waitForOn()
    {
        yield return new WaitForSeconds(onTime);
        Number = 2;
    }
    IEnumerator waitForOff()
    {
        yield return new WaitForSeconds(offTime);
        Number = 1;
    }
}
