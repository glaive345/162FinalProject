using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Speed : MonoBehaviour
{
    [SerializeField] private Text UISpeed;
    [SerializeField] private Text UIDistance;
    [SerializeField] private GameObject UIShipTracker;

    [SerializeField] private float maximumThrust;
    private float currentEngineThrust;

    [SerializeField] private float skyboxRotationMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        currentEngineThrust = maximumThrust;
    }

    // Update is called once per frame
    void Update()
    {
        //NEEDS WORK
    }

    public void changeThrust(float thrustChange)
    {
        currentEngineThrust += thrustChange;
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
