using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Text timeRemainingDisplayText;
    public SimpleObjectPool answerButtonsObjectPool;
    public SimpleObjectPool psychAnswerButtonsObjectPool;
    public Transform answerButtonParent;

    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    public GameObject instructionsDisplay;
    public Text highScoreDisplay;

    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private PsychRoundData currentPsychRoundData;
    private PsychQuestionData[] psychQuestionPool;

    private bool isRoundActive;
    private int questionIndex;
    private int playerScore;
    private int[] playerPsychScore = new int[5];
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        if (dataController.isNormal)
        {
            currentRoundData = dataController.GetCurrentRoundData();
            questionPool = currentRoundData.questions;
            UpdateTimeRemainingDisplay();
        }
        else
        {
            currentPsychRoundData = dataController.GetCurrentPsychRoundData();
            psychQuestionPool = currentPsychRoundData.questions;
        }

        playerScore = 0;
        questionIndex = 0;

        if (!dataController.isNormal)
        {
            instructionsDisplay.SetActive(true);
            questionDisplay.SetActive(false);
            scoreDisplayText.gameObject.SetActive(false);
            timeRemainingDisplayText.gameObject.SetActive(false);
        }
        else
        {
            ShowQuestions();
        }

        isRoundActive = true;
    }

    private void RemoveAnswersButtons()
    {
        while ( answerButtonGameObjects.Count > 0)
        {
            if (dataController.isNormal)
                answerButtonsObjectPool.ReturnObject(answerButtonGameObjects[0]);
            else
                psychAnswerButtonsObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    private void ShowQuestions()
    {
        RemoveAnswersButtons();
        if (dataController.isNormal)
        {
            QuestionData questionData = questionPool[questionIndex];
            questionDisplayText.text = questionData.questionText;

            for (int i = 0; i < questionData.answers.Length; i++)
            {
                GameObject answerButtonGameObject = answerButtonsObjectPool.GetObject();
                answerButtonGameObject.transform.SetParent(answerButtonParent);
                answerButtonGameObjects.Add(answerButtonGameObject);

                AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
                answerButton.Setup(questionData.answers[i]);
            }
        }
        else
        {
            PsychQuestionData questionData = psychQuestionPool[questionIndex];
            questionDisplayText.text = questionData.questionText;

            for (int i = 0; i < questionData.answers.Length; i++)
            {
                GameObject answerButtonGameObject = psychAnswerButtonsObjectPool.GetObject();
                answerButtonGameObject.transform.SetParent(answerButtonParent);
                answerButtonGameObjects.Add(answerButtonGameObject);

                PsychAnswerButton answerButton = answerButtonGameObject.GetComponent<PsychAnswerButton>();
                answerButton.Setup(questionData.answers[i]);
            }
        }
    }

    public void AnswerButtonClicked(int pointsForAnswer)
    {
                playerScore += pointsForAnswer;
                scoreDisplayText.text = "Счет: " + playerScore.ToString();

            if (questionPool.Length > questionIndex + 1)
            {
                questionIndex++;
                ShowQuestions();
            }
            else
            {
                EndRound();
            }
    }

    public void PsychAnswerButtonClicked(int whereToAddAPoint)
    {
        switch (whereToAddAPoint)
        {
            case 1:
                playerPsychScore[0]++;
                break;
            case 2:
                playerPsychScore[1]++;
                break;
            case 3:
                playerPsychScore[2]++;
                break;
            case 4:
                playerPsychScore[3]++;
                break;
            case 5:
                playerPsychScore[4]++;
                break;
        }
        /*scoreText.text = "";
        for (int i = 0; i < playerScore.Length; i++)
            scoreText.text += "Score" + i + ": " + playerScore[i].ToString() + "\n";
        */

        if (psychQuestionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestions();
        }
        else
        {
            EndRound();
        }
    }

    public void EndRound()
    {
        isRoundActive = false;
        dataController.SubmitNewPlayerScore(playerScore);
        if (dataController.isNormal)
            //highScoreDisplay.text = DBManager.username + ", ваш результат: " + playerScore + " правильных ответов";
            highScoreDisplay.text = "Ваш результат: " + playerScore + " правильных ответов";
        else
        {
            int index = ReturnBiggestResult();
            highScoreDisplay.text = "Ваш результат - " + dataController.resultData[index].definition + "\n"
                + dataController.resultData[index].description;
        }

        if (dataController.isNormal && (int.Parse(currentRoundData.name) < 3) && dataController.finishedLevels[int.Parse(currentRoundData.name)] == false)
        {
            dataController.chapterNumber++;
            dataController.alreadyShowedChapter = false;
            dataController.updateClearedData(currentRoundData.name);
        }
        else if (!dataController.isNormal && dataController.finishedLevels[int.Parse(currentPsychRoundData.name) - 4] == false)
        {
            dataController.chapterNumber++;
            dataController.alreadyShowedChapter = false;
            dataController.updateClearedData(currentPsychRoundData.name);
        }
        else
        {
            dataController.updateClearedData(currentRoundData.name);
            dataController.chapterNumber++;
            dataController.alreadyShowedChapter = false;
        }
        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);

        
    }


    public void ReturnToMenu()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void ReturnToSkills()
    {
        SceneManager.LoadScene("SelectLevel");
    }


    private void UpdateTimeRemainingDisplay()
    {
        
    }

    private void CalculateResults()
    {
        int index = ReturnBiggestResult();
        highScoreDisplay.text = "Ваш результат - " + dataController.resultData[index].definition + "\n"
            + dataController.resultData[index].description;
    }

    private int ReturnBiggestResult()
    {
        int maxIndex = 0;
        for (int i = 1; i < playerPsychScore.Length; i++)
        {
            if (playerPsychScore[i] > playerPsychScore[i - 1])
                maxIndex = i;
        }

        return maxIndex;
    }

    public void StartPsychTest()
    {
        instructionsDisplay.SetActive(false);
        questionDisplay.SetActive(true);
        ShowQuestions();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
