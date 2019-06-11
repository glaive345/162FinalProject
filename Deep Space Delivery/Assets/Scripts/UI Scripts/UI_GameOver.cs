using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    [SerializeField] private GameObject[] GameOverObjects;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        GameOverObjects = GameObject.FindGameObjectsWithTag("GameOver");
        hideGameOver();

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
    }

    public void hideGameOver()
    {
        foreach (GameObject i in GameOverObjects)
        {
            i.SetActive(false);
        }
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
