using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    [SerializeField] private GameObject[] GameOverObjects;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button MainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        GameOverObjects = GameObject.FindGameObjectsWithTag("GameOver");
        hideGameOver();
        this.RestartButton.onClick.AddListener(Restart);
        this.MainMenuButton.onClick.AddListener(ToMain);
    }

    // Update is called once per frame
    void Update()
    {
        if (false)//game over condition
        {
            showGameOver();
        }
    }

    public void showGameOver()
    {
        foreach (GameObject i in GameOverObjects)
        {
            i.SetActive(true);
        }
        RestartButton.gameObject.SetActive(true);
        MainMenuButton.gameObject.SetActive(true);
    }

    public void hideGameOver()
    {
        foreach (GameObject i in GameOverObjects)
        {
            i.SetActive(false);
        }
        RestartButton.gameObject.SetActive(false);
        MainMenuButton.gameObject.SetActive(false);
    }

    public void ToMain()
    {
        SceneManager.LoadScene("MainMenu");
        hideGameOver();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main Scene");
        hideGameOver();
    }
}
