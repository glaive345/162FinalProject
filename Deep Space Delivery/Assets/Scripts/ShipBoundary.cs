using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBoundary : MonoBehaviour
{
    [SerializeField] private LaserZone lz;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            Destroy(other.gameObject);
            this.lz.Damage--;
        }
    }
}
