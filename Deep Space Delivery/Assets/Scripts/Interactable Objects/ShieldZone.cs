using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;

    private bool gameActivated;
    private string currentPlayer;

    //ADD OTHER VARIABLES HERE
    [SerializeField] private float lowChargeMaximum;
    [SerializeField] private float highChargeMaximum;
    [SerializeField] private float highChargeLeniency;

    [SerializeField] private float spinSpeed;
    [SerializeField] private float chargeRateMulitplier;
    [SerializeField] private float decayRateMultiplier;


    private bool beginDecay;

    private GameObject rotateBar;
    private GameObject chargeBar;

    [SerializeField] private GameObject UIScripts;
    private ShieldBarManager shieldBarManager;

    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";

        //PREINITIALIZE VARIABLES HERE
        this.beginDecay = false;

        this.rotateBar = this.displayPanel.transform.GetChild(3).gameObject;
        this.chargeBar = this.displayPanel.transform.GetChild(5).gameObject;

        this.shieldBarManager = UIScripts.GetComponent<ShieldBarManager>();

        this.displayPanel.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (gameActivated == true)
        {
            rotateBar.transform.RotateAround(this.displayPanel.transform.position, new Vector3(0f, -0.43f, -1f), spinSpeed);
        }

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

                //If held too long chargebar decreases to zero
                if (beginDecay)
                {
                    if(chargeBar.transform.localScale.y - decayRateMultiplier * chargeRateMulitplier * Time.deltaTime > 0)
                    {
                        chargeBar.transform.localScale = new Vector3(chargeBar.transform.localScale.x, chargeBar.transform.localScale.y - decayRateMultiplier * chargeRateMulitplier * Time.deltaTime, chargeBar.transform.localScale.z);
                    }
                    else
                    {
                        chargeBar.transform.localScale = new Vector3(chargeBar.transform.localScale.x, 0, chargeBar.transform.localScale.z);
                    }
                }
            }
        }
        else
        {
            //Lose charge or input charge
            beginDecay = false;
            var savedCharge = chargeBar.transform.localScale.y / 150;
            chargeBar.transform.localScale = new Vector3(chargeBar.transform.localScale.x, 0, chargeBar.transform.localScale.z);

            //Check if released near green diamond for bonus charge, else regular charge
            if (Mathf.Abs(rotateBar.transform.localRotation.z) < highChargeLeniency)
            {
                savedCharge = savedCharge * highChargeMaximum;
            }
            else
            {
                savedCharge = savedCharge * lowChargeMaximum;
            }

            shieldBarManager.changeBar(savedCharge);
            //SEND TO SUBSCRIBERS RESULT
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If activating player leaves after starting
        if (other.gameObject.name == currentPlayer && gameActivated)
        {
            //Closing Game
            //RESET VARIABLES HERE
            chargeBar.transform.localScale = new Vector3(chargeBar.transform.localScale.x, 0, chargeBar.transform.localScale.z);
            beginDecay = false;


            this.gameActivated = false;
            Debug.Log("game deactivated");

            this.displayPanel.SetActive(false);

            currentPlayer = "None";
        }
    }
}
