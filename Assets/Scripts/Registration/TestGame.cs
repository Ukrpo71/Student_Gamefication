using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGame : MonoBehaviour
{
    public Text playerDisplay;
    public Text scoreDisplay;

    private void Awake()
    {
        playerDisplay.text = "Player: " + DBManager.username;
        scoreDisplay.text = "Score: " + DBManager.score1;
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score1", DBManager.score1);
        form.AddField("score2", DBManager.score2);
        form.AddField("score3", DBManager.score3);
        form.AddField("score4", DBManager.score4);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php",form);
        yield return www;

        if (www.text == "0")
        {
            Debug.Log("Game Saved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }

        DBManager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void IncreaseScore()
    {
        DBManager.score1++;
        scoreDisplay.text = "Score: " + DBManager.score1;
    }
}
