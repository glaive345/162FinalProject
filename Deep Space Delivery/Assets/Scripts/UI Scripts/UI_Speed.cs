using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Speed : MonoBehaviour
{
    [SerializeField] private Text UISpeed;
    [SerializeField] private Text UIMaxSpeed;
    [SerializeField] private Text UIDistance;
    [SerializeField] private GameObject UIShipTracker;
    private float distanceNeeded;
    private float distanceRemaining;

    [SerializeField] private GameObject refuelZoneGame;
    private RefuelZone refuelZone;

    [SerializeField] private float thrustDecay;
    [SerializeField] private float maximumThrust;
    private float currentEngineThrust;
    private float currentEngineThrustExternal;
    private float currentBridge;
    private float currentRadar;

    private bool mainCurrent;
    private bool topCurrent;
    private bool botCurrent;

    [SerializeField] private ParticleSystem mainEngineParticle;
    [SerializeField] private ParticleSystem topEngineParticle;
    [SerializeField] private ParticleSystem botEngineParticle;

    [SerializeField] private float skyboxRotationMultiplier;
    private float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        refuelZone = refuelZoneGame.GetComponent<RefuelZone>();
        currentEngineThrust = 0;
        currentEngineThrustExternal = 1;
        currentBridge = 1;
        currentRadar = 1;
        distanceNeeded = 5000;
        distanceRemaining = 5000;

        mainCurrent = true;
        topCurrent = true;
        botCurrent = true;

        currentRotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Plays particles if refuelzone shows it is active
        if(mainCurrent != refuelZone.mainActive)
        {
            mainCurrent = refuelZone.mainActive;
            if (mainCurrent)
            {
                mainEngineParticle.Play();
            }
            else
            {
                mainEngineParticle.Stop();
            }
        }
        if (topCurrent != refuelZone.topActive)
        {
            topCurrent = refuelZone.topActive;
            if (topCurrent)
            {
                topEngineParticle.Play();
            }
            else
            {
                topEngineParticle.Stop();
            }
        }
        if (botCurrent != refuelZone.botActive)
        {
            botCurrent = refuelZone.botActive;
            if (botCurrent)
            {
                botEngineParticle.Play();
            }
            else
            {
                botEngineParticle.Stop();
            }
        }

        var currentMaxSpeedMultiplier = 0f;
        if (mainCurrent)
        {
            currentMaxSpeedMultiplier += .5f;
        }
        if (topCurrent)
        {
            currentMaxSpeedMultiplier += .25f;
        }
        if (botCurrent)
        {
            currentMaxSpeedMultiplier += .25f;
        }

        //If above current max speed
        if(currentEngineThrust > currentMaxSpeedMultiplier * currentEngineThrustExternal)
        {
            currentEngineThrust -= thrustDecay * Time.deltaTime;
            //If overshot
            if(currentEngineThrust < currentMaxSpeedMultiplier * currentEngineThrustExternal)
            {
                currentEngineThrust = currentMaxSpeedMultiplier;
            }
        }
        //If below current max speed
        if(currentEngineThrust < currentMaxSpeedMultiplier * currentEngineThrustExternal)
        {
            currentEngineThrust += thrustDecay * Time.deltaTime;
            //If overshot
            if (currentEngineThrust > currentMaxSpeedMultiplier * currentEngineThrustExternal)
            {
                currentEngineThrust = currentMaxSpeedMultiplier;
            }
        }

        //Displays Max Speed
        UIMaxSpeed.text = "Max: " + (maximumThrust * currentMaxSpeedMultiplier * currentEngineThrustExternal).ToString("F");
        //Displays current speed
        UISpeed.text = "Speed: " + (maximumThrust * currentEngineThrust).ToString("F") + " AU/sec";
        //Updates Distance
        distanceRemaining -= maximumThrust * currentEngineThrust * Time.deltaTime;
        if(distanceRemaining < 0)
        {
            UIDistance.text = "Location Reached: Freeplay Mode\n " +  (-distanceRemaining).ToString("F");
            //ADD ON REACHED EFFECTS
            //ADD RESET TIMER FOR NEXT LOCATION
        }
        else
        {
            UIDistance.text = "Distance Remaining: " + (distanceRemaining).ToString("F") + " AU";
        }
        //Moves ship tracker (From -240 to 190)
        var currentTrackerPosition = (distanceNeeded - distanceRemaining) / distanceNeeded * 430;
        if(currentTrackerPosition < 430)
        {
            UIShipTracker.transform.localPosition = new Vector3(-240 + currentTrackerPosition, UIShipTracker.transform.localPosition.y, UIShipTracker.transform.localPosition.z);
        }
        else
        {
            UIShipTracker.transform.localPosition = new Vector3(190, UIShipTracker.transform.localPosition.y, UIShipTracker.transform.localPosition.z);
        }


        //Changes skybox rotation speed
        currentRotation += Time.deltaTime * skyboxRotationMultiplier * currentEngineThrust;
        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);
    }

    //Combines both influences from bridge and radar
    private void changeThrustExternal(float bridgeThrust, float radarThrust)
    {
        currentEngineThrustExternal = .5f + bridgeThrust * .25f + radarThrust * .25f;
    }

    public void changeThrustBridge(float thrustChange)
    {
        currentBridge = thrustChange;
        changeThrustExternal(currentBridge, currentRadar);
    }
    public void changeThrustRadar(float thrustChange)
    {
        currentRadar = thrustChange;
        changeThrustExternal(currentBridge, currentRadar);
    }

    public void zeroThrust()
    {
        currentEngineThrust = 0;
    }

    public void maxThrust()
    {
        currentEngineThrust = 1;
    }
}
