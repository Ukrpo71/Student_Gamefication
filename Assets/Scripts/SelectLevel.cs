using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class SelectLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject level1, level2, stage1, stage2;

    [SerializeField]
    private Animator level1Anim, level2Anim, stage1Anim, stage2Anim;

    DataController dataController;
    private void Start()
    {
        dataController = GameObject.Find("DataController").GetComponent<DataController>();
    }
    public void GoToGame()
    {
        string selected = EventSystem.current.currentSelectedGameObject.name;
        dataController.LoadAllData(selected);
    }

    IEnumerator ShowLevel1()
    {
        level1.SetActive(true);
        level1Anim.Play("Slide In");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator HideLevel1()
    {
        level1Anim.Play("Slide Out");
        yield return new WaitForSeconds(1f);
        level1.SetActive(false);
    }

    IEnumerator ShowLevel2()
    {
        level2.SetActive(true);
        level2Anim.Play("Slide In");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator HideLevel2()
    {
        level2Anim.Play("Slide Out");
        yield return new WaitForSeconds(1f);
        level2.SetActive(false);
    }

    IEnumerator ShowStage2()
    {
        stage2Anim.Play("Slide Left");
        stage1Anim.Play("Slide Left");
        yield return new WaitForSeconds(1f);
        stage1.SetActive(false);
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void GoToProfile()
    {
        SceneManager.LoadScene("ProfileScreen");
    }

    public void ShowLevel()
    {
        StartCoroutine(ShowLevel1());
    }

    public void HideLevel()
    {
        StartCoroutine(HideLevel1());
    }

    public void GoToLevel2()
    {
        StartCoroutine(ShowLevel2());
    }

    public void BackFromLevel2()
    {
        StartCoroutine(HideLevel2());
    }

    public void GoToStage2()
    {
        StartCoroutine(ShowStage2());
    }
}
