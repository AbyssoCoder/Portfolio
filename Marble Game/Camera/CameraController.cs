using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;
 

    void Start()
    {
        initalOffset = transform.position - target.position;
    }

    void FixedUpdate()
    {
       // cameraPosition = target.position + initalOffset;
        // transform.position = cameraPosition;
    }
    private void LateUpdate()
    {
        transform.LookAt(target.transform);
    }

}
    

