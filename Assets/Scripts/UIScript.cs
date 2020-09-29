using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text _winLoseText;
    private const string _loseString="You Lose";
    private const string _winString="You Win";
    public GameObject _winLoseWindow;
    
    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onPlayerLose()
    {
        _winLoseText.text = _loseString;
        LevelData.generation = 0;
        _winLoseWindow.SetActive(true);
    }

    public void onPlayerWin()
    {
        _winLoseText.text = _winString;
        LevelData.generation++;
        _winLoseWindow.SetActive(true);
    }
}
