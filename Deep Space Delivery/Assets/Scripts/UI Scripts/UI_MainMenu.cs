using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] menuObjects;
    [SerializeField] private GameObject directionText;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject creditText;
    //Direction
    //Start
    //Credits
    void Start()
    {
        showAll();

        directionText.SetActive(false);
        backButton.SetActive(false);
        creditText.SetActive(false);

    }


    public void play()
    {
        SceneManager.LoadScene("Main Scene");
        hideAll();
    }

    public void showDirection()
    {
        hideAll();
        directionText.SetActive(true);
        backButton.SetActive(true);
    }

    public void showCredit()
    {
        hideAll();
        creditText.SetActive(true);
        backButton.SetActive(true);
    }

    public void back()
    {
        directionText.SetActive(false);
        creditText.SetActive(false);
        showAll();
    }

    public void showAll()
    {
        foreach (GameObject i in menuObjects)
        {
            i.SetActive(true);
        }
    }

    public void hideAll()
    {
        foreach (GameObject i in menuObjects)
        {
            i.SetActive(false);
        }
    }

}
