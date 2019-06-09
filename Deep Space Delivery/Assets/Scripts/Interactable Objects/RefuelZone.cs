using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;

    private bool gameActivated;
    private string currentPlayer;

    //ADD OTHER VARIABLES HERE
    private int barrelCount;
    private int fuelPointingTo;

    private GameObject barrel;
    private GameObject barrelFuelBar;
    private GameObject barrelCurrentFuel;
    private GameObject mainFuel;
    private GameObject topAuxFuel;
    private GameObject botAuxFuel;
    private UnityEngine.UI.Text storedBarrels;
    private GameObject noBarrels;
    private GameObject currentPointer;



    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";

        //PREINITIALIZE VARIABLES HERE
        barrelCount = 0;
        fuelPointingTo = 0;

        barrel = this.displayPanel.transform.GetChild(2).gameObject;
        barrelFuelBar = this.displayPanel.transform.GetChild(3).gameObject;
        barrelCurrentFuel = this.displayPanel.transform.GetChild(4).gameObject;
        mainFuel = this.displayPanel.transform.GetChild(6).gameObject;
        topAuxFuel = this.displayPanel.transform.GetChild(8).gameObject;
        botAuxFuel = this.displayPanel.transform.GetChild(10).gameObject;
        storedBarrels = this.displayPanel.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        noBarrels = this.displayPanel.transform.GetChild(1).gameObject;
        currentPointer = this.displayPanel.transform.GetChild(11).gameObject;

        barrel.SetActive(false);
        barrelFuelBar.SetActive(false);
        barrelCurrentFuel.SetActive(false);

        this.displayPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var barrel = other.gameObject.transform.GetChild(2).gameObject;
        //Checks if carrying a barrel currently
        if(barrel.name == "Carrying Barrel" && barrel.activeSelf)
        {
            barrel.SetActive(false);
            barrelCount++;

            this.updateDisplay();
        }
    }

    private void OnTriggerStay(Collider other)
    {
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
                fuelPointingTo++;
                if(fuelPointingTo > 3)
                {
                    fuelPointingTo = 0;
                }


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



            this.gameActivated = false;
            Debug.Log("game deactivated");

            this.displayPanel.SetActive(false);

            currentPlayer = "None";


            //NEED TO SEND EARLY EXIT TO SUBSCRIBERS
        }
    }

    //Sets active state for the various parts of the minigame depending if there are barrels left
    private void updateDisplay()
    {
        if(barrelCount > 0)
        {
            noBarrels.SetActive(false);
            barrel.SetActive(true);
            barrelFuelBar.SetActive(true);
            barrelCurrentFuel.SetActive(true);
        }
        else
        {
            noBarrels.SetActive(true);
            barrel.SetActive(false);
            barrelFuelBar.SetActive(false);
            barrelCurrentFuel.SetActive(false);
        }
        storedBarrels.text = "Barrels: " + barrelCount;
    }
}
