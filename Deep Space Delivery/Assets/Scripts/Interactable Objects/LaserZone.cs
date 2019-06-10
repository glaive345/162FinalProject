using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject coolDownBar;

    private int remainingTargets;
    private float dtime;
    public int Damage;

    private bool gameActivated;
    private string currentPlayer;
    private bool gameInitiated;
    private float heat;
    private float prevTime;
    private bool coolingDown;

    private bool eventActivated;
    private EventManager eventManager;
    [SerializeField] private GameObject UIScript;

    //ADD OTHER VARIABLES HERE


    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";
        this.remainingTargets = 0;
        this.dtime = 0;
        this.Damage = 0;
        this.gameInitiated = false;
        this.prevTime = 0;
        this.coolingDown = false;
        this.eventActivated = false;

        //PREINITIALIZE VARIABLES HERE
        this.displayPanel.SetActive(false);

        eventManager = UIScript.GetComponent<EventManager>();
    }

    void Update()
    {
        if (this.heat > 0)
        {
            this.heat -= Time.deltaTime * 10;
        }

        if (this.heat < 60)
        {
            this.coolingDown = false;
        }

        this.coolDownBar.transform.localScale = new Vector3(3 * this.heat, 10, 1);
        // dtime -= Time.deltaTime;
        // if (this.gameActivated && this.remainingTargets > 0 && this.dtime <= 0)
        // {
        //     this.createTarget();
        //     this.dtime = Random.Range(0.0f, 1.0f);
        //     this.remainingTargets--;
        // }
    }

    private void OnTriggerStay(Collider other)
    {
        //If player1 or player 2 interacts
        dtime -= Time.deltaTime;
        if (this.gameActivated && this.remainingTargets > 0 && this.dtime <= 0)
        {
            this.createTarget();
            this.dtime = Random.Range(0.0f, 1.0f);
            this.remainingTargets--;
        }

        if ((other.gameObject.name == "Player1" && Input.GetButtonDown("Utility1"))
            || (other.gameObject.name == "Player2" && Input.GetButtonDown("Utility2")))
        {
            if (!gameActivated)
            {
                //Initializing Game
                //INITIALIZE START STATE HERE
                this.displayPanel.SetActive(true);
                if (!gameInitiated)
                {
                    this.remainingTargets = Random.Range(10, 20);
                    this.gameInitiated = true;
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
            //Progresses game if player who initiated it interacts
            else if (currentPlayer == other.gameObject.name && !this.coolingDown)
            {
                //Playing Game
                //ADD ON-INTERACT EFFECT HERE
                this.createLaser();

                if (this.prevTime != 0)
                {
                    var tempTime = Time.time - this.prevTime;
                    float tempHeat = 5;
                    if (tempTime < 2)
                    {
                        tempHeat = -12.5f * tempTime + 20;
                    }
                    this.heat += tempHeat;
                    if (this.heat > 100)
                    {
                        this.heat = 100;
                        this.coolingDown = true;
                    }
                    Debug.Log(this.heat);
                }

                this.prevTime = Time.time;

                //NEED TO SEND RESULTS TO SUBSCRIBERS
            }
        }
    }

    private void createTarget()
    {
        GameObject tempTarget = Instantiate(
            this.targetPrefab, this.displayPanel.transform.position, this.displayPanel.transform.rotation);
        tempTarget.transform.SetParent(this.displayPanel.transform);
        tempTarget.transform.localPosition = new Vector3(162.9f, -126.2f, 0);
        var randScale = Random.Range(10.0f, 25.0f);
        tempTarget.transform.localScale = new Vector3(randScale, randScale, randScale);
        var randForce = new Vector2(Random.Range(-7.5f, -3.0f), Random.Range(70.0f, 150.0f));
        tempTarget.GetComponent<Rigidbody>().AddRelativeForce(randForce);
    }

    private void createLaser()
    {
        GameObject tempLaser = Instantiate(
            this.laserPrefab, this.gun.transform.position, this.displayPanel.transform.rotation);
        tempLaser.transform.SetParent(this.displayPanel.transform);
        tempLaser.transform.localScale = new Vector3(50.0f, 1.0f, 1.0f);
        tempLaser.GetComponent<Rigidbody>().AddRelativeForce(new Vector2(350.0f, 0));
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

    public void setActiveEvent(bool setEvent)//string if more than one event
    {
        this.eventActivated = setEvent;
    }
}
