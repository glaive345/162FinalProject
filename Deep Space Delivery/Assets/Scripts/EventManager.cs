using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime;

public class EventManager : MonoBehaviour
{
    private float time;
    System.Random random = new System.Random();
    // Use this for initialization
    [SerializeField] private GameObject laserZoneMG;
    private LaserZone laserZone;
    [SerializeField] private GameObject missileZoneMG;
    private MissileZone missileZone;
    [SerializeField] private GameObject sodaZoneMG;
    private SodaZone sodaZone;
    [SerializeField] private GameObject Player1;
    private P1Controller p1Controller;
    [SerializeField] private GameObject Player2;
    private P2Controller p2Controller;
    [SerializeField] private GameObject UIAlerts;
    private Text AlertText;


    private MyTuple AsteroidEvent;
    private MyTuple CaffeineEvent;
    private MyTuple SpaceMonsterEvent;
    private MyTuple PowerOuttageEvent;

    private MyTuple[] eventList;

    private List<int> currentEvents;
    private float TIMELIMIT = 10.0f;

    void Start()
    {
        this.time = 0.0f;
        this.AsteroidEvent = new MyTuple (false,0.0f);
        this.CaffeineEvent = new MyTuple(false, 0.0f);
        this.SpaceMonsterEvent = new MyTuple(false, 0.0f);
        this.PowerOuttageEvent = new MyTuple(false, 0.0f);

        this.eventList = new MyTuple[] { AsteroidEvent, this.CaffeineEvent, this.SpaceMonsterEvent, this.PowerOuttageEvent };
        laserZone = laserZoneMG.GetComponent<LaserZone>();
        missileZone = missileZoneMG.GetComponent<MissileZone>();
        sodaZone = sodaZoneMG.GetComponent<SodaZone>();
        p1Controller = Player1.GetComponent<P1Controller>();
        p2Controller = Player2.GetComponent<P2Controller>();

        AlertText = UIAlerts.transform.GetChild(1).GetComponent<Text>();

        currentEvents = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        time = time+Time.deltaTime;
        if (time > 15.00f)//every 15 seconds
        {
            eventAssignment();
            incrementTime();
            time = 0.0f;
        }
    }

    void eventAssignment()
    {
        int num = random.Next(0, 3);
        while (this.currentEvents.Contains(num) == true)//while newEvent is in the current event
        {
            num = random.Next(0, 3);
            this.currentEvents.Add(num);
            Debug.Log(num);
        }
        Debug.Log(num);

        if (this.eventList[num].Item1 != true)
        {
            this.eventList[num].Item1 = true;
            switch (num)
            {
                case 0:
                    this.laserZone.setActiveEvent(true);
                    this.updateAlerts("laser");
                    break;
                case 1:
                    this.sodaZone.setActiveEvent(true);
                    p1Controller.changeSpeed(.5f);
                    p2Controller.changeSpeed(.5f);
                    Debug.Log("slowdown");
                    this.updateAlerts("soda");
                    break;
                case 2:
                    this.missileZone.setActiveEvent(true);
                    this.updateAlerts("missile");
                    break;
                case 3:
                    //"disables" all the minigames for 3 secons?
                    break;
            }
        }
        return;
    }

    void incrementTime()
    {
        int count = 0;
        foreach (MyTuple i in this.eventList)
        {
            if (i.Item1 == true)
            {
                i.Item2 = i.Item2 + Time.deltaTime;
            }//close if
            if (i.Item2 > this.TIMELIMIT)
            {
                i.Item1 = false;
                //some failure statement
                this.currentEvents.Remove(count);
            }
            count++;
        }//close foreach
        return;
    }
    public void returnFunction(string minigameName)
    {
        switch (minigameName)
        {
            case "laser":
                this.eventList[0].Item1 = false;
                AlertText.text = "You're not bored anymore!";
                this.currentEvents.Remove(0);
                break;
            case "missile":
                this.eventList[0].Item1 = false;
                this.currentEvents.Remove(1);
                AlertText.text = "Asteroid Destroyed!";
                break;
            case "soda":
                this.eventList[0].Item1 = false;
                p1Controller.changeSpeed(1.0f);
                p2Controller.changeSpeed(1.0f);
                this.currentEvents.Remove(2);
                AlertText.text = "Yay, caffeinated again!";
                break;
        }
    }

    public void updateAlerts(string eventName)
    {
        switch (eventName)
        {
            case "soda":
                AlertText.text = "You are un-caffeinated. Go get some soda";
                break;
            case "missile":
                AlertText.text = "Asteroid approaching. Use missiles to shoot it down";
                break;
            case "laser":
                AlertText.text = "You're bored. Like REALLY bored. play the laser game";
                break;
            case"clear":
                AlertText.text = "";
                break;
        }
    }
}

public class MyTuple
{
   public MyTuple(bool item1, float item2)
   {
     Item1 = item1;
     Item2 = item2; 
   }
   public bool Item1 {get;set;}
   public float Item2 {get;set;}
}