﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            DBManager.username = nameField.text;
            DBManager.score1 = int.Parse(www.text.Split('\t')[1]);
            //DBManager.score2 = int.Parse(www.text.Split('\t')[2]);
            //DBManager.score3 = int.Parse(www.text.Split('\t')[3]);
            //DBManager.score4 = int.Parse(www.text.Split('\t')[4]);
            SceneManager.LoadScene("Menu");
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }

    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}
