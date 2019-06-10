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

    private MyTuple AsteroidEvent;
    private MyTuple CaffeineEvent;
    private MyTuple SpaceMonsterEvent;
    private MyTuple PowerOuttageEvent;

    private MyTuple[] eventList;

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
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.deltaTime;
        if (time % 10.0f == 0)//every 10 seconds
        {
            eventAssignment();
            incrementTime();
        }
    }

    void eventAssignment()
    {
        int num = random.Next(0, 3);
        if (this.eventList[num].Item1 != true)
        {
            this.eventList[num].Item1 = true;
            switch (num)
            {
                case 0:
                    //if ship is running low on shield when time limit ends, deals damage
                    //if laser game is completed, this.eventlist[num[.Item1 = falase, item2 =0.0f;
                    break;
                case 1:
                    //if soda machine minigame is not finished, slows down the player. after 10 seconds,
                    //player movement is slowed down by 50%.
                    //if played
                    break;
                case 2:
                    //same with Asteroid Event
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
        foreach (MyTuple i in this.eventList)
        {
            if (i.Item1 == true)
            {
                i.Item2 = i.Item2 + Time.deltaTime;
            }//close if
            if (i.Item2 == this.TIMELIMIT)
            {
                i.Item1 = false;
                //some failure statement
            }
        }//close foreach
        return;
    }
    public void returnFunction(bool missionCompleted)
    {
        this.eventList[1].Item1 = missionCompleted;
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