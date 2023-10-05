using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerWalkMobile : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private CharacterController cc;
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private float maxLookUpAngle = 80f;
    [SerializeField] private float maxLookDownAngle = 80f;

    bool moveForward = false;
    bool moveBackward = false;
    bool strafeLeft = false;
    bool strafeRight = false;

    float currentXRotation = 0f;
    bool isPanelNull = false;

    private void Awake()
    {
        if (buttonsPanel == null)
            isPanelNull = true;
    }
    // Update is called once per frame
    void Update()
    {
        TouchRotation();
        CalculateGravity();
        CalculateMovement();
    }
    private void OnDisable()
    {
        moveForward = false;
        moveBackward = false;
        strafeLeft = false;
        strafeRight = false;
    }
    private void CalculateGravity()
    {
        Vector3 gravityVector = Vector3.down * gravity * Time.deltaTime;
        cc.Move(gravityVector);
    }
    private void CalculateMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        if (moveForward) moveDirection += transform.forward;
        if (moveBackward) moveDirection -= transform.forward;
        if (strafeLeft) moveDirection -= transform.right;
        if (strafeRight) moveDirection += transform.right;

        moveDirection.Normalize();
        moveDirection *= speed * Time.deltaTime;

        // Move the character controller
        cc.Move(moveDirection);

    }
    public void MoveForward(bool movePlayer)
    {
        moveForward = movePlayer;
    }

    public void MoveBackward(bool movePlayer)
    {
        moveBackward = movePlayer;
    }

    public void StrafeLeft(bool movePlayer)
    {
        strafeLeft = movePlayer;
    }

    public void StrafeRight(bool movePlayer)
    {
        strafeRight = movePlayer;
    }
    private void TouchRotation()
    {
        int activeTouchCount = Input.touchCount;

        for (int i = 0; i < activeTouchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (IsPointerOverUIObject())
            {
                Debug.Log("IsTouchingPanel");
                continue;
            }

            float rotationX = -touch.deltaPosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotationY, Space.World);

            currentXRotation += rotationX;
            currentXRotation = Mathf.Clamp(currentXRotation, -maxLookDownAngle, maxLookUpAngle);

            Quaternion xRotation = Quaternion.Euler(currentXRotation, 0f, 0f);
            Quaternion yRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);

            // Quaternion rotation based on X
            Quaternion newRotation = yRotation * xRotation;

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult result in results)
        {
            if (!isPanelNull)
            {
                if (result.gameObject == buttonsPanel)
                {
                    return results.Count > 0;
                }
            }

        }
        return false;
    }
}
