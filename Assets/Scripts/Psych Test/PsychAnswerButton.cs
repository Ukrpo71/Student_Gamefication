using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PsychAnswerButton : MonoBehaviour
{
    public Text answerText;

    private PsychAnswerData answerData;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void Setup(PsychAnswerData data)
    {
        answerData = data;
        answerText.text = answerData.AnswerText;
    }

    public void HandleClick()
    {
        gameController.PsychAnswerButtonClicked(answerData.pointForAnswer);
    }
}
