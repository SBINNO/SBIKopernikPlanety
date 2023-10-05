using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectMover : ObjectActionHandler
{
    public Vector3 firstPosition;
    public Vector3 lastPosition;
    public float objectSpeed = 1.0f;

    private bool isInFirstPosition = true;
    private bool activateMovement = false;
    private void Update()
    {
        if (activateMovement)
        {
            MoveObjectToPositions();
        }
    }
    private void MoveObjectToPositions()
    {
        if (isInFirstPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition, Time.deltaTime * objectSpeed);
            if (transform.position == lastPosition)
            {
                isInFirstPosition = false;
                activateMovement = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPosition, Time.deltaTime * objectSpeed);
            if (transform.position == firstPosition)
            {
                isInFirstPosition = true;
                activateMovement = false;
            }
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        activateMovement = true;
    }
}
