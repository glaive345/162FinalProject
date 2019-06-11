using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BridgeZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;
    private List<GameObject> warningWindows = new List<GameObject>();

    [SerializeField] private GameObject warningWindowPrefab;

    [SerializeField] private float throttleReset;
    [SerializeField] private int minSpawn;
    [SerializeField] private int maxSpawn;
    [SerializeField] private UnityEngine.UI.Text UIText;

    private bool gameActivated;
    private string currentPlayer;
    // current number of window being displayed
    private int systemHealth;
    private float throttle;
    private int remainingWindows;

    [SerializeField] private AudioSource mainAudio;
    [SerializeField] private AudioClip mouseAudio;

    // Start is called before the first frame update
    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";
        this.systemHealth = 100;
        this.throttle = throttleReset;
        this.remainingWindows = 0;

    }

    void Update()
    {
        this.throttle -= Time.deltaTime;
        if (this.throttle <= 0 && this.systemHealth > 0)
        {
            var randDamage = Random.Range(minSpawn, maxSpawn);
            for (var i = 0; i < randDamage; i++)
            {
                this.createWindow();
                this.remainingWindows++;
            }
            this.systemHealth -= randDamage;
            this.throttle = throttleReset;
        }
        if(remainingWindows > 0)
        {
            UIText.text = "Bridge: " + remainingWindows + " Errors";
            UIText.color = new Color(255, 0, 0);

        }
        else
        {
            UIText.text = "Bridge: Good";
            UIText.color = new Color(0, 255, 0);
        }
    }

    private void createWindow()
    {
        GameObject tempWindow = Instantiate(warningWindowPrefab, this.displayPanel.transform.position, this.displayPanel.transform.rotation);
        tempWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(175.34f, 56.91f);
        tempWindow.transform.SetParent(this.displayPanel.transform);
        tempWindow.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        tempWindow.transform.localPosition = new Vector3(
            Random.Range(-58.0f, 58.0f), Random.Range(-100.0f, 100.0f), 0);
    }


    private void OnTriggerStay(Collider other)
    {
        //If player1 or player 2 interacts
        if ((other.gameObject.name == "Player1" && Input.GetButtonDown("Utility1")) || (other.gameObject.name == "Player2" && Input.GetButtonDown("Utility2")))
        {
            if (!this.gameActivated)
            {
                // activating Game
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
            else if (currentPlayer == other.gameObject.name && this.systemHealth < 100)
            {
                var lastChild = this.displayPanel.transform.GetChild(this.displayPanel.transform.childCount - 1);
                // Debug.Log(lastChild);
                Destroy(lastChild.gameObject);
                mainAudio.PlayOneShot(mouseAudio);
                this.systemHealth++;
                this.remainingWindows--;
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

            currentPlayer = "None";
            
        }
    }
}
