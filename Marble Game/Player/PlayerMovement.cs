using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 6.0f;
    [SerializeField] Camera cam;
    void Update()
    {
        Vector3 CamForward = cam.transform.forward;
        Vector3 CamRight = cam.transform.right;
        Vector3 movement = (Input.GetAxis("Horizontal") * CamRight * movementSpeed) + (Input.GetAxis("Vertical") * CamForward * movementSpeed);
        movement *= Time.deltaTime;
        GetComponent<Rigidbody>().AddForce(movement, ForceMode.Force);
    }


}