using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] private GameObject[] pauseObjects;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();

    }

    // Update is called once per frame
    void Update()
    {

        //ECS to pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }
    public void showPaused()
    {
        foreach (GameObject i in pauseObjects)
        {
            i.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject i in pauseObjects)
        {
            i.SetActive(false);
        }
    }
}
