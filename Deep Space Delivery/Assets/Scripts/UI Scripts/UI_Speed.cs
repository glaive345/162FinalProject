using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Speed : MonoBehaviour
{
    [SerializeField] private Text UISpeed;
    [SerializeField] private Text UIDistance;
    [SerializeField] private GameObject UIShipTracker;

    [SerializeField] private GameObject refuelZoneGame;
    private RefuelZone refuelZone;

    [SerializeField] private float maximumThrust;
    private float currentEngineThrust;
    private float currentEngineThrustExternal;

    [SerializeField] private float skyboxRotationMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        refuelZone = refuelZoneGame.GetComponent<RefuelZone>();
        currentEngineThrust = maximumThrust;
    }

    // Update is called once per frame
    void Update()
    {
        //NEEDS WORK

    }

    public void changeThrust(float thrustChange)
    {
        currentEngineThrustExternal += thrustChange;
    }

    public void zeroThrust()
    {
        currentEngineThrust = 0;
    }

    public void maxThrust()
    {
        currentEngineThrust = maximumThrust;
    }
}
