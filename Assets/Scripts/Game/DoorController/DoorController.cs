using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;
    private OpenDoorTrigger openDoorTrigger;
    private CloseDoorTrigger closeDoorTrigger;


    protected virtual void Awake()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
    }
    protected void Start()
    {
        openDoorTrigger = GetComponentInChildren<OpenDoorTrigger>();
        closeDoorTrigger = GetComponentInChildren<CloseDoorTrigger>();
        if (openDoorTrigger != null)
        {
            openDoorTrigger.OnTriggerEnterEvent += OpenDoor;
        }
        if (closeDoorTrigger != null)
        {
            closeDoorTrigger.OnTriggerEnterEvent += CloseDoor;
        }
    }

    protected virtual void OpenDoor(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("OpenDoor");
            doorAnimator.SetTrigger("OpenDoorCollider");
            if (openDoorTrigger != null)
            {
                openDoorTrigger.GetComponent<Collider>().enabled = false;
            }
        }
    }
    protected virtual void CloseDoor(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("CloseDoor");
            doorAnimator.SetTrigger("CloseDoorCollider");
            Destroy(closeDoorTrigger);
            if (closeDoorTrigger != null)
            {
                closeDoorTrigger.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
