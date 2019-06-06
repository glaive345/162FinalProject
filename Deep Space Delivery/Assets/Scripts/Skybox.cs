using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    [SerializeField] private float RotationSpeed;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationSpeed);
    }


}
