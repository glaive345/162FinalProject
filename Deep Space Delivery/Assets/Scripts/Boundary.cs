using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var body = other.gameObject.GetComponent<Rigidbody>();
        body.velocity = Vector3.Reflect(body.velocity, -other.gameObject.transform.up);
    }
}
