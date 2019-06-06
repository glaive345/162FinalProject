using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;
    private List<GameObject> warningWindows = new List<GameObject>();

    [SerializeField] private GameObject congratulations;

    private bool gameActivated;
    private string currentPlayer;

    private int remainingWindows;


    // Start is called before the first frame update
    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";
        this.remainingWindows = 0;

        for(int i = 0; i < 20; i++)
        {
            this.warningWindows.Add(this.displayPanel.transform.GetChild(i).gameObject);
        }

        this.displayPanel.SetActive(false);
        this.warningWindows.ForEach(delegate (GameObject obj)
        {
            obj.SetActive(false);
        });

        this.congratulations.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        //If player1 or player 2 interacts
        if((other.gameObject.name == "Player1" && Input.GetButtonDown("Utility1")) || (other.gameObject.name == "Player2" && Input.GetButtonDown("Utility2")))
        {
            if(gameActivated == false)
            {
                //Initializing Game
                this.displayPanel.SetActive(true);
                this.warningWindows.ForEach(delegate (GameObject obj)
                {
                    obj.SetActive(true);
                });
                this.remainingWindows = 20;

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
            //Progresses game if player who initiated it interacts
            else if(currentPlayer == other.gameObject.name)
            {
                //Playing Game
                if(remainingWindows > 0)
                {
                    //Removes one window
                    this.remainingWindows--;
                    warningWindows[this.remainingWindows].SetActive(false);
                }
                else
                {
                    //Game Completed
                    this.congratulations.SetActive(true);
                    
                    //NEED TO SEND RESULTS TO SUBSCRIBERS
                }
            }
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
            this.warningWindows.ForEach(delegate (GameObject obj)
            {
                obj.SetActive(false);
            });
            this.remainingWindows = 0;
            currentPlayer = "None";

            this.congratulations.SetActive(false);

            //NEED TO SEND EARLY EXIT TO SUBSCRIBERS
        }
    }
}
