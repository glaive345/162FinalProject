using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPressurePlate : MonoBehaviour
{
    [SerializeField] GameObject Door1;
    [SerializeField] GameObject Door2;

    private int StandingCount;
    private BoxCollider PlateCollider;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        PlateCollider = this.gameObject.GetComponent<BoxCollider>();
        Timer = 0.0f;
        StandingCount = 0;
    }


    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(Timer < 1.0f && StandingCount == 0)
        {
            Door1.GetComponent<Animation>()["open"].time = 1 - Timer;
            Door1.GetComponent<Animation>().Play("open");
            Door2.GetComponent<Animation>()["open"].time = 1 - Timer;
            Door2.GetComponent<Animation>().Play("open");
            Timer = 1 - Timer;
        }
        else if(StandingCount == 0)
        {
            Door1.GetComponent<Animation>().Play("open");
            Door2.GetComponent<Animation>().Play("open");
            Timer = 0.0f;
        }
        StandingCount += 1;
    }

    private void OnTriggerExit(Collider other)
    {
        if(Timer < 1.0f && StandingCount == 1)
        {
            Door1.GetComponent<Animation>()["close"].time = 1 - Timer;
            Door1.GetComponent<Animation>().Play("close");
            Door2.GetComponent<Animation>()["close"].time = 1 - Timer;
            Door2.GetComponent<Animation>().Play("close");
            Timer = 1 - Timer;
        }
        else if(StandingCount == 1)
        {
            Door1.GetComponent<Animation>().Play("close");
            Door2.GetComponent<Animation>().Play("close");
            Timer = 0.0f;
        }
        StandingCount -= 1;
    }
}
