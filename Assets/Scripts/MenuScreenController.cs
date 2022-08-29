using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreenController : MonoBehaviour
{
    public Button registerButton;
    public Button loginButton;
    public Button playButton;

    public Text playerDisplay;

    public void Start()
    {
        if( DBManager.LoggedIn)
        {
            playerDisplay.text = "Player: " + DBManager.username;
        }

        registerButton.interactable = !DBManager.LoggedIn;
        loginButton.interactable = !DBManager.LoggedIn;
        playButton.interactable = DBManager.LoggedIn;


    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToSelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void GoToProfile()
    {
        SceneManager.LoadScene("ProfileScreen");
    }

    public void LoadWorld()
    {
        SceneManager.LoadScene("World");
    }

    public void ResetHighScore()
    {
        //DBManager.score = 0;
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene("RegisterMenu");
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene("LoginMenu");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Persistent");
    }

}
