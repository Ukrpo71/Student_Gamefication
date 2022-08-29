using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] chapterPanel;
    [SerializeField]
    private Animator[] chapterPanelAnimator;
    private DataController dataController;

    public Animator newChapterButtonAnimator;

    public int numberClicked;
    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        numberClicked = 0;
        if (dataController.alreadyShowedChapter == false)
        {
            StartCoroutine(StartButton());
        }
        
    }

    public void ChapterClicked(int chapter)
    {
        if (numberClicked == 0)
            StartCoroutine(ChangeText(chapter));
        else
            StartCoroutine(SlideUp(chapter));
    }

    public void ShowChapter()
    {
        if (dataController.alreadyShowedChapter == false)
        {
            StartCoroutine(ButtonDown());
            StartCoroutine(SlideDown(dataController.chapterNumber));
        }
    }

    IEnumerator SlideDown(int chapter)
    {
        chapterPanelAnimator[chapter].Play("SlideDown");
        yield return new WaitForSeconds(1.0f);;
    }

    IEnumerator StartButton()
    {
        newChapterButtonAnimator.Play("SlideUp");
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator ButtonDown()
    {
        newChapterButtonAnimator.Play("SlideDown");
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator SlideUp(int chapter)
    {
        chapterPanelAnimator[chapter].Play("SlideUp");
        yield return new WaitForSeconds(1.0f);
        numberClicked = 0;
        dataController.alreadyShowedChapter = true;
    }

    IEnumerator ChangeText(int chapter)
    {
        chapterPanelAnimator[chapter].Play("ChangeText");
        yield return new WaitForSeconds(1.0f);
        numberClicked++;
        Debug.Log(numberClicked);
    }
    
}
