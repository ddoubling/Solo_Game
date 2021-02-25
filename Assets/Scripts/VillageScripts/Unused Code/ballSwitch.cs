using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public Color gold;
    //public Color yellow;
    public Color grey;
    public Color red;
    public Color green;
    public MeshRenderer render;
    public GameObject door;
    public MeshRenderer doorLight;
    public Light myLight;


    bool inTrigger = false;

    // Use this for initialization
    void OnTriggerEnter(Collider plyr)
    {
        Debug.Log("inTrigger");
        if (plyr.tag == "ball")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider plyr)
    {
        if (plyr.tag == "ball")
        {
            inTrigger = false;
            render.GetComponent<Renderer>().material.color = grey;
            doorLight.GetComponent<Renderer>().material.color = red;
            myLight.GetComponent<Light>().color = red;
            door.SetActive(true);
        }
    }

    void Update()
    {
        if (inTrigger)
        {
            render.GetComponent<Renderer>().material.color = gold;
            doorLight.GetComponent<Renderer>().material.color = green;
            myLight.GetComponent<Light>().color = green;
            door.SetActive(false);
        }
    }
}
