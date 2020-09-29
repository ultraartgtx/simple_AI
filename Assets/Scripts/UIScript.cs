using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text  winLoseText;
    private const string  loseString="You Lose";
    private const string  winString="You Win";
    public GameObject  winLoseWindow;
    
    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onPlayerLose()
    {
         winLoseText.text =  loseString;
        LevelData.generation = 0;
         winLoseWindow.SetActive(true);
    }

    public void onPlayerWin()
    {
         winLoseText.text =  winString;
        LevelData.generation++;
         winLoseWindow.SetActive(true);
    }
}
