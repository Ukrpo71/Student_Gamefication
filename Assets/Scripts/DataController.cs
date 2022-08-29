using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour
{
    private RoundData[] allRoundData;
    private PsychRoundData[] allPsychRoundData = new PsychRoundData[1];
    //отслеживание прогресса
    public bool[] finishedLevels;
    public string typeOfPerson;
    public bool[] clearedHardSkills;
    public bool[] clearedSoftSkills;

    //показ сюжета
    public int chapterNumber;
    public bool alreadyShowedChapter;

    public string levelSelected;
    private PlayerProgress playerProgress;
    public string gameDataFileName = "data.json";
    public string dataAsJson = "";
    public bool isNormal;
    public ResultData[] resultData;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("SelectLevel");
        finishedLevels = new bool[4];
        for (int i = 0; i < 3; i++)
            finishedLevels[i] = false;
        //finishedLevels[0] = true;

        clearedHardSkills = new bool[3];
        clearedSoftSkills = new bool[2];
    }

    public RoundData GetCurrentRoundData()
    {
        return allRoundData[0];
    }

    public PsychRoundData GetCurrentPsychRoundData()
    {
        return allPsychRoundData[0];
    }

    public void SubmitNewPlayerScore(int newScore)
    {

        print(DBManager.score1);
        print(newScore);
        print(levelSelected);
        switch (int.Parse(levelSelected))
        {
            case 1:
                if (newScore > DBManager.score1)
                {
                    DBManager.score1 = newScore;
                    SavePlayerProgress();
                    print("New Score bigger than score 1 game saved");
                }
                break;

            case 2:
                if (newScore > DBManager.score2)
                {
                    DBManager.score2 = newScore;
                    SavePlayerProgress();
                }
                break;

            case 3:
                if (newScore > DBManager.score3)
                {
                    DBManager.score3 = newScore;
                    SavePlayerProgress();
                }
                break;

            case 4:
                if (newScore > DBManager.score4)
                {
                    DBManager.score4 = newScore;
                    SavePlayerProgress();
                }
                break;
        }

        
    }

    public void LoadAllData(string selectedLevel)
    {
        if (int.Parse(selectedLevel) != 4)
            isNormal = true;
        else
            isNormal = false;

        gameDataFileName = "data" + selectedLevel + ".json";
        levelSelected = selectedLevel;
        LoadGameData();

    }

    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    private void SavePlayerProgress()
    {
        //PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
        //DBManager.score = playerProgress.highestScore;
        StartCoroutine(SaveScore());
       
    }

    public void updateClearedData(string clearedLevel)
    {
        switch (clearedLevel)
        {
            case "1":
                clearedHardSkills[0] = true;
                finishedLevels[1] = true;
                break;
            case "2":
                clearedHardSkills[1] = true;
                finishedLevels[2] = true;
                break;
            case "3":
                clearedHardSkills[2] = true;
                break;
            case "4":
                clearedSoftSkills[0] = true;
                finishedLevels[0] = true;
                break;
        }

    }


    private void LoadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        IEnumerator LoadJsonFromWWW()
        {
            if (filePath.Contains("://") || filePath.Contains(":///"))
            {
                WWW www = new WWW(filePath);
                yield return www;

                dataAsJson = www.text;
                if (isNormal)
                {
                    GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
                    allRoundData = loadedData.allRoundData;
                }
                else
                {
                    PsychRoundData loadedData = JsonUtility.FromJson<PsychRoundData>(dataAsJson);
                    allPsychRoundData[0] = loadedData;
                }
                Debug.LogError("Data Loaded from web");
                print("Data Loaded from web");
            }
            else
            {
                dataAsJson = System.IO.File.ReadAllText(filePath);
                if (isNormal)
                {
                    GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
                    allRoundData = loadedData.allRoundData;
                }
                else
                {
                    PsychRoundData loadedData = JsonUtility.FromJson<PsychRoundData>(dataAsJson);
                    allPsychRoundData[0] = loadedData;
                }
                Debug.LogError("Data loaded from file");
                print("Data Loaded from file");
            }

        }

        IEnumerator WaitTillDataLoads()
        {
            yield return StartCoroutine(LoadJsonFromWWW());
            LoadPlayerProgress();

            SceneManager.LoadScene("Game");
        }

        StartCoroutine(WaitTillDataLoads());
    }

    

    IEnumerator SaveScore()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score1", DBManager.score1);
        form.AddField("score2", DBManager.score2);
        form.AddField("score3", DBManager.score3);
        form.AddField("score4", DBManager.score4);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;

        if (www.text == "0")
        {
            Debug.Log("Game Saved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }
    }
}
