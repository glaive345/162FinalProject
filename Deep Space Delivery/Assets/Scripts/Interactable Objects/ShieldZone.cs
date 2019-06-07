using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;

    private bool gameActivated;
    private string currentPlayer;

    //ADD OTHER VARIABLES HERE
    [SerializeField] private float lowChargePercent;
    [SerializeField] private float highChargePercent;

    [SerializeField] private float spinSpeed;
    [SerializeField] private float chargeRateMulitplier;
    [SerializeField] private float decayRateMultiplier;

    private float currentCharge;
    private bool beginDecay;

    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";

        //PREINITIALIZE VARIABLES HERE
        this.currentCharge = 0;
        beginDecay = false;


        this.displayPanel.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        var bar = this.displayPanel.transform.GetChild(3);

        if (gameActivated == true)
        {
            bar.transform.RotateAround(this.displayPanel.transform.position, new Vector3(0f, -0.43f, -1f), spinSpeed);
        }

        var chargeBar = this.displayPanel.transform.GetChild(5);

        //If player1 or player 2 interacts
        if ((other.gameObject.name == "Player1" && Input.GetButton("Utility1")) || (other.gameObject.name == "Player2" && Input.GetButton("Utility2")))
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
                if (chargeBar.transform.localScale.y <= 150 && beginDecay == false)
                {
                    chargeBar.transform.localScale = new Vector3(chargeBar.transform.localScale.x, chargeBar.transform.localScale.y + chargeRateMulitplier * Time.deltaTime, chargeBar.transform.localScale.z);
                }
                else
                {
                    beginDecay = true;
                }
                if (beginDecay)
                {
                    chargeBar.transform.localScale = new Vector3(chargeBar.transform.localScale.x, chargeBar.transform.localScale.y - decayRateMultiplier * chargeRateMulitplier * Time.deltaTime, chargeBar.transform.localScale.z);
                    if (chargeBar.transform.localScale.y <= 0)
                    {
                        beginDecay = false;
                    }
                }

                //NEED TO SEND RESULTS TO SUBSCRIBERS
            }
        }
        else
        {
            //Lose charge or input charge
            beginDecay = false;
            chargeBar.transform.localScale = new Vector3(chargeBar.transform.localScale.x, 0, chargeBar.transform.localScale.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If activating player leaves after starting
        if (other.gameObject.name == currentPlayer && gameActivated)
        {
            //Closing Game
            //RESET VARIABLES HERE



            this.gameActivated = false;
            Debug.Log("game deactivated");

            this.displayPanel.SetActive(false);

            currentPlayer = "None";


            //NEED TO SEND EARLY EXIT TO SUBSCRIBERS
        }
    }
}
