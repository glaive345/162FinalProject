using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileZone : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private GameObject UIAmmo;

    private bool gameActivated;
    private string currentPlayer;

    //ADD OTHER VARIABLES HERE
    [SerializeField] private float sensitivityMultiplier;
    [SerializeField] private float lockOnSensitivity;
    [SerializeField] private float lockOnSpeedMultiplier;
    [SerializeField] private float decayRateMultiplier;
    [SerializeField] private float targetSpeedMultiplier;
    [SerializeField] private float reloadSpeedMultiplier;
    [SerializeField] private float targetEraticness;

    private int maxAmmo;
    private int currentAmmo;

    private bool targetGoingUp;

    private GameObject reloadBar;
    private GameObject lockOnBar;
    private GameObject targeter;
    private GameObject target;
    private GameObject ammo;
    private GameObject empty;
    private int ammoshot;

    private bool eventActivated;
    private EventManager eventManager;
    [SerializeField] private GameObject UIScript;


    void Start()
    {
        this.gameActivated = false;
        this.currentPlayer = "None";

        //PREINITIALIZE VARIABLES HERE
        maxAmmo = 4;
        currentAmmo = 0;
        this.ammoshot = 0;

        targetGoingUp = true;

        reloadBar = this.displayPanel.transform.GetChild(6).gameObject;
        lockOnBar = this.displayPanel.transform.GetChild(3).gameObject;
        targeter = this.displayPanel.transform.GetChild(0).gameObject;
        target = this.displayPanel.transform.GetChild(1).gameObject;
        ammo = this.displayPanel.transform.GetChild(4).gameObject;
        empty = this.displayPanel.transform.GetChild(7).gameObject;


        this.displayPanel.SetActive(false);

        eventManager = UIScript.GetComponent<EventManager>();
    }

    private void Update()
    {
        //Updates ReloadBar even when not active
        if(currentAmmo < maxAmmo)
        {
            var reloadChange = reloadSpeedMultiplier * Time.deltaTime;
            if(reloadBar.transform.localScale.y + reloadChange < 250)
            {
                reloadBar.transform.localScale = new Vector3(reloadBar.transform.localScale.x, reloadBar.transform.localScale.y + reloadChange, reloadBar.transform.localScale.z);
                reloadBar.transform.localPosition = new Vector3(reloadBar.transform.localPosition.x, reloadBar.transform.localPosition.y + reloadChange / 2, reloadBar.transform.localPosition.z);
            }
            else
            {
                var ammoMaterial = ammo.transform.GetChild(currentAmmo).gameObject.GetComponent<Renderer>().material;
                var UIAmmoMaterial = UIAmmo.transform.GetChild(currentAmmo).gameObject.GetComponent<Renderer>().material;

                //Code to change material mode from fade to opaque
                ChangeMaterialMode(ammoMaterial, BlendMode.Opaque);
                ChangeMaterialMode(UIAmmoMaterial, BlendMode.Opaque);

                currentAmmo++;
                empty.SetActive(false);
                reloadBar.transform.localScale = new Vector3(reloadBar.transform.localScale.x, 0, reloadBar.transform.localScale.z);
                reloadBar.transform.localPosition = new Vector3(reloadBar.transform.localPosition.x, -125, reloadBar.transform.localPosition.z);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (gameActivated)
        {
            //Moves targeter down automatically by half of sensitivity multiplier
            if (targeter.transform.localPosition.y - sensitivityMultiplier * Time.deltaTime / 2 > -100)
            {
                targeter.transform.localPosition = new Vector3(targeter.transform.localPosition.x, targeter.transform.localPosition.y - sensitivityMultiplier * Time.deltaTime / 2, targeter.transform.localPosition.z);
            }
            else
            {
                targeter.transform.localPosition = new Vector3(targeter.transform.localPosition.x, -100, targeter.transform.localPosition.z);
            }

            //Decides whether to change target direction
            if(Random.Range(0f,1f) < targetEraticness)
            {
                //Flips bool
                targetGoingUp = !targetGoingUp;
            }
            var targetMotion = targetSpeedMultiplier * Time.deltaTime;
            //Inverts if going down
            if (!targetGoingUp)
            {
                targetMotion = -targetMotion;
            }
            //Moves target within bounds -90 to 90
            if(target.transform.localPosition.y + targetMotion > 90)
            {
                target.transform.localPosition = new Vector3(target.transform.localPosition.x, 90, target.transform.localPosition.z);
                targetGoingUp = false;
            }
            else if (target.transform.localPosition.y + targetMotion < -90)
            {
                target.transform.localPosition = new Vector3(target.transform.localPosition.x, -90, target.transform.localPosition.z);
                targetGoingUp = true;
            }
            else
            {
                target.transform.localPosition = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y + targetMotion, target.transform.localPosition.z);
            }
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
                //Moves targeter up to a max
                if (targeter.transform.localPosition.y + sensitivityMultiplier * Time.deltaTime < 100)
                {
                    targeter.transform.localPosition = new Vector3(targeter.transform.localPosition.x, targeter.transform.localPosition.y + sensitivityMultiplier * Time.deltaTime, targeter.transform.localPosition.z);
                }
                else
                {
                    targeter.transform.localPosition = new Vector3(targeter.transform.localPosition.x, 100, targeter.transform.localPosition.z);
                }
            }
        }
        //Checks location of target and targeter for lock on
        if (gameActivated)
        {
            var lockOnChange = lockOnSpeedMultiplier * Time.deltaTime;
            if(Mathf.Abs(targeter.transform.localPosition.y - target.transform.localPosition.y) < lockOnSensitivity && currentAmmo > 0)
            {
                lockOnBar.transform.localScale = new Vector3(lockOnBar.transform.localScale.x, lockOnBar.transform.localScale.y + lockOnChange, lockOnBar.transform.localScale.z);
                //Fire missile if locked on
                if (lockOnBar.transform.localScale.y > 250)
                {
                    currentAmmo--;
                    ammoshot++;
                    if(ammoshot == 3)
                    {
                        this.eventManager.returnFunction("missile");
                        ammoshot = 0;
                    }
                    //Changing material from opaque to fade
                    var ammoMaterial = ammo.transform.GetChild(currentAmmo).gameObject.GetComponent<Renderer>().material;
                    var UIAmmoMaterial = UIAmmo.transform.GetChild(currentAmmo).gameObject.GetComponent<Renderer>().material;
                    ChangeMaterialMode(ammoMaterial, BlendMode.Fade);
                    ChangeMaterialMode(UIAmmoMaterial, BlendMode.Fade);

                    if (currentAmmo == 0)
                    {
                        empty.SetActive(true);
                    }

                    lockOnBar.transform.localScale = new Vector3(lockOnBar.transform.localScale.x, 0, lockOnBar.transform.localScale.z);


                    //NEED TO SEND RESULTS TO SUBSCRIBERS
                }
            }
            else
            {
                if(lockOnBar.transform.localScale.y - lockOnChange * decayRateMultiplier < 0)
                {
                    lockOnBar.transform.localScale = new Vector3(lockOnBar.transform.localScale.x, 0, lockOnBar.transform.localScale.z);
                }
                else
                {
                    lockOnBar.transform.localScale = new Vector3(lockOnBar.transform.localScale.x, lockOnBar.transform.localScale.y - lockOnChange * decayRateMultiplier, lockOnBar.transform.localScale.z);
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
            lockOnBar.transform.localScale = new Vector3(lockOnBar.transform.localScale.x, 0, lockOnBar.transform.localScale.z);
            target.transform.localPosition = new Vector3(target.transform.localPosition.x, 60, target.transform.localPosition.z);
            targeter.transform.localPosition = new Vector3(targeter.transform.localPosition.x, -100, targeter.transform.localPosition.z);


            this.gameActivated = false;
            Debug.Log("game deactivated");

            this.displayPanel.SetActive(false);

            currentPlayer = "None";


            //NEED TO SEND EARLY EXIT TO SUBSCRIBERS
        }
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
    public void setActiveEvent(bool setEvent)//string if more than one event
    {
        this.eventActivated = setEvent;
    }
}
