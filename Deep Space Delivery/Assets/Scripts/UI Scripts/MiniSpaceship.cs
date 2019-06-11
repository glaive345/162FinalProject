using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpaceship : MonoBehaviour
{
    void Start()
    {
        this.gameObject.transform.localPosition = new Vector3(-300, Random.Range(-150.0f, 150.0f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x + 0.5f, this.gameObject.transform.localPosition.y, 0);
    }
}
