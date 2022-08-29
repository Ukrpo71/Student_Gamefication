using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileScript : MonoBehaviour
{
    private int score1_max = 14;
    private int score2_max = 9;
    private int score3_max = 10;

    [SerializeField]
    private Text[] scores;

    public Image score1Mask;
    public Image score2Mask;
    public Image score3Mask;

    public Text playerName;


    // Start is called before the first frame update
    void Start()
    {
        FillBars();
        playerName.text = DBManager.username;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillBars()
    {
        float score1 = (float)DBManager.score1 / (float)score1_max;
        float score2 = (float)DBManager.score2 / (float)score2_max;
        float score3 = (float)DBManager.score3 / (float)score3_max;

        score1Mask.transform.localScale = new Vector3(score1, 1, 1);
        score2Mask.transform.localScale = new Vector3(score2, 1, 1);
        score3Mask.transform.localScale = new Vector3(score3, 1, 1);

        scores[0].text = Mathf.Round(score1*100) + "%";
        scores[1].text = Mathf.Round(score2*100) + "%";
        scores[2].text = Mathf.Round(score3*100) + "%";
    }

    public void GoBack()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    
}
