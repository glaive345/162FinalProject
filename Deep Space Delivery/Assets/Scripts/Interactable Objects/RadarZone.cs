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
    private GameObject check;
    private GameObject cross;
    private GameObject nextScanBar;
    private GameObject scanTimeBar;
    private GameObject scanArea;
    private Material checkMaterial;
    private Material crossMaterial;

    [SerializeField] private float nextScanTime;
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
        checkMaterial = check.GetComponent<Renderer>().material;
        crossMaterial = cross.GetComponent<Renderer>().material;

        currentTimer = 0;

        this.displayPanel.SetActive(false);
    }

    private void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (gameActivated)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer > currentScanTime)
            {
                //Checks if number matches

                currentTimer = 0;
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
                ChangeMaterialMode(checkMaterial, BlendMode.Fade);
                ChangeMaterialMode(crossMaterial, BlendMode.Fade);

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

    private void createScanObject()
    {
        GameObject tempWindow = Instantiate(scanPrefab, this.displayPanel.transform.position, this.displayPanel.transform.rotation);
        tempWindow.transform.SetParent(this.displayPanel.transform);
        tempWindow.GetComponent<RectTransform>().localScale = new Vector3(5.0f, 5.0f, 5.0f);
        tempWindow.transform.localPosition = new Vector3(Random.Range(-125, 125f), Random.Range(-65f, 65f), 0);
    }

    //Code to change material mode (Adapted from: https://answers.unity.com/questions/1004666/change-material-rendering-mode-in-runtime.html)
    private enum BlendMode
    {
        Opaque,
        Fade,
    }

    private void ChangeMaterialMode(Material standardShaderMaterial, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = -1;
                break;
            case BlendMode.Fade:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
        }
    }
}
