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
        menuObjects = GameObject.FindGameObjectsWithTag("MainMenu");
        directionText = GameObject.FindGameObjectsWithTag("Direction");
        creditText = GameObject.FindGameObjectsWithTag("Credit");
        backButton = GameObject.FindGameObjectsWithTag("backButton");
        showAll();

        directionText.setActive(false);
        backButton.setActive(false);
        creditText.setActive(false);

    }


    public void play()
    {
        SceneManager.LoadScene("Main Scene");
        hideAll();
    }

    public void showDirection()
    {
        hideAll();
        directionText.setActive(true);
        backButton.setActive(true);
    }

    public void showCredit()
    {
        hideAll();
        creditText.setActive(true);
        backButton.setActive(true);
    }

    public void back()
    {
        directionText.setActive(false);
        creditText.setActive(false);
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
