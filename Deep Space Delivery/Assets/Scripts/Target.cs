using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    void Update()
    {
        var temp = this.transform.localPosition;
        temp.z = 0;
        this.transform.localPosition = temp;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
