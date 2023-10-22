using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

        public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

        public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

        public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

        public void LoadTheMeat()
    {
        SceneManager.LoadScene("TheMeat");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the Game...");
        Application.Quit();
    }
}
