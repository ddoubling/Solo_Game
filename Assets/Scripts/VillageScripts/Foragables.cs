using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foragables : MonoBehaviour
{
    //Variables
    [SerializeField] private int totalAvailable = 20;
    [SerializeField] private int available;

    //bool used when there are no resources available, used to move FriendlyAI to new tree after delpleting a tree and can carry more
    public bool depletedResource => available <= 0;
    private void OnEnable()
    {
        //when tree active, set availability of recources
        available = totalAvailable;
    }

    public bool Take()
    {
        // when called, check availability, reduce availability and if now no more available, set gameObject to false
        if (available <= 0)
        {
            Debug.Log(depletedResource);
            return false;
        }
        available--;
        if (totalAvailable - available == totalAvailable)
        {
            gameObject.SetActive(false);
        }

        return true;
        
    }


    }

