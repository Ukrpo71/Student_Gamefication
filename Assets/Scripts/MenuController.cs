using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject softSkills, hardSkills;
    public Animator softSkillsAnimator, hardSkillsAnimator, infoTextAnimator;
    public Text infoText;

    IEnumerator NextMenu()
    {
        softSkillsAnimator.Play("Slide Right");
        hardSkillsAnimator.Play("Slide Right");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator PreviousMenu()
    {
        softSkillsAnimator.Play("Slide Left");
        hardSkillsAnimator.Play("Slide Left");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator SlideDown(int value)
    {
        infoTextAnimator.Play("slide down");
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator SlideUp(int value)
    {
        yield return StartCoroutine(SlideDown(value));
        switch (value)
        {
            case 0:
                infoText.text = "Оцените свои компетенции в разных сферах ИТ. Узнайте в чем вы продвинутее всего.";
                break;
            case 1:
                infoText.text = "Данный этап является отличным способом оценки своих способностей, которые отвечают  за успешное участие в рабочем процессе.";
                break;
        }
        infoTextAnimator.Play("slide up");
        yield return new WaitForSeconds(0.5f);
    }

    public void GoToNext()
    {
        StartCoroutine(NextMenu());
        ExchangeTexts(0);
    }

    public void GoToPrevious()
    {
        StartCoroutine(PreviousMenu());
        ExchangeTexts(1);
    }

    public void GoToSoftSkills()
    {
        SceneManager.LoadScene("SoftSkills");
    }

    public void GoToHardSkills()
    {
        SceneManager.LoadScene("HardSkills");
    }

    public void GoToProfile()
    {
        SceneManager.LoadScene("ProfileScreen");
    }

    public void ExchangeTexts(int value)
    {
        switch (value)
        {
            case 0:
                StartCoroutine(SlideUp(0));
                break;
            case 1:
                StartCoroutine(SlideUp(1));
                break;
        }
        
    }
}
