using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadSceneAsync("Level1");
            SceneManager.UnloadSceneAsync("GameOver1");
        }
    }
}
