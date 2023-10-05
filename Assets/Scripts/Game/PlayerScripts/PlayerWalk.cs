using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerWalk : MonoBehaviour
{

    public Transform vrPlayer;

    public float LookDownAngle = 25.0f;
    public float Speed = 3.0f;
    public bool MoveForward;
    public bool MoveForwardEditor;
    CharacterController cc;

      
    
    float minXRotation = -90f;
    float maxXRotation = 90f;
    float currentXRotation = 0f;
    public float rotationSpeed = 10f;        


    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        EditorMovement();
        MouseMovement();
    }


    //void VRMovement()
    //{
    //    if (vrPlayer.eulerAngles.x >= LookDownAngle && vrPlayer.eulerAngles.x < 90.0f)
    //    {
    //        MoveForward = true;
    //    }
    //    else
    //    {
    //        MoveForward = false;
    //    }

    //    if (MoveForward || MoveForwardEditor)
    //    {
    //        Vector3 forward = vrPlayer.TransformDirection(Vector3.forward);

    //        cc.SimpleMove(forward * Speed);
    //    }
    //}

    void EditorMovement()
    {
        float editorSpeed = Speed * 500;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the player's forward direction based on the current rotation
        Vector3 forward = transform.forward;
        forward.y = 0f; // Ignore any vertical rotation

        // Get the right direction based on the current rotation
        Vector3 right = transform.right;

        // Calculate the movement vector by combining the forward and right vectors with the input values
        Vector3 movement = (forward * verticalInput + right * horizontalInput) * editorSpeed * Time.deltaTime;

        cc.SimpleMove(movement);

    }
    void MouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;


        // Vertical rotation
        transform.Rotate(Vector3.up, mouseX);

        // Horizontal rotation
        currentXRotation -= mouseY;
        currentXRotation = Mathf.Clamp(currentXRotation, minXRotation, maxXRotation - LookDownAngle);

        Quaternion xRotation = Quaternion.Euler(currentXRotation, 0f, 0f);
        Quaternion yRotation = Quaternion.Euler(0f, transform.localEulerAngles.y, 0f);

        // Quaternion rotation based on X
        Quaternion newRotation = yRotation * xRotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }
}
