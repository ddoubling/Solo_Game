using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserOff : MonoBehaviour
{

    public GameObject laser;
    public GameObject mylight;
    bool inTrigger = false;

    // Use this for initialization
    void OnTriggerEnter(Collider plyr)
    {
        Debug.Log("inTrigger");
        if (plyr.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider plyr)
    {
        inTrigger = false;
    }

    void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                laser.SetActive(false);
                mylight.SetActive(false);
                Debug.Log("off");
            }
        }
    }
}

