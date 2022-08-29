using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    public static string username;
    public static int score1;
    public static int score2;
    public static int score3;
    public static int score4;

    public static bool LoggedIn{get { return username != null; } }

    public static void LogOut()
    {
        username = null;
    }
}
