using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SodaZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;

    private bool gameActivated;
    private string currentPlayer;

    //ADD OTHER VARIABLES HERE

    [SerializeField] private float speedBoostMultiplier;
    [SerializeField] private float stepSpeed;
    [SerializeField] private float shakeThreshold;
    [SerializeField] private float shakeSpeedMultiplier;

    [SerializeField] private Text cansDrunkText;

    private float shakeAmount;
    private float cansDrunk;
    private int eventCansDrunk;

    private GameObject track;
    private GameObject sodaCan;

    private bool eventActivated;
    private EventManager eventManager;
    [SerializeField] private GameObject UIScript;

    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";

        //PREINITIALIZE VARIABLES HERE
        cansDrunk = 0;

        track = this.displayPanel.transform.GetChild(1).gameObject;
        sodaCan = this.displayPanel.transform.GetChild(2).gameObject;

        this.displayPanel.SetActive(false);

        eventManager = UIScript.GetComponent<EventManager>();
        eventCansDrunk = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameActivated)
        {
            sodaCan.transform.Rotate(new Vector3(0, 25, 10));
            track.transform.localScale = new Vector3(track.transform.localScale.x, track.transform.localScale.y + shakeSpeedMultiplier * Time.deltaTime, track.transform.localScale.z);
        }
        //If player1 or player 2 interacts
        if ((other.gameObject.name == "Player1" && Input.GetButtonDown("Utility1")) || (other.gameObject.name == "Player2" && Input.GetButtonDown("Utility2")))
        {
            if (gameActivated == false)
            {
                //Initializing Game
                //INITIALIZE START STATE HERE


                this.displayPanel.SetActive(true);

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
            else if (currentPlayer == other.gameObject.name)
            {
                //Playing Game
                //ADD ON-INTERACT EFFECT HERE
                cansDrunk++;
                if (eventActivated)
                {
                    eventCansDrunk++;
                    if(eventCansDrunk == 10)
                    {
                        this.eventManager.returnFunction("soda");
                        eventCansDrunk = 0;
                    }
                }
                cansDrunkText.text = "Cans Drunk: " + cansDrunk;

                track.transform.localScale = new Vector3(track.transform.localScale.x, 0, track.transform.localScale.z);


                //NEED TO SEND RESULTS TO SUBSCRIBERS
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If activating player leaves after starting
        if (other.gameObject.name == currentPlayer && gameActivated)
        {
            //Closing Game
            //RESET VARIABLES HERE
            track.transform.localScale = new Vector3(track.transform.localScale.x, 0, track.transform.localScale.z);



            this.gameActivated = false;
            Debug.Log("game deactivated");

            this.displayPanel.SetActive(false);

            currentPlayer = "None";


            //NEED TO SEND EARLY EXIT TO SUBSCRIBERS
        }
    }
    public void setActiveEvent(bool setEvent)//string if more than one event
    {
        this.eventActivated = setEvent;
    }
}
