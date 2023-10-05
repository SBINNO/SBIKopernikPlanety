using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerDestroy : MonoBehaviour
{
    private Animator doorAnimator;
    private GameObject openDoorTriggerObject;
    private GameObject closeDoorTriggerObject;

    private void Awake()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
        openDoorTriggerObject = GetComponentInChildren<OpenDoorTrigger>().gameObject;
        closeDoorTriggerObject = GetComponentInChildren<CloseDoorTrigger>().gameObject;
    }

    private void Start()
    {
        if (openDoorTriggerObject != null)
        {
            var openDoorTrigger = openDoorTriggerObject.GetComponent<OpenDoorTrigger>();
            openDoorTrigger.OnTriggerEnterEvent += OpenDoor;
        }
        if (closeDoorTriggerObject != null)
        {
            var closeDoorTrigger = closeDoorTriggerObject.GetComponent<CloseDoorTrigger>();
            closeDoorTrigger.OnTriggerEnterEvent += CloseDoor;
        }
    }

    private void OpenDoor(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("OpenDoor");
            doorAnimator.SetTrigger("OpenDoorCollider");
            Debug.Log("Door opened");
            Destroy(openDoorTriggerObject);
        }
    }

    private void CloseDoor(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("CloseDoor");
            doorAnimator.SetTrigger("CloseDoorCollider");
            Debug.Log("Door closed");
            Destroy(closeDoorTriggerObject);
        }
    }
}
