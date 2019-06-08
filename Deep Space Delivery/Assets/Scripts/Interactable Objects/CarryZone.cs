using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private GameObject barrelProp;

    private bool gameActivated;
    private string currentPlayer;

    //ADD OTHER VARIABLES HERE
    [SerializeField] private float wobblinessMultiplier;
    [SerializeField] private float tippingThreshold;
    [SerializeField] private float spillTime;
    [SerializeField] private float inputTiltMultiplier;

    private bool inputTiltPositive;

    private bool spill;
    private float spillSustainTimer;

    [SerializeField] private float respawnTime;
    private float timer;

    private GameObject barrel;
    private GameObject spilledText;

    [SerializeField] private GameObject carryBarrel1;
    [SerializeField] private GameObject carryBarrel2;


    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";

        //PREINITIALIZE VARIABLES HERE
        barrel = this.displayPanel.transform.GetChild(0).gameObject;
        spilledText = this.displayPanel.transform.GetChild(1).gameObject;
        inputTiltPositive = true;

        carryBarrel1.SetActive(false);
        carryBarrel2.SetActive(false);

        spill = false;
        spillSustainTimer = 0;
        timer = 0;

        this.displayPanel.SetActive(false);
    }

    private void Update()
    {
        //While Carrying Barrel
        if (gameActivated)
        {
            if (spill)
            {
                Debug.Log("spilled");
                //On first frame of spill
                if(spillSustainTimer == 0)
                {
                    spilledText.GetComponent<UnityEngine.UI.Text>().text = "Spilled";
                    //Immediately remove barrel, preventing drop off
                    if (currentPlayer == "Player1")
                    {
                        carryBarrel1.SetActive(false);
                    }
                    else if(currentPlayer == "Player2")
                    {
                        carryBarrel2.SetActive(false);
                    }
                }
                spillSustainTimer += Time.deltaTime;
                if(spillSustainTimer >= spillTime)
                {
                    this.SetInactive();
                }
            }
            else
            {
                //spilledText doubles as a timer while not spilled
                spilledText.GetComponent<UnityEngine.UI.Text>().text = (respawnTime - timer).ToString();
                //Rotates barrel towards direction it is closest to falling down towards
                if (barrel.transform.localRotation.z > 0)
                {
                    barrel.transform.Rotate(0, 0, wobblinessMultiplier * Time.deltaTime);
                }
                else
                {
                    barrel.transform.Rotate(0, 0, - wobblinessMultiplier * Time.deltaTime);
                }

                //Apply player input
                if ((currentPlayer == "Player1" && Input.GetButton("Utility1")) || (currentPlayer == "Player2" && Input.GetButton("Utility2")))
                {
                    var inputTilt = inputTiltMultiplier * wobblinessMultiplier * Time.deltaTime;
                    if (!inputTiltPositive)
                    {
                        inputTilt = -inputTilt;
                    }
                    barrel.transform.Rotate(0, 0, inputTilt);
                }
                //While holding down the button will not change direction of input tilting
                else if(inputTiltPositive && barrel.transform.localRotation.z > 0)
                {
                    inputTiltPositive = false;
                }
                else if(!inputTiltPositive && barrel.transform.localRotation.z < 0)
                {
                    inputTiltPositive = true;
                }

                //If barrel tilts beyond threshold
                if (Mathf.Abs(barrel.transform.localRotation.z) > tippingThreshold)
                {
                    spill = true;
                }
            }
        }
        //Respawn timer for barrel prop at pickup location
        if (!barrelProp.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer > respawnTime)
            {
                barrelProp.SetActive(true);
                //If game still activated, deactivate game
                if (gameActivated)
                {
                    this.SetInactive();
                }
            }
        }

        //Checks if barrel has been dropped off
        if (!spill)
        {
            if ((gameActivated && currentPlayer == "Player1" && !carryBarrel1.activeSelf) || (gameActivated && currentPlayer == "Player2" && !carryBarrel2.activeSelf))
            {
                this.SetInactive();
                Debug.Log("Delivered Barrel");
            }
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
                spill = false;
                spillSustainTimer = 0;
                timer = 0;

                barrelProp.SetActive(false);

                this.displayPanel.SetActive(true);

                //Setting Starting Player
                this.gameActivated = true;
                this.currentPlayer = other.gameObject.name;

                //Positions panel in correct location
                if (currentPlayer == "Player2")
                {
                    this.displayPanel.transform.localPosition = new Vector3(734.5284f, 0, 0);
                    carryBarrel2.SetActive(true);
                }
                else
                {
                    this.displayPanel.transform.localPosition = new Vector3(-734.5284f, 0, 0);
                    carryBarrel1.SetActive(true);
                }
            }
        }
    }

    public void SetInactive()
    {
        this.barrel.transform.localRotation = Quaternion.Euler(barrel.transform.localRotation.x, barrel.transform.localRotation.y, 0);

        //Removes barrel from in front of player
        if (currentPlayer == "Player2")
        {
            carryBarrel2.SetActive(false);
        }
        else
        {
            carryBarrel1.SetActive(false);
        }

        this.gameActivated = false;
        this.currentPlayer = "None";
        this.displayPanel.SetActive(false);
    }
}
