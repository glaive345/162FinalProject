using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBarManager : MonoBehaviour
{
    [SerializeField] private GameObject laserBar;
    [SerializeField] private GameObject laserZone;
    private float laserZoneHeat;
    private float xStart;

    private void Start()
    {
        laserZoneHeat = laserZone.GetComponent<LaserZone>().heat;
        xStart = laserBar.transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        laserZoneHeat = laserZone.GetComponent<LaserZone>().heat;

        laserBar.transform.localScale = new Vector3(laserZoneHeat * 3.5f, laserBar.transform.localScale.y, laserBar.transform.localScale.z);
        laserBar.transform.localPosition = new Vector3(xStart + laserZoneHeat * 3.5f / 2, laserBar.transform.localPosition.y, laserBar.transform.localPosition.z);

    }
}
