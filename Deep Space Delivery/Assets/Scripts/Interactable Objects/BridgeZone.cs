using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BridgeZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;
    private List<GameObject> warningWindows = new List<GameObject>();

    [SerializeField] private GameObject congratulations;
    [SerializeField] private GameObject warningWindowPrefab;

    private bool gameActivated;
    private string currentPlayer;
    // current number of window being displayed
    private int systemHealth;
    private float throttle;
    private int remainingWindows;

    [SerializeField] private AudioSource mainAudio;
    [SerializeField] private AudioClip mouseAudio;

    // Start is called before the first frame update
    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";
        this.systemHealth = 100;
        this.throttle = Random.Range(0.0f, 10.0f);
        this.remainingWindows = 0;

        // for(int i = 0; i < 20; i++)
        // {
        //     this.warningWindows.Add(this.displayPanel.transform.GetChild(i).gameObject);
        // }

        // this.displayPanel.SetActive(false);
        // this.warningWindows.ForEach(delegate (GameObject obj)
        // {
        //     obj.SetActive(false);
        // });

        // this.congratulations.SetActive(false);
    }

    void Update()
    {
        this.throttle -= Time.deltaTime;
        if (this.throttle <= 0 && this.systemHealth > 0)
        {
            var randDamage = Random.Range(1, 5);
            if (this.gameActivated)
            {
                for (var i = 0; i < randDamage; i++)
                {
                    this.createWindow();
                    this.remainingWindows++;
                }
            }
            this.systemHealth -= randDamage;
            this.throttle = Random.Range(0.0f, 10.0f);
        }
        // if (this.systemHealth != 100)
        // {
        //     Debug.Log(this.systemHealth);
        // }
    }

    private void createWindow()
    {
        GameObject tempWindow = Instantiate(
            warningWindowPrefab, this.displayPanel.transform.position, this.displayPanel.transform.rotation);
        tempWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(175.34f, 56.91f);
        tempWindow.transform.SetParent(this.displayPanel.transform);
        tempWindow.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        tempWindow.transform.localPosition = new Vector3(
            Random.Range(-58.0f, 58.0f), Random.Range(-100.0f, 100.0f), 0);
    }


    private void OnTriggerStay(Collider other)
    {
        //If player1 or player 2 interacts
        if ((other.gameObject.name == "Player1" && Input.GetButtonDown("Utility1"))
            || (other.gameObject.name == "Player2" && Input.GetButtonDown("Utility2")))
        {
            if (!this.gameActivated)
            {
                // activating Game
                this.displayPanel.SetActive(true);
                var temp = 100 - this.systemHealth - this.remainingWindows;
                for (var i = 0; i < temp; i++)
                {
                    this.createWindow();
                    this.remainingWindows++;
                }

                //Setting Starting Player
                this.gameActivated = true;
                this.currentPlayer = other.gameObject.name;

                //Positions panel in correct location
                if (currentPlayer == "Player2")
                {
                    this.displayPanel.transform.localPosition = new Vector3(734.5284f, 0, 0);
                }
                else
                {
                    this.displayPanel.transform.localPosition = new Vector3(-734.5284f, 0, 0);
                }
            }
            else if (this.systemHealth < 100)
            {
                var lastChild = this.displayPanel.transform.GetChild(this.displayPanel.transform.childCount - 1);
                // Debug.Log(lastChild);
                Destroy(lastChild.gameObject);
                mainAudio.PlayOneShot(mouseAudio);
                this.systemHealth++;
                this.remainingWindows--;
            }

            //Progresses game if player who initiated it interacts
            // else if(currentPlayer == other.gameObject.name)
            // {
            //     //Playing Game
            //     if(remainingWindows > 0)
            //     {
            //         //Removes one window
            //         this.remainingWindows--;
            //         warningWindows[this.remainingWindows].SetActive(false);
            //     }
            //     else
            //     {
            //         //Game Completed
            //         this.congratulations.SetActive(true);

            //         //NEED TO SEND RESULTS TO SUBSCRIBERS
            //     }
            // }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If activating player leaves after starting
        if (other.gameObject.name == currentPlayer && gameActivated)
        {
            //Closing Game
            this.gameActivated = false;
            Debug.Log("game deactivated");

            this.displayPanel.SetActive(false);
            // this.warningWindows.ForEach(delegate (GameObject obj)
            // {
            //     obj.SetActive(false);
            // });
            // this.remainingWindows = 0;
            currentPlayer = "None";

            this.congratulations.SetActive(false);

            //NEED TO SEND EARLY EXIT TO SUBSCRIBERS
        }
    }
}
