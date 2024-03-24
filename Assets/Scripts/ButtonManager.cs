using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public string nextLevelScene;
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1.0f;
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelScene);
        Time.timeScale = 1.0f;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
