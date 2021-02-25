using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
    bool win = false;
    public void EndGame()
    {
        Debug.Log("gameOver1");
        if (gameOver == false)
        {
            gameOver = true;
            Restart();
        }
    }
    void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("GameOver1");
    }

    public void LevelUp()
    {
        if (win == false)
        {
            win = true;
            WinScene();
        }
    }

    void WinScene()
    {
        SceneManager.LoadScene("LevelUp");
    }

}
