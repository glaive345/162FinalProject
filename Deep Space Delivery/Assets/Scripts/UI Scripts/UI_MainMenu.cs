using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private Button DirectionButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button BackButton;
    [SerializeField] private Text DirectionText;
    [SerializeField] private Text CreditText;
    [SerializeField] private Text Title;

    void Start()
    {
        this.DirectionButton.onClick.AddListener(showDirection);
        this.CreditsButton.onClick.AddListener(showCredit);
        this.PlayButton.onClick.AddListener(play);
        this.BackButton.onClick.AddListener(back);

        ShowHomeScreen();
        
    }


    public void play()
    {
        SceneManager.LoadScene("Main Scene");
        hideAll();
    }

    public void showDirection()
    {
        hideAll();
        DirectionText.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
    }

    public void showCredit()
    {
        hideAll();
        CreditText.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
    }

    public void back()
    {
        DirectionText.gameObject.SetActive(false);
        CreditText.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        ShowHomeScreen();
    }

    public void hideAll()
    {
        DirectionText.gameObject.SetActive(false);
        CreditText.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        DirectionButton.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);
        Title.gameObject.SetActive(false);
    }

    public void ShowHomeScreen()
    {
        hideAll();

        Title.gameObject.SetActive(true);
        DirectionButton.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(true);
        CreditsButton.gameObject.SetActive(true);
    }

}
