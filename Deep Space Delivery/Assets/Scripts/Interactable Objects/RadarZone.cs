using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;

    private bool gameActivated;
    private string currentPlayer;

    //ADD OTHER VARIABLES HERE
    [SerializeField] private GameObject scanPrefab;
    private Text scansRequiredText;
    private Text nextScanText;
    private Text scansLeftText;
    private Text numberText;
    private GameObject scansCompleted;
    private GameObject check;
    private GameObject cross;
    private GameObject nextScanBar;
    private GameObject scanTimeBar;
    private GameObject scanArea;
    private Material checkMaterial;
    private Material crossMaterial;

    private float nextScanBarPos;

    [SerializeField] private UnityEngine.UI.Text UIText;

    [SerializeField] private float nextScanTime;
    private float nextScanTimer;
    private int numberOfScansLeft;

    [SerializeField] private int minSpawn;
    [SerializeField] private int maxSpawn;
    private int randomAmount;
    private int numberGuessed;

    [SerializeField] private float currentScanTime;
    private float currentTimer;


    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";

        //PREINITIALIZE VARIABLES HERE
        scansRequiredText = displayPanel.transform.GetChild(0).GetComponent<Text>();
        nextScanText = displayPanel.transform.GetChild(4).GetComponent<Text>();
        scansLeftText = displayPanel.transform.GetChild(5).GetComponent<Text>();
        numberText = displayPanel.transform.GetChild(8).GetComponent<Text>();
        check = displayPanel.transform.GetChild(10).gameObject;
        cross = displayPanel.transform.GetChild(9).gameObject;
        nextScanBar = displayPanel.transform.GetChild(3).gameObject;
        scanTimeBar = displayPanel.transform.GetChild(7).gameObject;
        scanArea = displayPanel.transform.GetChild(1).gameObject;
        scansCompleted = displayPanel.transform.GetChild(11).gameObject;

        nextScanBarPos = nextScanBar.transform.localPosition.x;

        currentTimer = 0;
        numberOfScansLeft = 0;
        nextScanTimer = 0;
        randomAmount = 0;
        numberGuessed = 0;

        scansLeftText.text = "0";

        this.displayPanel.SetActive(false);
    }

    private void Update()
    {
        nextScanTimer += Time.deltaTime;
        if(nextScanTimer >= nextScanTime)
        {
            if(numberOfScansLeft == 0)
            {
                this.createNewBoard();
                scansCompleted.SetActive(false);
            }
            numberOfScansLeft++;
            scansLeftText.text = numberOfScansLeft.ToString();
            nextScanTimer = 0;
        }

        this.updateUI();
        this.updateBars();
    }


    private void OnTriggerStay(Collider other)
    {
        if (gameActivated)
        {
            if(numberOfScansLeft > 0)
            {
                currentTimer += Time.deltaTime;
                if (currentTimer > currentScanTime)
                {
                    if (numberGuessed == randomAmount)
                    {
                        check.SetActive(true);
                        cross.SetActive(false);
                        numberOfScansLeft--;
                        scansLeftText.text = numberOfScansLeft.ToString();
                        if (numberOfScansLeft == 0)
                        {
                            scansCompleted.SetActive(true);
                        }
                        this.updateUI();
                    }
                    else
                    {
                        check.SetActive(false);
                        cross.SetActive(true);
                    }
                    this.createNewBoard();

                    numberGuessed = 0;
                    numberText.text = numberGuessed.ToString();

                    currentTimer = 0;
                }
            }
            
        }

        //If player1 or player 2 interacts
        if ((other.gameObject.name == "Player1" && Input.GetButtonDown("Utility1")) || (other.gameObject.name == "Player2" && Input.GetButtonDown("Utility2")))
        {
            if (gameActivated == false)
            {
                //Initializing Game
                //INITIALIZE START STATE HERE
                currentTimer = 0;
                numberGuessed = 0;
                numberText.text = numberGuessed.ToString();

                if(numberOfScansLeft > 0)
                {
                    this.createNewBoard();
                    scansCompleted.SetActive(false);
                }
                else
                {
                    scansCompleted.SetActive(true);
                }

                this.displayPanel.SetActive(true);
                this.cross.SetActive(false);
                this.check.SetActive(false);

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
                numberGuessed++;
                numberText.text = numberGuessed.ToString();
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

    private void createScanObject()
    {
        GameObject tempWindow = Instantiate(scanPrefab, this.scanArea.transform.position, this.scanArea.transform.rotation);
        tempWindow.transform.SetParent(this.scanArea.transform);
        tempWindow.GetComponent<RectTransform>().localScale = new Vector3(0.07272f, 0.13332f, 5.0f);
        tempWindow.transform.localPosition = new Vector3(Random.Range(-.45f, .45f), Random.Range(-.45f, .45f), 0);
    }

    private void createNewBoard()
    {
        //Removes old boards
        for(int i = 0; i < randomAmount; i++)
        {
            var firstChild = this.scanArea.transform.GetChild(i).gameObject;
            Destroy(firstChild);
            Debug.Log("Destroyed");
        }

        //Creates new board
        randomAmount = Random.Range(minSpawn, maxSpawn);
        for(int i = 0; i < randomAmount; i++)
        {
            createScanObject();
        }
    }
    

    private void updateUI()
    {
        UIText.text = "Radar: " + numberOfScansLeft + " Scans";
        if (numberOfScansLeft == 0)
        {
            UIText.color = new Color(0, 255, 0);
            scansRequiredText.text = "Scans Completed";
        }
        else if (numberOfScansLeft > 0 && numberOfScansLeft < 4)
        {
            UIText.color = new Color(255, 255, 0);
            scansRequiredText.text = "Scans Required";
        }
        else if (numberOfScansLeft >= 4)
        {
            UIText.color = new Color(255, 0, 0);
            scansRequiredText.text = "Scans Required";
        }
    }

    private void updateBars()
    {
        var nextScanPercent = nextScanTimer / nextScanTime;
        var currentScanPercent = currentTimer / currentScanTime;

        nextScanBar.transform.localScale = new Vector3(nextScanPercent * 200, nextScanBar.transform.localScale.y, nextScanBar.transform.localScale.z);
        nextScanBar.transform.localPosition = new Vector3(nextScanBarPos + nextScanPercent * 100, nextScanBar.transform.localPosition.y, nextScanBar.transform.localPosition.z);
        scanTimeBar.transform.localScale = new Vector3(currentScanPercent * 200, scanTimeBar.transform.localScale.y, scanTimeBar.transform.localScale.z);
    }
}