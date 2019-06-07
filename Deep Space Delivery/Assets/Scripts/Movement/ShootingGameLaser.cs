using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGameLaser : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Target")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
