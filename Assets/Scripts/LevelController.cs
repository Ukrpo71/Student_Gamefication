using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public DataController dataController;

    [SerializeField]
    private GameObject[] unlockedStars;

    [SerializeField]
    private Button[] buttons;

    [SerializeField]
    private GameObject[] grayStars;

    [SerializeField]
    private GameObject[] clearedStars;

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        UnlockLevels();
        ClearedLevels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockLevels()
    {
        for (int i = 0; i < 3; i++)
        {
            if (dataController.finishedLevels[i] == true)
            {
                unlockedStars[i].SetActive(true);
                buttons[i].interactable = true;
                grayStars[i].SetActive(false);
            }
            else
            {
                unlockedStars[i].SetActive(true);
                grayStars[i].SetActive(true);
                buttons[i].interactable = false;
            }
        }
    }

    public void ClearedLevels()
    {
        for (int i = 0; i < 3; i++)
        {
            if (dataController.clearedHardSkills[i] == true)
            {
                unlockedStars[i].SetActive(false);
                buttons[i].interactable = true;
                grayStars[i].SetActive(false);

            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (dataController.clearedHardSkills[i] == true)
            {
                clearedStars[i].SetActive(false);

            }
        }
    }
}
