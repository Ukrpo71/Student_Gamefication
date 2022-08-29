using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour
{
    [SerializeField]
    private GameObject GamePanel;
    [SerializeField]
    private Animator GamePanelAnim;

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    public void OpenLevel1()
    {
        GamePanel.SetActive(true);
        GamePanelAnim.Play("SlideIn");
    }

    IEnumerator CloseLevel()
    {
        GamePanelAnim.Play("SlideOut");
        yield return new WaitForSeconds(1f);
        GamePanel.SetActive(false);
    }

    public void CloseLevel1()
    {
        StartCoroutine(CloseLevel());

    }

}
