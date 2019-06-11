using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullBarManager : MonoBehaviour
{
    [SerializeField] private GameObject hullBar;

    private float currentChangePercent;
    private float maxHull;
    private Vector3 hullCenter;

    // Start is called before the first frame update
    void Start()
    {
        currentChangePercent = 0;
        maxHull = hullBar.transform.localScale.x;
        hullCenter = hullBar.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var currentChange = currentChangePercent * maxHull / 100;

        if (hullBar.transform.localScale.x + currentChange <= maxHull && hullBar.transform.localScale.x + currentChange >= 0)
        {
            hullBar.transform.localScale = new Vector3(hullBar.transform.localScale.x + currentChange, hullBar.transform.localScale.y, hullBar.transform.localScale.z);
            hullBar.transform.localPosition = new Vector3(hullBar.transform.localPosition.x + currentChange / 2, hullBar.transform.localPosition.y, hullBar.transform.localPosition.z);
        }
        else if (hullBar.transform.localScale.x + currentChange > maxHull)
        {
            hullBar.transform.localScale = new Vector3(maxHull, hullBar.transform.localScale.y, hullBar.transform.localScale.z);
            hullBar.transform.localPosition = hullCenter;
        }
        else
        {
            hullBar.transform.localScale = new Vector3(0, hullBar.transform.localScale.y, hullBar.transform.localScale.z);
            hullBar.transform.localPosition = new Vector3(hullCenter.x - maxHull / 2, hullBar.transform.localPosition.y, hullBar.transform.localPosition.z);
        }
        currentChangePercent = 0;
    }

    public void changeBar(float percent)
    {
        currentChangePercent += percent;
    }
}
