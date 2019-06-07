using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusGame : MonoBehaviour, IMinigame
{
    [SerializeField] private GameObject warningWindow0;
    [SerializeField] private GameObject warningWindow1;
    [SerializeField] private GameObject warningWindow2;
    [SerializeField] private GameObject warningWindow3;
    [SerializeField] private GameObject warningWindow4;
    [SerializeField] private GameObject warningWindow5;
    [SerializeField] private GameObject warningWindow6;
    [SerializeField] private GameObject warningWindow7;
    [SerializeField] private GameObject warningWindow8;
    [SerializeField] private GameObject warningWindow9;
    [SerializeField] private GameObject warningWindow10;
    [SerializeField] private GameObject warningWindow11;
    [SerializeField] private GameObject warningWindow12;
    [SerializeField] private GameObject warningWindow13;
    [SerializeField] private GameObject warningWindow14;
    [SerializeField] private GameObject warningWindow15;
    [SerializeField] private GameObject warningWindow16;
    [SerializeField] private GameObject warningWindow17;
    [SerializeField] private GameObject warningWindow18;
    [SerializeField] private GameObject warningWindow19;
    [SerializeField] private GameObject displayPanel;

    private List<GameObject> warningWindows = new List<GameObject>();
    private bool playerInPlayZone;
    // whether the game can be played
    private bool gameActivated;
    private bool gameInitialized;
    // number of unclosed windows after time up
    private int remainingWindows;

    // public int key = 11;

    void Start()
    {
        // stay true for now
        this.gameActivated = true;
        this.playerInPlayZone = true;
        this.gameInitialized = false;

        this.remainingWindows = 0;
        this.gameInitialized = false;

        this.warningWindows.Add(this.warningWindow0);
        this.warningWindows.Add(this.warningWindow1);
        this.warningWindows.Add(this.warningWindow2);
        this.warningWindows.Add(this.warningWindow3);
        this.warningWindows.Add(this.warningWindow4);
        this.warningWindows.Add(this.warningWindow5);
        this.warningWindows.Add(this.warningWindow6);
        this.warningWindows.Add(this.warningWindow7);
        this.warningWindows.Add(this.warningWindow8);
        this.warningWindows.Add(this.warningWindow9);
        this.warningWindows.Add(this.warningWindow10);
        this.warningWindows.Add(this.warningWindow11);
        this.warningWindows.Add(this.warningWindow12);
        this.warningWindows.Add(this.warningWindow13);
        this.warningWindows.Add(this.warningWindow14);
        this.warningWindows.Add(this.warningWindow15);
        this.warningWindows.Add(this.warningWindow16);
        this.warningWindows.Add(this.warningWindow17);
        this.warningWindows.Add(this.warningWindow18);
        this.warningWindows.Add(this.warningWindow19);

        this.displayPanel.SetActive(false);
        this.warningWindows.ForEach(delegate(GameObject obj)
        {
            obj.SetActive(false);
        });
    }

    void Update()
    {
        if (this.gameActivated)
        {
            if (this.playerInPlayZone && !this.gameInitialized)
            {
                // initialize game
                this.displayPanel.SetActive(true);
                this.warningWindows.ForEach(delegate (GameObject obj)
                {
                    obj.SetActive(true);
                });
                this.remainingWindows = 20;
                this.gameInitialized = true;
            }
            // close game if not in playzone
            else if (!this.playerInPlayZone)
            {
                this.displayPanel.SetActive(false);
            }
        }
        // else
        // {

        // }
    }

    public void Interaction()
    {
        if (this.playerInPlayZone && this.gameInitialized && this.remainingWindows > 0)
        {
            this.remainingWindows--;
            // Debug.Log("closing: " + this.remainingWindows);
            warningWindows[this.remainingWindows].SetActive(false);
        }
    }
}
