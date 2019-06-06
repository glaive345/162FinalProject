using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject Target;
    [SerializeField] Vector3 CameraOffset;

    private Camera ManagedCamera;

    // Start is called before the first frame update
    void Awake()
    {
        ManagedCamera = this.gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var targetPosition = Target.transform.position;
        var cameraPosition = targetPosition + CameraOffset;

        this.ManagedCamera.transform.position = cameraPosition;
    }
}
