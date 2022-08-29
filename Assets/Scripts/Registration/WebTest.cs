using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebTest : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        WWW request = new WWW("http://localhost/sqlconnect/webtest.php");
        yield return request;
        string[] requestSplit = request.text.Split('\t');
        foreach (string s in requestSplit)
        {
            Debug.Log(s);
        }

        int number = int.Parse(requestSplit[1]);
        number *= 2;
        Debug.Log(number);
    }

}
