using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class ObjectSkinnedHolder : ObjectActionHandler
{
    private GameObject heldObject;
    private bool isHoldingObject = false;
    private Vector3 capsuleOffset; // Store the offset of the capsule collider

    private void Update()
    {
        if (isHoldingObject)
        {
            MoveHeldObject();
        }
    }

    public bool IsHoldingObject()
    {
        return isHoldingObject;
    }

    public void ReleaseObject()
    {
        isHoldingObject = false;
    }

    private void MoveHeldObject()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position + player.transform.forward;
            heldObject.transform.position = targetPosition;
            heldObject.transform.rotation = player.transform.rotation;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!isHoldingObject)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                isHoldingObject = true;
                heldObject = gameObject;
                heldObject.transform.SetParent(player.transform);
                heldObject.GetComponent<Rigidbody>().isKinematic = true;

                // Calculate the offset of the capsule collider
                CapsuleCollider capsuleCollider = heldObject.GetComponent<CapsuleCollider>();
                if (capsuleCollider != null)
                {
                    Vector3 meshBoundsCenter = heldObject.GetComponent<MeshRenderer>().bounds.center;
                    capsuleOffset = heldObject.transform.position - meshBoundsCenter;
                }
            }
        }
        else
        {
            isHoldingObject = false;
            heldObject.transform.SetParent(null);
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject = null;
        }
    }
}
