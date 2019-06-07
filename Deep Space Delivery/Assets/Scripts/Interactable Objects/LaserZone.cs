using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject gun;


    private bool gameActivated;
    private string currentPlayer;

    //ADD OTHER VARIABLES HERE


    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";

        //PREINITIALIZE VARIABLES HERE

        this.displayPanel.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        //If player1 or player 2 interacts
        if ((other.gameObject.name == "Player1" && Input.GetButtonDown("Utility1"))
            || (other.gameObject.name == "Player2" && Input.GetButtonDown("Utility2")))
        {
            if (!gameActivated)
            {
                //Initializing Game
                //INITIALIZE START STATE HERE
                this.displayPanel.SetActive(true);
                var numTargets = Random.Range(10, 20);
                for (var i = 0; i < numTargets; i++)
                {
                    this.createTarget();
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
            else if (currentPlayer == other.gameObject.name)
            {
                //Playing Game
                //ADD ON-INTERACT EFFECT HERE

                this.createLaser();

                //NEED TO SEND RESULTS TO SUBSCRIBERS
            }
        }
    }

    private void createTarget()
    {
        GameObject tempTarget = Instantiate(
            this.targetPrefab, this.displayPanel.transform.position, Quaternion.identity);
        tempTarget.GetComponent<RectTransform>().sizeDelta = new Vector2(1.0f, 1.0f);
        tempTarget.transform.SetParent(this.displayPanel.transform);
        tempTarget.transform.localPosition = new Vector3(162.9f, -126.2f, 0);
        var randScale = Random.Range(10.0f, 25.0f);
        tempTarget.GetComponent<RectTransform>().localScale = new Vector3(randScale, randScale, randScale);
        var randForce = new Vector2(Random.Range(-7.5f, -3.0f), Random.Range(100.0f, 200.0f));
        tempTarget.GetComponent<Rigidbody2D>().AddForce(randForce);
    }

    private void createLaser()
    {
        GameObject tempLaser = Instantiate(
            this.laserPrefab, this.gun.transform.position, Quaternion.identity);
        tempLaser.GetComponent<RectTransform>().sizeDelta = new Vector2(1.0f, 1.0f);
        tempLaser.transform.SetParent(this.displayPanel.transform);
        tempLaser.GetComponent<RectTransform>().localScale = new Vector3(13.0f, -0.5f, 1.0f);
        tempLaser.GetComponent<Rigidbody2D>().AddForce(new Vector2(200.0f, 0));
    }

    private void OnTriggerExit(Collider other)
    {
        //If activating player leaves after starting
        if (other.gameObject.name == currentPlayer && gameActivated)
        {
            //Closing Game
            //RESET VARIABLES HERE

            // destroy prefabs



            this.gameActivated = false;
            Debug.Log("game deactivated");

            this.displayPanel.SetActive(false);

            currentPlayer = "None";


            //NEED TO SEND EARLY EXIT TO SUBSCRIBERS
        }
    }
}
