using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorTrigger : MonoBehaviour
{
    public Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorLayer"))
        {
            Debug.Log("Player entered OpenDoorTrigger.");

            doorAnimator.SetTrigger("OpenDoor");
        }
        else if (other.CompareTag("DoorLayer"))
        {
            Debug.Log("Player entered CloseDoorTrigger.");

            doorAnimator.SetTrigger("CloseDoor");
        }
    }
}

