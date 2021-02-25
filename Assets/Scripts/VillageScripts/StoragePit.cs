using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoragePit : MonoBehaviour
{
    [SerializeField] private float ResourceGoal = 2000;
    [SerializeField] private  float TotalForaged;
    public Text scoreText;
    public Text goalText;




    private void OnEnable()
    {
        // Holds total Foraged
        TotalForaged = 0;
        goalText.text = ResourceGoal.ToString();

    }
    public void Add()
    {
       //increases total foraged from called
        TotalForaged++;
        scoreText.text = TotalForaged.ToString();
        if (TotalForaged == ResourceGoal)
            FindObjectOfType<GameManager>().LevelUp();

    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
}
