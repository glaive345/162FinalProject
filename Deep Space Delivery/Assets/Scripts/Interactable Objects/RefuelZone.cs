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

    [SerializeField] private float refuelRateMultiplier;
    [SerializeField] private float barrelDrainMultiplier;
    [SerializeField] private float fuelDrainMultiplier;
    [SerializeField] private float mainEngineDrainMultiplier;

    public bool mainActive;
    public bool topActive;
    public bool botActive;

    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";

        //PREINITIALIZE VARIABLES HERE
        barrelCount = 3;
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

        this.updateDisplay();

        mainActive = true;
        topActive = true;
        botActive = true;

        this.displayPanel.SetActive(false);
    }

    private void Update()
    {
        var drain = - fuelDrainMultiplier * Time.deltaTime;
        this.changeFuelLevel(0, drain * mainEngineDrainMultiplier);
        this.changeFuelLevel(1, drain);
        this.changeFuelLevel(2, drain);

        if (gameActivated)
        {
            //Moves pointer when button is released
            if ((currentPlayer == "Player1" && Input.GetButtonUp("Utility1")) || (currentPlayer == "Player2" && Input.GetButtonUp("Utility2")))
            {
                fuelPointingTo++;
                if (fuelPointingTo > 2)
                {
                    fuelPointingTo = 0;
                }
                switch (fuelPointingTo)
                {
                    case 0:
                        currentPointer.GetComponent<RectTransform>().localPosition = new Vector3(-68.1f, 0, 0);
                        break;
                    case 1:
                        currentPointer.GetComponent<RectTransform>().localPosition = new Vector3(-40, 75, 0);
                        break;
                    case 2:
                        currentPointer.GetComponent<RectTransform>().localPosition = new Vector3(-40, -75, 0);
                        break;
                }
            }
        }
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
        }
        if ((currentPlayer == other.gameObject.name && Input.GetButton("Utility1")) || (currentPlayer == other.gameObject.name && Input.GetButton("Utility2")))
        {
            if(barrelCount > 0)
            {
                if (barrelCurrentFuel.transform.localScale.y - barrelDrainMultiplier * Time.deltaTime > 0)
                {
                    barrelCurrentFuel.transform.localScale = new Vector3(barrelCurrentFuel.transform.localScale.x, barrelCurrentFuel.transform.localScale.y - barrelDrainMultiplier * Time.deltaTime, barrelCurrentFuel.transform.localScale.z);


                    this.changeFuelLevel(fuelPointingTo, refuelRateMultiplier * Time.deltaTime);
                }
                //When current barrel runs out of fuel
                else
                {
                    barrelCount--;
                    //If barrels still remaining refill barrel
                    if(barrelCount > 0)
                    {
                        barrelCurrentFuel.transform.localScale = new Vector3(barrelCurrentFuel.transform.localScale.x, 100, barrelCurrentFuel.transform.localScale.z);
                    }
                    this.updateDisplay();
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

    public void changeFuelLevel(int tankNumber, float change)
    {
        switch (tankNumber)
        {
            //Main Fuel
            case 0:
                if (mainFuel.transform.localScale.y + change < 250 && mainFuel.transform.localScale.y + change > 0)
                {
                    mainFuel.transform.localScale = new Vector3(mainFuel.transform.localScale.x, mainFuel.transform.localScale.y + change, mainFuel.transform.localScale.z);
                    mainActive = true;
                }
                else if(mainFuel.transform.localScale.y + change >= 250)
                {
                    mainFuel.transform.localScale = new Vector3(mainFuel.transform.localScale.x, 250, mainFuel.transform.localScale.z);
                    mainActive = true;
                }
                else
                {
                    mainFuel.transform.localScale = new Vector3(mainFuel.transform.localScale.x, 0, mainFuel.transform.localScale.z);
                    mainActive = false;
                }
                break;
            //Top Aux
            case 1:
                if (topAuxFuel.transform.localScale.y + change < 100 && topAuxFuel.transform.localScale.y + change > 0)
                {
                    topAuxFuel.transform.localScale = new Vector3(topAuxFuel.transform.localScale.x, topAuxFuel.transform.localScale.y + change, topAuxFuel.transform.localScale.z);
                    topActive = true;
                }
                else if(topAuxFuel.transform.localScale.y + change >= 100)
                {
                    topAuxFuel.transform.localScale = new Vector3(topAuxFuel.transform.localScale.x, 100, topAuxFuel.transform.localScale.z);
                    topActive = true;
                }
                else
                {
                    topAuxFuel.transform.localScale = new Vector3(topAuxFuel.transform.localScale.x, 0, topAuxFuel.transform.localScale.z);
                    topActive = false;
                }
                break;
            //Bot Aux
            case 2:
                if (botAuxFuel.transform.localScale.y + change < 100 && botAuxFuel.transform.localScale.y + change > 0)
                {
                    botAuxFuel.transform.localScale = new Vector3(botAuxFuel.transform.localScale.x, botAuxFuel.transform.localScale.y + change, botAuxFuel.transform.localScale.z);
                    botActive = true;
                }
                else if(botAuxFuel.transform.localScale.y + change >= 100)
                {
                    botAuxFuel.transform.localScale = new Vector3(botAuxFuel.transform.localScale.x, 100, botAuxFuel.transform.localScale.z);
                    botActive = true;
                }
                else
                {
                    botAuxFuel.transform.localScale = new Vector3(botAuxFuel.transform.localScale.x, 0, botAuxFuel.transform.localScale.z);
                    botActive = false;
                }
                break;
        }
    }
}
