using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public float onTime;
    public float offTime;
    private Light mylight;
    private int Number = 1;
    // Use this for initialization
    void Start()
    {
        mylight = GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Number == 1)
        {
            mylight.enabled = true;
            StartCoroutine(waitForOn());
        }
        if (Number == 2)
        {
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
