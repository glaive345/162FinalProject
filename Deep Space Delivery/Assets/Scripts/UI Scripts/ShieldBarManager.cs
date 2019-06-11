using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBarManager : MonoBehaviour
{
    [SerializeField] private GameObject shieldBar;
    [SerializeField] private float naturalDecay;

    private float currentChangePercent;
    private float maxShield;
    private Vector3 shieldCenter;

    public bool shieldActive;

    // Start is called before the first frame update
    void Start()
    {
        currentChangePercent = 0;
        maxShield = shieldBar.transform.localScale.x;
        shieldCenter = shieldBar.transform.localPosition;
        shieldActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentChangePercent -= naturalDecay * Time.deltaTime;

        var currentChange = currentChangePercent * maxShield / 100;

        if(shieldBar.transform.localScale.x + currentChange <= maxShield && shieldBar.transform.localScale.x + currentChange >= 0)
        {
            shieldBar.transform.localScale = new Vector3(shieldBar.transform.localScale.x + currentChange, shieldBar.transform.localScale.y, shieldBar.transform.localScale.z);
            shieldBar.transform.localPosition = new Vector3(shieldBar.transform.localPosition.x + currentChange / 2, shieldBar.transform.localPosition.y, shieldBar.transform.localPosition.z);
        }
        else if(shieldBar.transform.localScale.x + currentChange > maxShield)
        {
            shieldBar.transform.localScale = new Vector3(maxShield, shieldBar.transform.localScale.y, shieldBar.transform.localScale.z);
            shieldBar.transform.localPosition = shieldCenter;
        }
        else
        {
            shieldBar.transform.localScale = new Vector3(0, shieldBar.transform.localScale.y, shieldBar.transform.localScale.z);
            shieldBar.transform.localPosition = new Vector3(shieldCenter.x - maxShield / 2, shieldBar.transform.localPosition.y, shieldBar.transform.localPosition.z);
        }
        currentChangePercent = 0;

        //Sets public variable
        if(shieldBar.transform.localScale.x > 10)
        {
            shieldActive = true;
        }
        else
        {
            shieldActive = false;
        }
    }

    public void changeBar(float percent)
    {
        currentChangePercent += percent;
    }
}
